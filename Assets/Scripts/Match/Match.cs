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
    }

    public void UpdateMatchInfo()
    {


    }

    public void PlayerWinsRound(Player winner)
    {
        BulletManager.Instance.DeleteAllBullets();
        foreach (Player p in playerList) { if (p.GetPlayerId() == winner.GetPlayerId()) p.roundsWon++; }

        if (IsMatchWon(winner))
        {
            Debug.Log("Winner is you:" + winner.GetPlayerId());
            //UIManager.UpdateMatchInfo(true);
            
        }
        else {
            //UIManager.UpdateMatchInfo(false);
            currentRound++;
            currentLevelNumber++;
            LevelManager.Instance.ResetLevel(currentLevelNumber);
        }
    }

    public bool IsMatchWon(Player lastRoundWinner)
    {
        return lastRoundWinner.roundsWon >= (totalRounds + 1) / 2 ? true : false;
    }
}
