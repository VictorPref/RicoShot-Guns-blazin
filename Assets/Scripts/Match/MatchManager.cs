using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager {

    private static MatchManager instance = null;

    LevelManager levelManager;
    PlayerManager playerManager;
    Match match;

    #region Singleton
    public static MatchManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MatchManager();

            }
            return instance;
        }
    }
    #endregion

    public void Initialize() {
        //levelManager = LevelManager.Instance;
        //playerManager = PlayerManager.Instance;
        match = new Match();
        CreateMatch();
    }

    public void CreateMatch() {
        match.Initialize();
    }

   public void Update() {
        match.UpdateMatchInfo();

	}

    public void PlayerWinsRound(Player winner) {
        match.PlayerWinsRound(winner);
    }

}
