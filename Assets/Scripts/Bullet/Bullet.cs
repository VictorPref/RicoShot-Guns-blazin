using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    private float initialSpeed = 15, minSpeed = 3;
    private float angle, rayLength = 0.5f, bounciness = 0.75f;
    private int maxRicochets = 5;

    public void initialization()
    {
        //gameObject.GetComponent<Renderer>().material.color = Color.white;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * initialSpeed;
    }
    public void UpdateBullet()
    {
        //creating mask so bullet collides with: default map obstacles, its own layer(including the player's obstacles) and the players' layer
        LayerMask mask = 1 << LayerMask.NameToLayer("NeutralObstacle") | 1 << gameObject.layer | 1 << LayerMask.NameToLayer("Player");

        //raycasting to check for collisions with mask
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, rayLength, mask);

        for (int i = 0; i < hits.Length; i++)
        {
            //reduce lifespan
            maxRicochets--;
            Vector2 reflected = Vector2.Reflect(transform.right, hits[i].normal);
            //retrieving angle formed by incoming direction and its reflection
            angle = Mathf.Atan2(transform.right.x * reflected.y - transform.right.y * reflected.x, transform.right.x * reflected.x + transform.right.y * reflected.y) * 180 / Mathf.PI;
            //applying new velocity following the reflected direction
            initialSpeed *= bounciness;
            rb.velocity = reflected * initialSpeed;
            //rotating the sprite's transform
            transform.Rotate(new Vector3(0, 0, 1), angle);

            //if the bullet hits a player, destroy the player
            if (hits[i].transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Destroy(hits[i].transform.gameObject);
                PlayerManager.Instance.isLastManStanding();
            }
        }



        //checking if this is the last rebound allowed for this bullet
        if (isBulletDead())
        {
            Destroy(gameObject);
        }
    }

    public bool isBulletDead()
    {
        //has this bullet reached minimum rebounds allowed
        return maxRicochets == 0 || rb.velocity.magnitude <= minSpeed ? true : false;
    }
}
