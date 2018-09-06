using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager
{

    private static UI_Manager instance = null;

    List<UIPlayer> uiPlayers;
    UIRound uiRound;
    UIWinning uIWinning;
    GameObject window;
    bool end = false;
    #region Singleton
    public static UI_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UI_Manager();
            }
            return instance;
        }
    }
    #endregion

    public void initialization()
    {
        uiPlayers = new List<UIPlayer>();
        GameObject mainUI = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI_Game/UI_InGame"));


        uiRound = GameObject.FindGameObjectWithTag("UIRound").GetComponent<UIRound>();
        uiPlayers.Add(GameObject.FindGameObjectWithTag("UIPlayer1").GetComponent<UIPlayer>());
        uiPlayers.Add(GameObject.FindGameObjectWithTag("UIPlayer2").GetComponent<UIPlayer>());
        window = GameObject.FindGameObjectWithTag("WinningScreen");
        uIWinning = window.GetComponent<UIWinning>();
        uIWinning.initialization();
        window.SetActive(false);

    }

    public void UpdatePlayer(UIPlayer.UIPlayerPKG pkg)
    {
        uiPlayers[pkg.id-1].UpdatePlayerUI(pkg);
    }
    public void UpdateRound(UIRound.UIRoundPKG pkg)
    {
        uiRound.UpdateUIRound(pkg);
    }
    public void UpdateWinning(int id)
    {
        window.SetActive(true);
        end = true;
        uIWinning.UpdateUIWinning(id);
    }
    public void UpdateButton()
    {
       
        if(end)
        uIWinning.UpdateBoutton();
    }


}

