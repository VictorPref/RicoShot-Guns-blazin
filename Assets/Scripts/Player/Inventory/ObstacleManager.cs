using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager
{
    public int nbObstacleMax = 3;
    int obstacleNum = 0;
    List<Obstacle> obstacles = new List<Obstacle>();
    GameObject gameObject;
    MeshRenderer[] meshRenderers;
    List<Material> materials;
    Obstacle obstacle;
    Obstacle selectedObstacle;
    int nbBaseLayer = 7;
    public int listElement = 0;
    public int id_player;

    float halfAlpha = 0.5f;
    float fullAlpha = 1f;

    public void CreateObstacle()
    {
        if (obstacles != null)
        {
            //Create the obstacle
            gameObject = GameObject.Instantiate(InventoryManager.inventory.getObstacle(obstacleNum));
            materials = new List<Material>();



            //Get the script on the prefab
            obstacle = gameObject.GetComponent<Obstacle>();
            obstacle.SetMaterial();
            materials = obstacle.materials;
            //obstacle.material = materials;

            //add color corresponding to playerId  

        if (PlayerManager.Instance.GetPlayers()[id_player - 1] != null){
            foreach (Material mat in materials)
            {
                mat.color = PlayerManager.Instance.GetPlayers()[id_player - 1].playerColor;
            }
            }
        }
    }
    public void Update(Vector3 pos, float rotation)
    {

        if (obstacles != null && obstacle != null)
        {
            //Move the obstacle
            obstacle.UpdateObstacle(pos, rotation);
        }



        //Loop to check if one of the obstacle is dead
        for (int i = obstacles.Count - 1; i >= 0; i--)
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
        if (obstacleNum >= InventoryManager.inventory.getLength())
        {
            //set the number for the obstacle selected to 0
            obstacleNum = 0;
        }
        GameObject.Destroy(gameObject);
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
            obstacleNum = InventoryManager.inventory.getLength() - 1;
        }
        GameObject.Destroy(gameObject);
        CreateObstacle();
    }

    //Set the obstacle on the field and add it to the list of obstacle on the field
    public void setObstacle()
    {
        //Check if the lists of obstacle is less than the number max of obstacle on the level

        if (obstacles.Count < nbObstacleMax)
        {
            //Add the obstacle to the list
            gameObject.layer = nbBaseLayer + id_player;

            obstacles.Add(obstacle);
            obstacle.isFixed = true;

            CreateObstacle();
        }
    }

    //Detele obstacle
    public void DeleteObstacle()
    {
        GameObject.Destroy(gameObject);
    }

    //Select Obstacle from the existing obstacle
    public void SelectFromList()
    {
        selectedObstacle = obstacles[listElement];
        alphaDown();
    }

    public void alphaDown()
    {
        float a = halfAlpha;
        foreach(Material m in selectedObstacle.materials)
        {
            m.color = new Color(m.color.r, m.color.g, m.color.b, a);
        }
       
    }

    public void alphaUp()
    {
        float a = fullAlpha;

        foreach (Material m in selectedObstacle.materials)
        {
            m.color = new Color(m.color.r, m.color.g, m.color.b, a);
        }

    }

    //Delete the selected obstacle from the list
    public void DeleteSelectedObstacle()
    {
        if (obstacles.Count > 0)
        {

            SelectFromList();
            obstacles.Remove(selectedObstacle);
            selectedObstacle.DestroyObstacle();
            SelectedObstacleForward();
        }
    }

    //Change the selected obstacle plus one in the list
    public void SelectedObstacleForward()
    {
        alphaUp();
        listElement++;
        if (listElement > obstacles.Count - 1)
        {
            listElement = 0;
        }
        SelectFromList();
        alphaDown();



    }
    //Change the selected obstacle minus one in the list
    public void SelectedObstacleBack()
    {
        alphaUp();
        listElement--;
        if (listElement < 0)
        {
            listElement = obstacles.Count - 1;
        }
        SelectFromList();
        alphaDown();
    }

    public void ResetObstacleList()
    {
        foreach (Obstacle obs in obstacles)
        {
            //GameObject.Destroy(obs.gameObject);
            obs.DestroyObstacle();
        }
        obstacles.Clear();
    }

    //Get the nb of obstacle in the list
    public int getNbObstacles()
    {
        return obstacles.Count;
    }

}
