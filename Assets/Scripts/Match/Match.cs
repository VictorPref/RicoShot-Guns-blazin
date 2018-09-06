using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match
{
    List<Player> playerList;
    int totalRounds = 3, currentRound, currentLevelNumber;

    public void Initialize()
    {
        playerList = PlayerManager.Instance.GetPlayers();
     //   PlayerManager.Instance.GetPlayers().ToArray().CopyTo(playerList, 0);
        currentLevelNumber = LevelManager.Instance.lvlNumber;
        currentRound = 1;
        UpdateUI();
    }

    public void UpdateMatchInfo()
    {
        UI_Manager.Instance.UpdateButton();

    }

    public void PlayerWinsRound(Player winner)
    {

        try
        {
            foreach (Player p in playerList) { if (p.GetPlayerId() == winner.GetPlayerId()) p.roundsWon++; }

            if (IsMatchWon(winner))
            {
                Debug.Log("Winner is you:" + winner.GetPlayerId());
                //UIManager.UpdateMatchInfo(true);

                UI_Manager.Instance.UpdateWinning(winner.GetPlayerId());

            }
            else
            {
                //UIManager.UpdateMatchInfo(false);
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

    public bool IsMatchWon(Player lastRoundWinner)
    {
        return lastRoundWinner.roundsWon >= (totalRounds + 1) / 2 ? true : false;
    }
    void UpdateUI()
    {
        UIRound.UIRoundPKG pkg = new UIRound.UIRoundPKG();
        pkg.p1Round = playerList[0].roundsWon;
        pkg.p2Round = playerList[1].roundsWon;
        pkg.round = currentRound;
        UI_Manager.Instance.UpdateRound(pkg);

    }
}
