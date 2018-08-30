using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    readonly int INPUTCHECKDELAY = 2;

    List<Player> players;
    string[] connectedControllers;

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

    void CreatePlayers()
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
        for (int i = 0; i < connectedControllers.Length - 1; i++) {
            if (string.IsNullOrEmpty(connectedControllers[i])) {
                players[i] = null;
            }
        }
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
