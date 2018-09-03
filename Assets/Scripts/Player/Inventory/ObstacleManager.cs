using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager {
    int nbObstalceMax = 3;
    int obstacleNum = 0;
    List<Obstacle> obstacles = new List<Obstacle>();
    GameObject Gameobstacle;
    Obstacle obstacle;
     public int listElement = 0;
    Obstacle selectedObstacle;
    public int id_player;
    int nbBaseLayer = 7;

    public void CreateObstacle()
    {
        //Create the obstacle
        Gameobstacle = GameObject.Instantiate(InventoryManager.inventory.getObstacle(obstacleNum));

        //Get the script on the prefab
        obstacle = Gameobstacle.GetComponent<Obstacle>();
    }
    public void Update(Vector3 pos,float rotation)
    {
        //Move the obstacle
        obstacle.UpdateObstacle(pos,rotation);

        //Loop to check if one of the obstacle is dead
        for(int i = obstacles.Count-1;i >= 0;i--)
        {
            if (obstacles[i].destroy == true)
            {
                Obstacle temp = obstacles[i];
                obstacles.Remove(temp);  
            }
        }

    }
    

    //Change the obstacle the player is gonna put on the field. Change up
    public void changeObstaclePlus()
    {
        obstacleNum++;

        //check if the number for the obstacle selected is bigger than the max array of obstacle 
        if(obstacleNum >= InventoryManager.inventory.getTaille())
        {
            //set the number for the obstacle selected to 0
            obstacleNum = 0;
        }
        GameObject.Destroy(Gameobstacle);
        CreateObstacle();
    }

    //Change the obstacle the player is gonna put on the field. Change down
    public void changeObstacleMoins()
    {
        obstacleNum--;
        //check if the number for the obstacle selected is less than 0 
        if (obstacleNum <= -1)
        {
            //set the number for the obstacle selected to the array size
            obstacleNum = InventoryManager.inventory.getTaille()-1;
        }
        GameObject.Destroy(Gameobstacle);
        CreateObstacle();

    }

    //Set the obstacle on the field and add it to the list of obstacle on the field
    public void setObstacle()
    {
        //Check if the lists of obstacle is less than the number max of obstacle on the level
       if(obstacles.Count < nbObstalceMax)
        {
            //Add the obstacle to the list
            Gameobstacle.layer = nbBaseLayer + id_player;
            obstacles.Add(obstacle);
            obstacle.placer = true;

            CreateObstacle();
        }
    }

    //Detele obstacle
    public void DeleteObstacle()
    {
        GameObject.Destroy(Gameobstacle);
    }

    //Select Obstacle from the existing obstacle
    public void SelectFromList()
    {
        selectedObstacle = obstacles[listElement];
    }

    //Delete the selected obstacle from the list
    public void DeleteSelectedObstacle()
    {
        SelectFromList();
        obstacles.Remove(selectedObstacle);
        selectedObstacle.DestroyObstacle();
        SelectedObstaclePlus();
       
    }

    //Change the selected obstacle plus one in the list
    public void SelectedObstaclePlus()
    {
        listElement++;
        if(listElement > obstacles.Count - 1)
        {
            listElement = 0;
        }
    }
    //Change the selected obstacle minus one in the list
    public void SelectedObstacleMoins()
    {
        listElement--;
        if (listElement < 0)
        {
            listElement = obstacles.Count - 1;
        }
    }

}
