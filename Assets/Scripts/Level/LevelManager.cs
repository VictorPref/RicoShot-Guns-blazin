using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager {

    public Level currentLevel;
    private GameObject levelObject;
    GameObject[] listeLevel;
    public int lvlNumber;
    int currentlvl = -1;

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

        listeLevel = Resources.LoadAll("Prefabs/Levels", typeof(GameObject)).Cast<GameObject>().ToArray(); 
        levelObject = GameObject.Instantiate<GameObject>(listeLevel[RandomLevel()]); //Create level
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

        levelObject = GameObject.Instantiate<GameObject>(listeLevel[RandomLevel() ]);

        //   levelObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Levels/Level" + lvlNumber)); //Create level
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

    int RandomLevel()
    {
        int random = Random.Range(0,listeLevel.Length);

        while(random == currentlvl)
        {
            random = Random.Range(0, listeLevel.Length);
        }
        currentlvl = random;
        return random;
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
