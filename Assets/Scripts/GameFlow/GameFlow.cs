using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : Flow {

    PlayerManager playerManager;
    BulletManager bulletManager;

    public override void InitializeFlow()
    {
        base.InitializeFlow();
        playerManager = new PlayerManager();
        playerManager.Initialize();
        bulletManager = BulletManager.Instance;

    }
    public override void Update(float dt)
    {
        base.Update(dt);
        playerManager.Update();
        Debug.Log("Update GameFlow"); 
        bulletManager.Update();
    }

    public override void FixedUpdate(float dt)
    {
        base.FixedUpdate(dt);


    }

}
