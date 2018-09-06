using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour {

    MenuInput input;
    List<Button> buttons;  
    Image imgButton;
    GameObject optionPanel;
    PlayVideo video;
    int selectedButton = 0;
    bool changeButton = false;
    bool inOption = false;
    
    public void Initialize()
    {
        input = new MenuInput();
        buttons = new List<Button>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Buttons")
                foreach (Transform c in child.gameObject.transform)
                {
                    buttons.Add(c.gameObject.GetComponent<Button>());

                }
            if(child.gameObject.name == "RawImage")
            {
                video = child.transform.GetChild(0).GetComponent<PlayVideo>();
            }
        }
        video.initialization();
        imgButton = buttons[selectedButton].GetComponent<Image>();
        UpdateList(selectedButton);
    }

    /// <summary>
    /// Updates 
    /// </summary>
	public void UpdateMenu () {
        MenuInput.InputPkg inputPkg = input.GetKeysInput();
        if (!inOption)
        {
            if (inputPkg.leftDir.y < 0 && !changeButton)
            {
                changeButton = true;
                UpdateList(1);
            }
            else if (inputPkg.leftDir.y > 0 && !changeButton)
            {
                changeButton = true;
                UpdateList(-1);
            }
            if (inputPkg.leftDir.y == 0)
            {
                changeButton = false;
            }
        }
        if (inputPkg.A && !inOption)
        {
            buttons[selectedButton].onClick.Invoke();
            inOption = true;
        }
        else if (inputPkg.B && inOption)
        {
            buttons[selectedButton].onClick.Invoke();
            inOption = false;
        }
        imgButton.color = Color.gray;
    }

    /// <summary>
    /// Updates selected button in button List. Reverts the previously selected menu button's color to default.
    /// </summary>
    void UpdateList(int pos)
    {
        imgButton.color = Color.white;
        
          selectedButton += pos;

        if (selectedButton >= buttons.Count)
            selectedButton = 0;
        if (selectedButton < 0)
            selectedButton = buttons.Count - 1;

        imgButton = buttons[selectedButton].GetComponent<Image>();
    }  
}
