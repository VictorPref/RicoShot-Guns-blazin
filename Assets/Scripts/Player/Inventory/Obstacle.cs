﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    int lifespan = 3;
    public bool isFixed = false;
    public bool destroy = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFixed)
            lifespan--;
        if (lifespan <= 0)
        {
            destroy = true;
            DestroyObstacle();
        }
    }

    public void UpdateObstacle(Vector3 pos, float rotation)
    {
        transform.position += pos;
        transform.Rotate(new Vector3(0, 0, -rotation));
    }
    public void DestroyObstacle()
    {
        Destroy(gameObject,0.05f);
    }


}
