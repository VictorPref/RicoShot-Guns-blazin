using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match
{
    List<Player> playerList;
    int totalRounds = 3, currentRound, currentLevelNumber;

    /// <summary>
    /// Initializes match information
    /// </summary>
    public void Initialize()
    {
        playerList = PlayerManager.Instance.GetPlayers();
        currentLevelNumber = LevelManager.Instance.lvlNumber;
        currentRound = 1;
        UpdateUI();
    }

    public void UpdateMatchInfo()
    {
        UI_Manager.Instance.UpdateButton();
    }

    /// <summary>
    /// Updates information after a round has ended
    /// </summary>
    public void PlayerWinsRound(Player winner)
    {
        try
        {
            foreach (Player p in playerList) { if (p.GetPlayerId() == winner.GetPlayerId()) p.roundsWon++; }

            if (IsMatchWon(winner))
            {
                UI_Manager.Instance.UpdateWinning(winner.GetPlayerId());
            }
            else
            {
                currentRound++;
                currentLevelNumber++;
                LevelManager.Instance.ResetLevel(currentLevelNumber);
                BulletManager.Instance.DeleteAllBullets();
            }
        }
        catch
        {
            Debug.Log("No player alive");
        }
        UpdateUI();
    }

    /// <summary>
    /// declares a winner if the winner of the previous round has won more than half the round total + 1
    /// </summary>
    public bool IsMatchWon(Player lastRoundWinner)
    {
        return lastRoundWinner.roundsWon >= (totalRounds + 1) / 2 ? true : false;
    }

    /// <summary>
    /// Calls an update to the UI Manager for round/match information
    /// </summary>
    public void UpdateUI()
    {
        UIRound.UIRoundPKG pkg = new UIRound.UIRoundPKG();
        pkg.p1Round = playerList[0].roundsWon;
        pkg.p2Round = playerList[1].roundsWon;
        pkg.round = currentRound;
        UI_Manager.Instance.UpdateRound(pkg);
    }
}
