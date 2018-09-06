using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager
{
    private GameObject levelObject;
    public Level currentLevel;
    public int lvlNumber;
    GameObject[] levelPrefabsList;
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

    /// <summary>
    /// Initial level generation, also initializes the level prefabs List
    /// </summary>
    public void GenerateLevel(int lvlNumber)
    {
        levelPrefabsList = Resources.LoadAll("Prefabs/Levels", typeof(GameObject)).Cast<GameObject>().ToArray();
        levelObject = GameObject.Instantiate<GameObject>(levelPrefabsList[RandomLevel()]); //Create random level
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

    /// <summary>
    /// Given the number of a level (filename format: "LevelX" where X is the level number), destroys previous level gameObject and instantiates LevelX
    /// </summary>
    public void ResetLevel(int lvlNumber)
    {
        GameObject.Destroy(levelObject);

        levelObject = GameObject.Instantiate<GameObject>(levelPrefabsList[RandomLevel()]);
        currentLevel = levelObject.GetComponent<Level>();
        currentLevel.Initialize();

        PlayerManager.Instance.ActivatePlayers();
        if (!levelObject)
        {
            Debug.LogError("Couldn't find any resources at Prefabs/Levels/Level" + lvlNumber);
            return;
        }
        levelObject.transform.position = new Vector2();
        PlayerManager.Instance.InitializePlayersPositions();
        currentLevel = levelObject.GetComponent<Level>();
    }

    /// <summary>
    /// Returns a random number chosen across the range of available level prefabs
    /// </summary>
    int RandomLevel()
    {
        int random = Random.Range(0, levelPrefabsList.Length);

        while (random == currentlvl)
        {
            random = Random.Range(0, levelPrefabsList.Length);
        }
        currentlvl = random;
        return random;
    }

    /// <summary>
    /// Returns a list of each player's spawn position according to the level prefab
    /// </summary>
    public List<Transform> RetrievePlayersSpawnPositions()
    {
        int playerCount = PlayerManager.Instance.GetPlayers().Count;
        List<Transform> resultList = currentLevel.playerPositions;

        return resultList;
    }
}
