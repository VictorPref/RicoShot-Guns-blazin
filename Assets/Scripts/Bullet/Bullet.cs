using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float initialSpeed = 15, minSpeed = 3;
    private float angle, rayLength = 0.5f, bounciness = 0.75f;
    private int maxRicochets = 5;
    Rigidbody2D rb;
    LayerMask mask;
    LayerMask playersLayer;
    TrailRenderer trailRenderer;
    public Vector2 position;
    AudioSource audio;

    public void Initialize()
    { 
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * initialSpeed;

        //creating mask so bullet collides with: default map obstacles, its own layer(including the player's obstacles) and the players' layer
        playersLayer = LayerMask.NameToLayer("Player");
        mask = 1 << LayerMask.NameToLayer("NeutralObstacle") | 1 << gameObject.layer | 1 << playersLayer;
        trailRenderer = GetComponent<TrailRenderer>();
        audio = GetComponent<AudioSource>();
    }

    public void UpdateBullet()
    {
        //raycasting to check for collisions with mask
        if (gameObject )
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, rayLength, mask);

            for (int i = 0; i < hits.Length; i++)
            {
                //reduce lifespan
                maxRicochets--;
                audio.Play();
                Vector2 reflected = Vector2.Reflect(transform.right, hits[i].normal);
                //retrieving angle formed by incoming direction and its reflection
                angle = Mathf.Atan2(transform.right.x * reflected.y - transform.right.y * reflected.x, transform.right.x * reflected.x + transform.right.y * reflected.y) * 180 / Mathf.PI;
                //applying new velocity following the reflected direction
                initialSpeed *= bounciness;
                rb.velocity = reflected * initialSpeed;
                //rotating the sprite's transform
                transform.Rotate(new Vector3(0, 0, 1), angle);

                //if the bullet hits a player, kill the player
                if (hits[i].transform.gameObject.layer == playersLayer)
                {
                    Player p = hits[i].transform.gameObject.GetComponent<Player>();
                    p.PlayerDies();
                }
                //if bullet hits one of the players' obstacles, reduce its lifespan
                else if (hits[i].transform.gameObject.layer == gameObject.layer) {
                    hits[i].transform.gameObject.GetComponent<Obstacle>().ReduceLifespan();
                }
            }

            //checking if this is the last rebound allowed for this bullet
            if (IsBulletDead())
            {
                Destroy(gameObject);
            }
        }
    }

    public bool IsBulletDead()
    {
        //has this bullet reached the maximum rebounds allowed or has gone lower than the speed limit
        return maxRicochets == 0 || rb.velocity.magnitude <= minSpeed ? true : false;
    }

    public void KillBullet()
    {
        Destroy(gameObject);
    }

    public void ActivateTrailRenderer(int playerId) {  
        switch (playerId) {
            case 1:
                trailRenderer.startColor = Color.blue;
                break;
            case 2:
                trailRenderer.startColor = Color.red;
                break;
            default:
                break;
        }
        trailRenderer.enabled = true;
    }
}
