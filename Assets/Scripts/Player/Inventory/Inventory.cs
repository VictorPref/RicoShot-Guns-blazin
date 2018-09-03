using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory  {

    GameObject[] listes;

    public void initialization()
    {
        listes = Resources.LoadAll("Prefabs/Player/Inventory", typeof(GameObject)) .Cast<GameObject>() .ToArray();

    }

    public int getTaille()
    {
        return listes.Length;
    }
    public GameObject getObstacle(int pos)
    {
        return listes[pos];
    }

}
