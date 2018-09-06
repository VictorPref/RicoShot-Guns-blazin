using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BulletManager
{
    private static BulletManager instance = null;

    List<Bullet> bullets = new List<Bullet>();
    int nbBaseLayers = 7; //the number of default layers in unity, so we can jump to the ones we created by using playerId

    #region Singleton
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
    #endregion

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

    public void CreateBullet(Vector2 spawnLocation, Vector2 rotation, int id)
    {
        //Create Bullet object
        GameObject bulletObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Bullet/Bullet"), spawnLocation,Quaternion.Euler(0,0,rotation.y));
       
        if (!bulletObject)
        {
            Debug.LogError("Couldn't find enemy resources at Prefabs/Bullet/Bullet");
            return;
        }
        //Bullet takes the player's gun's transform and rotation for its position
        //bulletObject.transform.position = spawnLocation;
        //bulletObject.transform.Rotate(new Vector3(0, 0, rotation.y));
        //bullet is assigned player's object's (obstacles and bullets) layer
        bulletObject.layer = nbBaseLayers + id;
        Bullet b = bulletObject.GetComponent<Bullet>();
        b.initialization();
        b.ActivateTrailRenderer(id);
        bullets.Add(b);
    }

    public void DeleteAllBullets()
    {
        foreach (Bullet b in bullets)
        {
            //GameObject.Destroy(b.gameObject);
            if(b)
            b.KillBullet();
        }
        bullets = new List<Bullet>();
    }
}
