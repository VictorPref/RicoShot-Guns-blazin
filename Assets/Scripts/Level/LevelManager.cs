using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager {

    public Level currentLevel;
    private GameObject levelObject;
    public int lvlNumber;

    #region Singleton
    private LevelManager()
    {
    }

    public static LevelManager Instance { get { return Nested.instance; } }

    private class Nested
    {
        static Nested()
        {
        }

        internal static readonly LevelManager instance = new LevelManager();
    }
    #endregion

    public void GenerateLevel(int lvlNumber) { 
        levelObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Levels/Level" + lvlNumber)); //Create level
        if (!levelObject)
        {
            Debug.LogError("Didn't find any resources at Prefabs/Levels/Level" + lvlNumber);
            return;
        }
        this.lvlNumber = lvlNumber;
        levelObject.transform.position = new Vector2();

        currentLevel = levelObject.GetComponent<Level>();
        currentLevel.Initialize();


    }

    public void ResetLevel(int lvlNumber) {
        GameObject.Destroy(levelObject);
        
        levelObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Levels/Level" + lvlNumber)); //Create level
        currentLevel = levelObject.GetComponent<Level>();
        currentLevel.Initialize();

        PlayerManager.Instance.ActivatePlayers();
        if (!levelObject)
        {
            Debug.LogError("Didn't find any resources at Prefabs/Levels/Level" + lvlNumber);
            return;
        }
        levelObject.transform.position = new Vector2();
        PlayerManager.Instance.InitializePlayersPositions();
        currentLevel = levelObject.GetComponent<Level>();
    }

    public void Update() {
       
    }

    public List<Transform> RetrievePlayersSpawnPositions() {
        int playerCount = PlayerManager.Instance.GetPlayers().Count;
        int i = 1;
        List<Transform> resultList = currentLevel.playerPositions;

        //while (i <= playerCount) {
            // resultList.Add(GameObject.FindGameObjectWithTag("Player" + i).transform.position);
        //    resultList.Add(currentLeve);
        //    i++;
       // }

        return resultList;
    }
}
