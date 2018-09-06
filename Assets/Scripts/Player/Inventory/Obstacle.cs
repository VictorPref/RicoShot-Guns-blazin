using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    int lifespan = 3;
    public bool isFixed = false;
    public bool destroy = false;
    public List<Material> materials;
    MeshRenderer[] meshRenderers;
    public ObstacleManager manager;

    /// <summary>
    /// Decreases obstacle lifespan
    /// </summary>
    public void ReduceLifespan()
    {
        if (isFixed)
            lifespan--;
        if (lifespan <= 0)
        {
            destroy = true;
            DestroyObstacle();
        }
    }

    /// <summary>
    /// Fetchs all mesh renderers in the obstacle's prefab then fills a list of corresponding materials
    /// </summary>
    public void SetMaterialList()
    {
        meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in meshRenderers)
        {
            materials.Add(mr.material);
        }
    }

    /// <summary>
    /// Obstacle update for movement and deletion during Phase 1
    /// </summary>
    public void UpdateObstacle(Vector3 pos, float rotation)
    {
        transform.position += pos;
        transform.Rotate(new Vector3(0, 0, -rotation));
    }

    /// <summary>
    /// Destroys an obstacle's Game Object
    /// </summary>
    public void DestroyObstacle()
    {
        Destroy(gameObject, 0.05f);
    }
}
