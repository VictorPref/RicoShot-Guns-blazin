using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager  {

    Player player;

    public void Initialize()
    {

        CreatePlayer();

    }

    public void Update()
    {
        if (player)
        {
            player.UpdatePlayer();
        }
    }

    void CreatePlayer()
    {
        //CREATE PLAYER HERE
    }

    public void DeleteManager()
    {

    }
}
