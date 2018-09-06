using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIWinning : MonoBehaviour  {

    // Use this for initialization
    public Text winner;
    MenuInput menuInput;
    List<Button> buttons;
    List<GameObject> buttonObjects;
    List<Image> buttonColor;
    int selectionner = 0;
    bool joystick = false;
    bool aInput = true;

    public void initialization()
    {
        //Create a list of buttons
        buttons = new List<Button>();
        //create a list of image in the button
        buttonColor = new List<Image>();
        //create gameobject of button
        buttonObjects = new List<GameObject>();
        //Find gameobject with the tag Button
        buttonObjects.AddRange(GameObject.FindGameObjectsWithTag("Boutton"));

        //Get the two component needed from every button
        foreach (GameObject g in buttonObjects)
        {
            buttons.Add(g.GetComponent<Button>());
            buttonColor.Add(g.GetComponent<Image>());
        }
       


    }

    public void UpdateUIWinning(int id)
    {
        //SUPER DUPER TEXT for the winner
        winner.text = "PLAYER " + id.ToString() + " IS THE BEST!!!!!";

    }

    public void UpdateBoutton()
    {
        if(menuInput ==null)
            menuInput = new MenuInput();

        MenuInput.InputPkg pkg = menuInput.GetKeysInput();
        //Block the button A to be pressed when the winning screen is not activated or when the player already press A just after the winning screen is enabled
        if (!pkg.A || !aInput && gameObject.activeSelf)
        {

            aInput = false;
            //block the joystick after one movement and unlock it when no movement
            if (!joystick)
            {
                if (pkg.leftDir.x > 0)
                {
                    BouttonSelect(1);
                }
                else if (pkg.leftDir.x < 0)
                {
                    BouttonSelect(-1);
                }

            }
            else if (pkg.leftDir.x == 0)
            {
                joystick = false;
            }
            //Click on the button
            if (pkg.A)
            {
                buttons[selectionner].onClick.Invoke();
            }
        }
        else
        {
            aInput = true;
        }

    }

    void BouttonSelect(int i)
    {
        joystick = true;
        buttonColor[selectionner].color = Color.white;

        //Change the selected button
        selectionner += i;
        if(selectionner < 0)
        {
            selectionner = buttonObjects.Count - 1;
        }
        if(selectionner > buttonObjects.Count - 1)
        {
            selectionner = 0;
        }
        //Change the color of the selected Button
        buttonColor[selectionner].color = Color.gray;
       
    }
    public void ButtonReset()
    {
        //Reload the Game
        SceneManager.LoadScene("GameScene");
    }
    public void ButtonMenu()
    {
        //Load the Menu
        SceneManager.LoadScene("MainMenu");
    }

}
