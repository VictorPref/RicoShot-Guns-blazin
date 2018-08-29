using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    InputManager inputManager;
    Phase1Manager phase1Manager;

   
    public void PlayerCreated()
    {
        inputManager = new InputManager();
        phase1Manager.Initialize();


    }

    public void UpdatePlayer()
    {
        InputManager.InputPkg inputPkg = inputManager.GetKeysInput();
        RotatePlayer(inputPkg.dirPressed);
        
    }


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

    // Update is called once per frame
    void Update () {


        //Rotation systeme with Keyboard
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 2));
            phase1Manager.Update();
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Rotate(new Vector3(0, 0, -2));
        }



    }
}
