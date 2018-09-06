using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFlow : MonoBehaviour {

    GameObject option;

    /// <summary>
    /// Loads the game's main scene
    /// </summary>
	public void StartButton()
    {
        SceneManager.LoadScene("GameScene");
    }
  
    /// <summary>
    /// Instantiates the "options" canvas which gives an overview the controller mapping
    /// </summary>
    public void OptionButton()
    {
        if (option)
        {
            if (option.activeSelf)
            {
                option.SetActive(false);
            }
            else
            {
                option.SetActive(true);
            }
        }
        else
        {
            option = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/MainMenu/OptionCanvas"));
            option.transform.position = new Vector3(0, 0, 0);
        }
    }

    /// <summary>
    /// Closes the application
    /// </summary>
    public void QuitButton()
    { 
        Application.Quit();
    }
}
