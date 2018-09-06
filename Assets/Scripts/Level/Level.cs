using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    private GameObject levelPrefab; //this is the level's preset to which it refers to when initializing
    private Dictionary<int, GameObject> obstacleList;
    public GameObject p1, p2;
    public List<Transform> playerPositions;

    public void Initialize() {
        // levelPrefab = targetPreset;
        //obstacleList = new Dictionary<int, GameObject>();
        playerPositions = new List<Transform>();
        playerPositions.Add(p1.transform);
        playerPositions.Add(p2.transform);

    }

    public void UpdateLevel()
    {
     
    }


}
