using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    int nb_rebond =5;
    Rigidbody2D rb;
    Vector3 currDir;
    Vector2 destination;
    float vitesse = 10;

    float angle, bounciness = .5f;

    public void initialization()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * vitesse;
        angle = transform.rotation.z;
        //rb.AddForce(transform.right);
        
    }
    public void UpdateBullet()
    {
        currDir = new Vector3(transform.position.x, transform.position.y, 0);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    { 
      Vector3 newDir = new Vector3(transform.position.x, transform.position.y, 0);
      float newDirValue = Mathf.Atan2(newDir.y - currDir.y, newDir.x - currDir.x);
      float newDirValueDeg = (180 / Mathf.PI) * newDirValue;
      transform.rotation = Quaternion.Euler(0, 0, newDirValueDeg);

      Vector2 inDirection = transform.position.normalized;
      Vector2 inNormal = collision.contacts[0].normal;
      Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);

      rb.velocity = transform.right * vitesse;

        nb_rebond--;
        if (nb_rebond <= 0) {
            Destroy(gameObject);
        }
    }
}
