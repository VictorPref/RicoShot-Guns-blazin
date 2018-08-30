using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    bool rtDown = false;
    public InputPkg GetKeysInput(int id)
    {
        //Create package
        InputPkg toRet = new InputPkg();
        

        //Get input
        float left_horizontal = Input.GetAxis("P"+id+"_Horizontal");
        float left_vertical= Input.GetAxis("P" + id + "_Vertical");
        float right_horizontal = Input.GetAxis("P" + id + "_ROTATE");
        float A= Input.GetAxis("P" + id + "_A");
        float B= Input.GetAxis("P" + id + "_B");
        float X= Input.GetAxis("P" + id + "_X");
        float Y= Input.GetAxis("P" + id + "_Y");
        float start= Input.GetAxis("P" + id + "_START");
        float rb= Input.GetAxis("P" + id + "_RB");
        float lb= Input.GetAxis("P" + id + "_LB");
        float rt= Input.GetAxis("P" + id + "_RT");
        float lt= Input.GetAxis("P" + id + "_LT");

        
      
        //Fill the package
        toRet.leftDir = new Vector2(left_horizontal, left_vertical);
        toRet.rightDir = new Vector2(right_horizontal, 0);
        toRet.A = A != 0 ? true:false;
        toRet.B = B != 0 ? true : false;
        toRet.X = X != 0 ? true : false;
        toRet.Y = Y != 0 ? true : false;
        toRet.start = start != 0 ? true : false;
        toRet.rb = rb != 0 ? true : false;
        toRet.lb = lb != 0 ? true : false;


        toRet.rt = rt != 0 ? true : false;

        //Check if the input is already pressed so player can't spam the bullet
        IsShooting(ref toRet.rt);

        toRet.lt = lt != 0 ? true : false;
        toRet.id = id;

        //return package
        Debug.Log(toRet.ToString());

        return toRet;
    }

    public void IsShooting(ref bool rt)
    {
        if (rt && !rtDown)
        {
            rtDown = true;
        }
        else if (rtDown && rt)
        {
            rt = false;
        }
        else
        {
            rtDown = false;
        }
    }


    public class InputPkg
    {
        public Vector2 leftDir;
        public Vector2 rightDir;
        public bool A;
        public bool B;
        public bool X;
        public bool Y;
        public bool start;
        public bool rb;
        public bool lb;
        public bool rt;
        public bool lt;
        public int id;

        override
        public string ToString()
        {
            return "ID: " + id + " RB: " + rb + " RT: " + rt + " LB: " + lb + " LT: " + lt;
        }
        
    }

}
