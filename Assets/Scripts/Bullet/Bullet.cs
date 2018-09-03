using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int nb_rebond = 5;
    Rigidbody2D rb;
    CircleCollider2D collider;
    Vector3 currDir;
    Vector2 destination;
    float vitesse = 15;

    float angle, rayLength = 1.5f, bounciness = 0.5f;

    public void initialization()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        collider = gameObject.GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * vitesse;
    }
    public void UpdateBullet()
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("NeutralObstacle") | 1 << gameObject.layer;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, rayLength, mask); //raycasting to verify collision with mask

        for (int i = 0; i < hits.Length; i++)
        {           
            Vector2 reflected = Vector2.Reflect(transform.right, hits[i].normal);

            if (collider.IsTouching(hits[i].collider)) 
            {
                angle = Vector2.Angle(transform.right, Vector2.Reflect(transform.right, hits[i].normal)); //retrieving angle formed by incoming direction and its reflection

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
                rb.velocity = Vector2.Reflect(transform.right, hits[i].normal) * vitesse * bounciness;
                //rotating the sprite's transform
                transform.Rotate(new Vector3(0, 0, 1), angle);
            }
        }
    }
}
