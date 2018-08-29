using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public InputPkg GetKeysInput()
    {
        //Create package
        InputPkg toRet = new InputPkg();

        //Get input
        float x = Input.GetAxis("Horizontal");
        bool isFiring = Input.GetAxis("Fire1") > 0;

        //Fill the package
        toRet.dirPressed = new Vector2(x, 0);
        toRet.shootPressed = isFiring;

        //return package
        return toRet;
    }


    public class InputPkg
    {
        public Vector2 dirPressed;
        public bool shootPressed;

    }

}
