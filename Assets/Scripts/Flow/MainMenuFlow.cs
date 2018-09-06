using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFlow : Flow  {

    MainMenuManager menuManager;

    public override void InitializeFlow()
    {
        base.InitializeFlow();
        menuManager = MainMenuManager.Instance;
        menuManager.Initialize();      
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        menuManager.Update(dt);
    }

    public override void FixedUpdate(float dt)
    {
        base.FixedUpdate(dt);


    }

}
