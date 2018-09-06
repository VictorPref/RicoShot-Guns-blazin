using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFlow : MonoBehaviour {

    GameObject option;

	public void StartButton()
    {
        Debug.Log("Start");
        SceneManager.LoadScene("GameScene");
    }
  
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
    public void QuitButton()
    { 
        Application.Quit();
    }
}
