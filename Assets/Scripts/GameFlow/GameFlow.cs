﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : Flow {

    PlayerManager playerManager;

    public override void InitializeFlow()
    {
        base.InitializeFlow();
        playerManager = new PlayerManager();
        playerManager.Initialize();

    }
    public override void Update(float dt)
    {
        base.Update(dt);
        playerManager.Update();
    }

    public override void FixedUpdate(float dt)
    {
        base.FixedUpdate(dt);


    }

}
