using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    private GameObject levelPrefab; //this is the level's preset to which it refers to when initializing
    private Dictionary<int, GameObject> obstacleList;

    public void Initialize(GameObject targetPreset) {
        levelPrefab = targetPreset;
        obstacleList = new Dictionary<int, GameObject>();
    }

    public void UpdateLevel()
    {
     
    }


}
