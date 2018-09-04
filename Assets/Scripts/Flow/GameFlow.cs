using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : Flow {

    public readonly int CURRENTLEVELTESTED = 1;

    PlayerManager playerManager;
    BulletManager bulletManager;
    LevelManager levelManager;

    public override void InitializeFlow()
    {
        base.InitializeFlow();     
        bulletManager = BulletManager.Instance;
        levelManager = LevelManager.Instance;
        playerManager = PlayerManager.Instance;
        levelManager.GenerateLevel(CURRENTLEVELTESTED);       
        playerManager.Initialize();
    }
    public override void Update(float dt)
    {
        base.Update(dt);
        playerManager.Update(); 
        bulletManager.Update();
    }

    public override void FixedUpdate(float dt)
    {
        base.FixedUpdate(dt);
    }

}
