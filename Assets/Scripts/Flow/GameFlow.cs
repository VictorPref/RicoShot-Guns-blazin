using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : Flow {

    public readonly int CURRENTLEVELTESTED = 1;

    PlayerManager playerManager;
    BulletManager bulletManager;
    LevelManager levelManager;
    MatchManager matchManager;
    UI_Manager uI_Manager;

    public override void InitializeFlow()
    {
        base.InitializeFlow();     
        bulletManager = BulletManager.Instance;
        levelManager = LevelManager.Instance;
        playerManager = PlayerManager.Instance;
        matchManager = MatchManager.Instance;
        uI_Manager = UI_Manager.Instance;


        levelManager.GenerateLevel(CURRENTLEVELTESTED);
        uI_Manager.initialization();
        playerManager.Initialize();
        matchManager.Initialize();
        
    }
    public override void Update(float dt)
    {
        base.Update(dt);

        playerManager.Update(); 
        bulletManager.Update();
        matchManager.Update();
        levelManager.Update();
    }

    public override void FixedUpdate(float dt)
    {
        base.FixedUpdate(dt);
    }

}
