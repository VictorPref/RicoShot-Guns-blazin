using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    int vie = 3;
   public bool placer = false;
    public bool destroy = false;

	public void Initialization(int obstacleNum)
    {
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (placer)
            vie--;
        if(vie <= 0)
        {
            destroy = true;
            Destroy(gameObject);
        }
    }

    public void UpdateObstacle (Vector3 pos, float rotation) {
            transform.position += pos;
        transform.Rotate(new Vector3(0, 0, -rotation));
	}
    public void DestroyObstacle()
    {
        Destroy(gameObject);
    }
  
    
}
