using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager  {

    GameObject mainMenu;
    MainMenu menu;
    #region Singleton
    private static MainMenuManager instance = null;
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
    #endregion

    public void Initialize()
    {
        mainMenu = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/MainMenu/MainMenu"));
        menu = mainMenu.GetComponent<MainMenu>();
        menu.Initialize();
    }

    public void Update(float dt)
    {
        menu.UpdateMenu();
    }
}
