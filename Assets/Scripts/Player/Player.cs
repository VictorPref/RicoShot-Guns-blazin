using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerColor { BLUE, RED, GREEN, YELLOW };
public class Player : MonoBehaviour {

    InputManager inputManager;
    int playerId;

    public void setPlayerId(int playerId)
    {
        this.playerId = playerId;

    }
   
    public void PlayerCreated()
    {
        inputManager = new InputManager();

    }

    public void UpdatePlayer()
    {
        InputManager.InputPkg inputPkg = inputManager.GetKeysInput();
        RotatePlayer(inputPkg.dirPressed);
        
    }

    public void UpdatePhase1(InputManager.InputPkg inputPkg) { }

    public void UpdatePhase2(InputManager.InputPkg inputPkg) { }


    public void RotatePlayer(Vector2 dir)
    {
        if(dir.x != 0)
        {
            transform.Rotate(new Vector3(0, 0, dir.x * 2));
        }
    }

    public void PlayerFixedUpdate()
    {

    }

    public void PlayerDies()
    {

    }

 
}
