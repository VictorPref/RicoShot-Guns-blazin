using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    readonly int INPUTCHECKDELAY = 2;
    private List<Player> players;
    private List<Transform> playersPositions = null;
    string[] connectedControllers;
    private static PlayerManager instance = null;
    public int playersAlive = 0;

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
        players = new List<Player>();
        CreatePlayers();
        InitializePlayersPositions();
        playersAlive = players.Count;
    }

    public void InitializePlayersPositions()
    {
        playersPositions = LevelManager.Instance.RetrievePlayersSpawnPositions();
        int i = 0;
        foreach (Player p in players)
        {
            p.transform.position = playersPositions[i].position;
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
                    //Create an instance of the player
                    GameObject playerObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Player/PlayerTest")); 
                    playerObject.transform.position = new Vector2();
                    Player newPlayer = playerObject.GetComponent<Player>();
                    newPlayer.Initialize(i + 1); //+1 because id should not be 0
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
        while (true) {

            yield return new WaitForSeconds(INPUTCHECKDELAY);
        }
        
    }

    public void IsLastManStanding() {
        if (playersAlive <= 1) {
            Player winner = null;
            foreach (Player p in players) { if (p.isAlive) winner = p; }
            MatchManager.Instance.PlayerWinsRound(winner);
        }
    }

    public void ActivatePlayers() {
        foreach (Player p in players) {

            p.ResetPlayer();
        }
    }

    public void DeleteManager()
    {

    }


}
