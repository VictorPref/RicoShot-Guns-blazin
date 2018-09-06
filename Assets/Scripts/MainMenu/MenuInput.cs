using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput  {

    public InputPkg GetKeysInput()
    {
        //Create package
        InputPkg toRet = new InputPkg();

        //Get input
        float left_horizontal = Input.GetAxis("P1_Horizontal");
        float left_vertical = Input.GetAxis("P1_Vertical");
        float A = Input.GetAxis("Submit");
        float B = Input.GetAxis("Cancel");

        //Fill the package
        toRet.leftDir = new Vector2(left_horizontal, left_vertical);
      
        toRet.A = A != 0 ? true : false;
        toRet.B = B != 0 ? true : false;

        //return package
        return toRet;
    }

    public class InputPkg
    {
        public Vector2 leftDir;
        public bool A;
        public bool B;
    }
}
