using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BulletManager
{
    private static BulletManager instance = null;

    List<Bullet> bullets = new List<Bullet>();
    int nbBaseLayer = 7;

    public static BulletManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BulletManager();

            }
            return instance;
        }
    }

    public void initialization()
    {

    }

    public void Update()
    {

        if (bullets != null)
        {

            foreach (Bullet bullet in bullets)
            {

                if (bullet != null)
                {
                    bullet.UpdateBullet();
                }
            }
        }
    }

    public void CreateBullet(Vector2 SpawnLocation, Vector2 rotation, int id)
    {

        GameObject bulletObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Bullet/Sphere")); //Create Bullet
        if (!bulletObject)
        {
            Debug.LogError("Didn't find enemy resources at Prefabs/Bullet/Sphere");
            return;
        }
        bulletObject.transform.position = SpawnLocation;
        bulletObject.transform.Rotate(new Vector3(rotation.x, 0, rotation.y));
        bulletObject.layer = nbBaseLayer + id;


        Bullet b = bulletObject.GetComponent<Bullet>();
        b.initialization();

        bullets.Add(b);


    }
}
