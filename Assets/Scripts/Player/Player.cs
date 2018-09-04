using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerColor { BLUE, RED, GREEN, YELLOW };
enum ObstacleType { ObsMSmall1, ObsMSmall2, ObsWSmall1, ObsWSmall2 };

public class Player : MonoBehaviour {

    readonly int CHARGE_MAX = 6;
    readonly int CHARGE_MIN = 0;

    public GameObject gun;
    InputManager inputManager;
    int playerId;
    float timeShoot;
    float rotationSpeed = -50;

    int chargeur;


    InventoryManager inventory;

    bool inPhase1 = false;
    float obstacleMovementSpeed = 0.2f;
    bool isObstacleFixed = false;

    ObstacleManager obstacleManager;
    bool isLeftTriggerPressed = false;
    bool bButton = false;



    public void setPlayerId(int playerId)
    {
        this.playerId = playerId;
        inputManager = new InputManager();
        timeShoot = Time.time;
        chargeur = CHARGE_MAX;
        inventory = InventoryManager.Instance;
        obstacleManager = new ObstacleManager();
        obstacleManager.id_player = playerId;
    }


    public void UpdatePlayer()
    {
        //Get Package of Controller Input
        InputManager.InputPkg inputPkg = inputManager.GetKeysInput(playerId);
       
        //Go to Phase 2 if input rb is press
        if(inputPkg.rb)
        {
            UpdatePhase2(inputPkg);
            obstacleManager.DeleteObstacle();

        }
        else
        {
            UpdatePhase1(inputPkg);
        }
        
    }

     void UpdatePhase1(InputManager.InputPkg inputPkg) {

        //If in Phase 1 create Object 1 time
        if(inPhase1 == false)
        {
            obstacleManager.CreateObstacle();
        }
        else
        {
            //Update the obstacleManager with the left joystick and right joystick
            obstacleManager.Update(new Vector3(inputPkg.leftDir.x, inputPkg.leftDir.y, 0) * obstacleMovementSpeed,inputPkg.rightDir.x);
            
            //if button A pressed one obstacle is placed and the player can't spam 
            if (inputPkg.A && !isObstacleFixed)
            {
                obstacleManager.setObstacle();

                isObstacleFixed = true;
            }
            //if button A is not pressed unlock the possibility of placing an obstacle
            else if (!inputPkg.A)
            {
                isObstacleFixed = false;
            }

            // if Y is pressed player can go trought the list of obstacle on the field and delete them
            if (inputPkg.Y)
            {
                Debug.Log("Y Obstacle");
                
                //Rt button pressed go up in the list of obstacle place on the field
                if (inputPkg.rt)
                {
                    obstacleManager.SelectedObstacleForward();
                }
                //Lt button pressed go down in the list of obstacle place on the field
                if (inputPkg.lt)
                {
                    obstacleManager.SelectedObstacleBack();
                }
                //B button pressed delete obstacle on the field and lock the possibility once B is pressed
                if (inputPkg.B && !bButton )
                {
                    bButton = true;
                    obstacleManager.DeleteSelectedObstacle();
                }
                //Unlock possibility of deleting an obstacle once B is not pressed
                else if (!inputPkg.B)
                {
                    bButton = false;
                }

            }
             //Change between the type of obstacle 
            else if (inputPkg.rt)
            {
                obstacleManager.changeObstaclePlus();
            }
            //Change between the type of obstacle
            //Weird behaviour with the LT button that the RT button doesn't have so we need to lock the LT button once its pressed
            else if (inputPkg.lt && !isLeftTriggerPressed)
            {
                obstacleManager.changeObstacleMoins();
                isLeftTriggerPressed = true;
            }
            // unlock the LT button once its not pressed
            if (!inputPkg.lt)
            {
                isLeftTriggerPressed = false;
            }
            
        }
        inPhase1 = true;

    }

   

     void UpdatePhase2(InputManager.InputPkg inputPkg) {
        //In phase 2
        inPhase1 = false;


        //Rotate the Player and send Input information about the left Joystick
        RotatePlayer(inputPkg.leftDir);


        //Player Shoot and send rt input information
        Shoot(inputPkg.rt);

        //Player can Reload
        Reload(inputPkg.X);
    }


     void RotatePlayer(Vector2 dir)
    {

        //Rotate player with left Joystick input informaton and multiply by a rotation speed and deltaTime
            transform.Rotate(new Vector3(0, 0, dir.x * rotationSpeed * Time.deltaTime));
        
    }
    void Shoot(bool shoot)
    {

        //Can shoot if the input is true
        if (shoot && chargeur > CHARGE_MIN)
        {
            BulletManager.Instance.CreateBullet(gun.transform.position+gun.transform.right ,new Vector2(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z),playerId);
            chargeur--;  
        }
        
    }
    void Reload(bool reload)
    {
        if (reload)
            chargeur = CHARGE_MAX;
    }

    public void PlayerFixedUpdate()
    {

    }

    public void PlayerDies()
    {

    }

 
}
