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
    int selectedButton = 0;
    bool joystick = false;
    bool aInput = true;

    public void Initialize()
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

    /// <summary>
    /// Inserts message declaring who's the winner of the match
    /// </summary>
    public void UpdateUIWinning(int id)
    {
        winner.text = "PLAYER " + id.ToString() + " IS THE BEST!!!!!";
    }

    public void UpdateButton()
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
                    ButtonSelect(1);
                }
                else if (pkg.leftDir.x < 0)
                {
                    ButtonSelect(-1);
                }

            }
            else if (pkg.leftDir.x == 0)
            {
                joystick = false;
            }
            //Click on the button
            if (pkg.A)
            {
                buttons[selectedButton].onClick.Invoke();
            }
        }
        else
        {
            aInput = true;
        }

    }

    void ButtonSelect(int i)
    {
        joystick = true;
        buttonColor[selectedButton].color = Color.white;

        //Change the selected button
        selectedButton += i;
        if(selectedButton < 0)
        {
            selectedButton = buttonObjects.Count - 1;
        }
        if(selectedButton > buttonObjects.Count - 1)
        {
            selectedButton = 0;
        }
        //Change the color of the selected Button
        buttonColor[selectedButton].color = Color.gray;      
    }

    /// <summary>
    /// Reloads the main game's scene
    /// </summary>
    public void ButtonReset()
    {
        //Reload the Game
        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// Loads the Main Menu scene
    /// </summary>
    public void ButtonMenu()
    {
        //Load the Menu
        SceneManager.LoadScene("MainMenu");
    }

}
