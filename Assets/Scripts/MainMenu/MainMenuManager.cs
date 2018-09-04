using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager  {

    private static MainMenuManager instance = null;

    GameObject mainMenu;
    MainMenu menu;
   
    public static MainMenuManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MainMenuManager();

            }
            return instance;
        }
    }
    public void initialization()
    {
        mainMenu = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/MainMenu/MainMenu"));
        menu = mainMenu.GetComponent<MainMenu>();
        menu.initialization();
    }

    public void Update(float dt)
    {
        menu.UpdateMenu();
    }
}
