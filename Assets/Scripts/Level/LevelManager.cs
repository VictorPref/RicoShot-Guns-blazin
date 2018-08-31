using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager {

    private Level currentLevel;

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
        Debug.Log("in generateLevel()");
        GameObject levelObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Levels/Level" + lvlNumber)); //Create level
        if (!levelObject)
        {
            Debug.LogError("Didn't find any resources at Prefabs/Levels/Level" + lvlNumber);
            return;
        }
        levelObject.transform.position = new Vector2();

        currentLevel = levelObject.GetComponent<Level>();
    }

    public void Update() {
       
    }

    public List<Vector2> RetrievePlayersPositions() {
        int playerCount = PlayerManager.Instance.GetPlayers().Count;
        int i = 1;
        List<Vector2> resultList = new List<Vector2>();

        while (i <= playerCount) {
            resultList.Add(GameObject.FindGameObjectWithTag("Player" + i).transform.position);
            i++;
        }

        return resultList;
    }
}
