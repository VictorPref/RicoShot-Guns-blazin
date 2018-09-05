using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    readonly int MAX_BULLETS = 6;
    readonly int MIN_BULLETS = 0;

    InputManager inputManager;
    int playerId;
    float rotationSpeed = -50;
    int bulletsRemaining;
    public GameObject gun;

    InventoryManager inventory;
    bool inPhase1 = false;
    float obstacleMovementSpeed = 0.2f;
    bool isObstacleFixed = false;
    public Color playerColor;

    ObstacleManager obstacleManager;
    bool isLeftTriggerPressed = false;
    bool bButton = false;

    public int roundsWon = 0;
    public bool isAlive = true;


    public void Initialize(int playerId)
    {
        this.playerId = playerId;
        inputManager = new InputManager();
        bulletsRemaining = MAX_BULLETS;
        inventory = InventoryManager.Instance;
        obstacleManager = new ObstacleManager();
        obstacleManager.id_player = playerId;
        SetPlayerColor(playerId);
        gameObject.SetActive(true);
    }

    public void ResetPlayer() {
        bulletsRemaining = MAX_BULLETS;
        gameObject.SetActive(true);
        isAlive = true;
        obstacleManager.ResetObstacleList();
    }

    public int GetPlayerId()
    {
        return playerId;
    }

    public void SetPlayerColor(int playerId)
    {
        switch (playerId)
        {
            case 1:
                playerColor = Color.blue;
                break;
            case 2:
                playerColor = Color.red;
                break;
            case 3:
                playerColor = Color.yellow;
                break;
            case 4:
                playerColor = Color.green;
                break;
            default:
                break;
        }
    }


    public void UpdatePlayer()
    {
        if (isAlive)
        {
            //Get Package of Controller Input
            InputManager.InputPkg inputPkg = inputManager.GetKeysInput(playerId);

            //Go to Phase 2 if input rb is press
            if (inputPkg.X)
            {
                UpdatePhase2(inputPkg);
                obstacleManager.DeleteObstacle();

            }
            else
            {
                UpdatePhase1(inputPkg);
            }
        }

    }

    void UpdatePhase1(InputManager.InputPkg inputPkg)
    {

        //If in Phase 1 create Object 1 time
        if (inPhase1 == false)
        {
            obstacleManager.CreateObstacle();
        }
        else
        {
            //Update the obstacleManager with the left joystick and right joystick
            obstacleManager.Update(new Vector3(inputPkg.leftDir.x, inputPkg.leftDir.y, 0) * obstacleMovementSpeed, inputPkg.rightDir.x);
            Debug.Log("input a" + inputPkg.A);
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
                //Rt button pressed go up in the list of obstacle place on the field
                if (inputPkg.lt > 0)
                {
                    obstacleManager.SelectedObstacleForward();
                }
                //Lt button pressed go down in the list of obstacle place on the field
                if (inputPkg.lt < 0)
                {
                    obstacleManager.SelectedObstacleBack();
                }
                //B button pressed delete obstacle on the field and lock the possibility once B is pressed
                if (inputPkg.B && !bButton)
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
            else if (inputPkg.lt > 0)
            {
                obstacleManager.changeObstaclePlus();
            }
            //Change between the type of obstacle
            //Weird behaviour with the LT button that the RT button doesn't have so we need to lock the LT button once its pressed
            else if (inputPkg.lt < 0)// !isLeftTriggerPressed)
            {
                obstacleManager.changeObstacleMoins();
                isLeftTriggerPressed = true;
            }
            // unlock the LT button once its not pressed
            if (inputPkg.lt == 0)
            {
                isLeftTriggerPressed = false;
            }

        }
        inPhase1 = true;

    }



    void UpdatePhase2(InputManager.InputPkg inputPkg)
    {
        //In phase 2
        inPhase1 = false;


        //Rotate the Player and send Input information about the left Joystick
        RotatePlayer(inputPkg.leftDir);


        //Player Shoot and send rt input information
        Shoot(inputPkg.A);

        //Player can Reload
        Reload(inputPkg.B);
    }


    void RotatePlayer(Vector2 dir)
    {

        //Rotate player with left Joystick input informaton and multiply by a rotation speed and deltaTime
        transform.Rotate(new Vector3(0, 0, dir.x * rotationSpeed * Time.deltaTime));

    }
    void Shoot(bool shoot)
    {

        //Can shoot if the input is true
        if (shoot && bulletsRemaining > MIN_BULLETS)
        {
            BulletManager.Instance.CreateBullet(gun.transform.position + gun.transform.right, new Vector2(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z), playerId);
            bulletsRemaining--;
        }

    }
    void Reload(bool reload)
    {
        if (reload)
            bulletsRemaining = MAX_BULLETS;
    }

    public void PlayerFixedUpdate()
    {

    }

    public void PlayerDies()
    {
        isAlive = false;
        gameObject.SetActive(false);
        PlayerManager.Instance.playersAlive--;
        PlayerManager.Instance.IsLastManStanding();
        
    }


}
