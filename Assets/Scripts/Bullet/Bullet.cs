using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int maxRicochets = 5;
    Rigidbody2D rb;
    CircleCollider2D collider;
    Vector3 currDir;
    Vector2 destination;
    float initialSpeed = 15, minSpeed = 3, colliderWeight = 0.3f;
    float angle, rayLength = 1.5f, bounciness = 0.75f;

    public void initialization()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        collider = gameObject.GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * initialSpeed;
    }
    public void UpdateBullet()
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("NeutralObstacle") | 1 << gameObject.layer | 1 << LayerMask.NameToLayer("Player");

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, rayLength, mask); //raycasting to verify collision with mask

        Debug.DrawLine(transform.position, transform.position+ transform.right * rayLength);

        for (int i = 0; i < hits.Length; i++)
        {           
            Vector2 reflected = Vector2.Reflect(transform.right, hits[i].normal);

            if (collider.IsTouching(hits[i].collider)) 
            {
           //if( Mathf.Abs(transform.position.x - hits[i].point.x) < colliderWeight && Mathf.Abs(transform.position.y - hits[i].point.y) < colliderWeight) { 
                maxRicochets--;
                angle = Vector2.Angle(transform.right, reflected); //retrieving angle formed by incoming direction and its reflection

                //this check and the three next are to make sure the rotation angle goes clockwise if needed
                if (transform.right.x > 0 && reflected.x > 0 && transform.position.y > transform.position.y+ reflected.y) {
                    angle *= -1;
                }
                else if (transform.right.x > 0 && reflected.x < 0 && transform.position.y > transform.position.y+ reflected.y)
                {
                    angle *= -1;
                }
                else if (transform.right.x < 0 && reflected.x < 0 && transform.position.y < transform.position.y+ reflected.y)
                {
                    angle *= -1;
                }
                else if (transform.right.x < 0 && reflected.x > 0 && transform.position.y < transform.position.y+ reflected.y)
                {
                    angle *= -1;
                }
                //applying new velocity following the reflected direction
                initialSpeed *= bounciness;
                rb.velocity = reflected * initialSpeed;
                //rotating the sprite's transform
                transform.Rotate(new Vector3(0, 0, 1), angle);
            }
        }

        if (isBulletDead()) {
            Destroy(gameObject);
        }
    }

    public bool isBulletDead() {
        return maxRicochets == 0 || rb.velocity.magnitude <= minSpeed ? true : false;
    }
}
