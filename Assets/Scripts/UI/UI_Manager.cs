using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager
{

    private static UI_Manager instance = null;

    List<UIPlayer> uiPlayers;
    UIRound uiRound;

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

    }

    public void UpdatePlayer(UIPlayer.UIPlayerPKG pkg)
    {
        uiPlayers[pkg.id].UpdatePlayerUI(pkg);
    }
    public void UpdateRound(UIRound.UIRoundPKG pkg)
    {
        uiRound.UpdateUIRound(pkg);
    }


}

