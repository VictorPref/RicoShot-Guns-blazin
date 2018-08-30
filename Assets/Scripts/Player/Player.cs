using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerColor { BLUE, RED, GREEN, YELLOW };
public class Player : MonoBehaviour {

    InputManager inputManager;
    int playerId;
    float timeShoot;
    float timeBetweenShot = 0.3f;
    bool isTriggerDown = false;
    float rotationSpeed = -50;

    public void setPlayerId(int playerId)
    {
        this.playerId = playerId;
        inputManager = new InputManager();
        timeShoot = Time.time;
    }


    public void UpdatePlayer()
    {
        //Get Package of Controller Input
        InputManager.InputPkg inputPkg = inputManager.GetKeysInput(playerId);
       
        //Go to Phase 2 if input rb is press
        if(inputPkg.rb)
        {
            UpdatePhase2(inputPkg);
          
        }
        else
        {
            UpdatePhase1(inputPkg);

        }
        
    }

     void UpdatePhase1(InputManager.InputPkg inputPkg) { }

     void UpdatePhase2(InputManager.InputPkg inputPkg) {

        //Rotate the Player and send Input information about the left Joystick
        RotatePlayer(inputPkg.leftDir);


        //Player Shoot and send rt input information
        Shoot(inputPkg.rt);
    }


     void RotatePlayer(Vector2 dir)
    {

        //Rotate player with left Joystick input informaton and multiply by a rotation speed and deltaTime
            transform.Rotate(new Vector3(0, 0, dir.x * rotationSpeed * Time.deltaTime));
        
    }
    void Shoot(bool shoot)
    {

        //Can shoot if the input is true
        if (shoot )
        {
            Debug.Log("SHOOT: "+playerId);      
        }
        
    }

    public void PlayerFixedUpdate()
    {

    }

    public void PlayerDies()
    {

    }

 
}
