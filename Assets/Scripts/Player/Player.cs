using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerColor { BLUE, RED, GREEN, YELLOW };
enum ObstacleType { ObsMSmall1, ObsMSmall2, ObsWSmall1, ObsWSmall2 };

public class Player : MonoBehaviour {

    public GameObject gun;
    InputManager inputManager;
    int playerId;
    float timeShoot;
    float rotationSpeed = -50;
    BulletManager bulletManager;
    readonly int CHARGE_MAX = 6;
    readonly int CHARGE_MIN = 0;
    int chargeur;
    int currentObstacle = 0;

    public void setPlayerId(int playerId)
    {
        this.playerId = playerId;
        inputManager = new InputManager();
        timeShoot = Time.time;
        bulletManager = BulletManager.Instance;
        chargeur = CHARGE_MAX;
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

     void UpdatePhase1(InputManager.InputPkg inputPkg) {


    }

     void UpdatePhase2(InputManager.InputPkg inputPkg) {

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
            bulletManager.CreateBullet(gun.transform.position+gun.transform.right ,new Vector2(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.z),playerId);
            chargeur--;  
        }
        
    }
    void Reload(bool reload)
    {
        if (reload)
            chargeur = CHARGE_MAX;
    }

    void CycleObstacles() {

    }

    public void PlayerFixedUpdate()
    {

    }

    public void PlayerDies()
    {

    }

 
}
