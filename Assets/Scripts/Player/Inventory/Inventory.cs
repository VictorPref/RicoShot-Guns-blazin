using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory  {

    GameObject[] obstaclePrefabList;

    public void Initialize()
    {
        obstaclePrefabList = Resources.LoadAll("Prefabs/Player/Inventory", typeof(GameObject)).Cast<GameObject>().ToArray();
    }

    public int GetLength()
    {
        return obstaclePrefabList.Length;
    }

    public GameObject GetObstacle(int pos)
    {
        return obstaclePrefabList[pos];
    }

}
