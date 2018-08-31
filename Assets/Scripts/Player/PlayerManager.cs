using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    readonly int INPUTCHECKDELAY = 2;
    private List<Player> players;
    string[] connectedControllers;

    #region Singleton
    private PlayerManager()
    {
    }

    public static PlayerManager Instance { get { return Nested.instance; } }

    private class Nested
    {
        static Nested()
        {
        }

        internal static readonly PlayerManager instance = new PlayerManager();
    }
    #endregion

    public void Initialize()
    {
        connectedControllers = Input.GetJoystickNames();
        Debug.Log("Joystick names :" + connectedControllers[0]);

        players = new List<Player>();
        CreatePlayers();
        
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
                    newPlayer.setPlayerId(i + 1);
                    players.Add(newPlayer);
                    Debug.Log("Player created");
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
