using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    readonly int INPUTCHECKDELAY = 2;
    private List<Player> players;
    private List<Vector2> playersPositions = null;
    string[] connectedControllers;
    private static PlayerManager instance = null;

    #region Singleton
    private PlayerManager()
    {
    }

    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerManager();
            }
            return instance;
        }
    }
    #endregion

    public void Initialize()
    {
        connectedControllers = Input.GetJoystickNames();
        Debug.Log("Joystick names :" + connectedControllers[0]);
        players = new List<Player>();
        CreatePlayers();
        InitializePlayersPositions();
    }

    public void InitializePlayersPositions()
    {
        playersPositions = LevelManager.Instance.RetrievePlayersSpawnPositions();
        int i = 0;
        foreach (Player p in players)
        {
            p.transform.position = playersPositions[i];
            i++;
        }
    }

    public void Update()
    {

        if (players != null)
        {
            foreach (Player p in players)
            {
                if (p != null)
                {
                    p.UpdatePlayer();
                }
            }
        }
    }

    private void CreatePlayers()
    {
        if (connectedControllers.Length > 0)
        {
            for (int i = 0; i < connectedControllers.Length; i++)
            {
                if (!string.IsNullOrEmpty(connectedControllers[i]))
                {
                    GameObject playerObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Player/PlayerTest")); //Create an instance of the player
                    playerObject.transform.position = new Vector2();
                    Player newPlayer = playerObject.GetComponent<Player>();
                    newPlayer.setPlayerId(i + 1); //+1 because id should not be 0
                    players.Add(newPlayer);
                }
            }

        }

    }

    void RemovePlayer()
    {
        for (int i = 0; i < connectedControllers.Length - 1; i++)
        {
            if (string.IsNullOrEmpty(connectedControllers[i]))
            {
                players[i] = null;
            }
        }
    }

    public List<Player> GetPlayers()
    {
        return players;
    }

    IEnumerator UpdateConnectedControllers()
    {
        //check for controllers plugged in/out
        //update player list accordingly
        yield return new WaitForSeconds(INPUTCHECKDELAY);
    }

    public void DeleteManager()
    {

    }


}
