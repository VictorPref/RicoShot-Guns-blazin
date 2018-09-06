using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager
{
    public int nbObstacleMax = 3;
    public int listElement = 0;
    public int id_player;
    int obstacleNum = 0;
    List<Obstacle> obstacles = new List<Obstacle>();
    GameObject gameObject;
    MeshRenderer[] meshRenderers;
    List<Material> materials;
    Obstacle obstacle;
    Obstacle selectedObstacle;
    int nbBaseLayer = 7; //the number of default layers
    float halfAlpha = 0.5f;
    float fullAlpha = 1f;

    /// <summary>
    /// Instantiates an obstacle from the preloaded list of assets in Inventory
    /// </summary>
    public void CreateObstacle()
    {
        if (obstacles != null)
        {
            //Create the obstacle
            gameObject = GameObject.Instantiate(InventoryManager.inventory.GetObstacle(obstacleNum));
            materials = new List<Material>();

            //Get the script on the prefab
            obstacle = gameObject.GetComponent<Obstacle>();
            obstacle.manager = this;

            //push the gameobjects materials from the mesh rendrer into its materials List member
            obstacle.SetMaterialList();
            materials = obstacle.materials;

            //add color corresponding to playerId  
            if (PlayerManager.Instance.GetPlayers()[id_player - 1] != null)
            {
                foreach (Material mat in materials)
                {
                    mat.color = PlayerManager.Instance.GetPlayers()[id_player - 1].playerColor;
                }
            }
        }
    }

    /// <summary>
    /// Update called in phase 1 of player for movement and count
    /// </summary>
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

    /// <summary>
    /// Update called in phase 2 of player for count
    /// </summary>
    public void UpdateObstacleCount() {
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

    /// <summary>
    /// Change the obstacle the player is going to put on the field. Change up
    /// </summary>
    public void changeObstaclePlus()
    {
        obstacleNum++;

        //check if the number for the obstacle selected is bigger than the max array of obstacle 
        if (obstacleNum >= InventoryManager.inventory.GetLength())
        {
            //set the number for the obstacle selected to 0
            obstacleNum = 0;
        }
        GameObject.Destroy(gameObject);
        CreateObstacle();
    }

    /// <summary>
    /// Change the obstacle the player is going to put on the field. Change down
    /// </summary>
    public void changeObstacleMoins()
    {
        obstacleNum--;
        //check if the number for the obstacle selected is less than 0 
        if (obstacleNum <= -1)
        {
            //set the number for the obstacle selected to the array size
            obstacleNum = InventoryManager.inventory.GetLength() - 1;
        }
        GameObject.Destroy(gameObject);
        CreateObstacle();
    }

    /// <summary>
    /// Set the obstacle on the field and add it to the list of obstacles on the field
    /// </summary>
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

    /// <summary>
    /// Deletes an obstacle's gameObject
    /// </summary>
    public void DeleteObstacle()
    {
        GameObject.Destroy(gameObject);
    }

    /// <summary>
    /// Select Obstacle from the existing obstacle
    /// </summary>
    public void SelectFromList()
    {
        selectedObstacle = obstacles[listElement];
        alphaDown(); //when a fixed obstacle is selected, make it's material transluscent
    }

    /// <summary>
    /// Reduce selected obstacle's alpha
    /// </summary>
    public void alphaDown()
    {
        float a = halfAlpha;
        foreach (Material m in selectedObstacle.materials)
        {
            m.color = new Color(m.color.r, m.color.g, m.color.b, a);
        }

    }

    /// <summary>
    /// Increment selected obstacle's alpha
    /// </summary>
    public void alphaUp()
    {
        float a = fullAlpha;

        if (obstacles.Count > 0)
        {
            foreach (Material m in selectedObstacle.materials)
            {
                m.color = new Color(m.color.r, m.color.g, m.color.b, a);
            }
        }

    }

    /// <summary>
    /// Delete the selected obstacle from the list
    /// </summary>
    public void DeleteSelectedObstacle()
    {
        if (obstacles.Count > 0)
        {
            SelectFromList();
            obstacles.Remove(selectedObstacle);
            selectedObstacle.DestroyObstacle();
        }
        if (obstacles.Count > 0)
        {
            SelectedObstacleForward();
        }

    }

    /// <summary>
    /// Change the selected obstacle plus one in the list
    /// </summary>
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

    /// <summary>
    /// Change the selected obstacle minus one in the list
    /// </summary>
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

    /// <summary>
    /// Clears the obstacles list
    /// </summary>
    public void ResetObstacleList()
    {
        foreach (Obstacle obs in obstacles)
        {
            obs.DestroyObstacle();
        }
        obstacles.Clear();
    }

    /// <summary>
    /// Returns the number of obstacles in its list
    /// </summary>
    public int getNbObstacles()
    {
        return obstacles.Count;
    }
}
