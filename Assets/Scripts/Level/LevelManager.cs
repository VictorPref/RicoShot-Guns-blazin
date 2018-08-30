using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager {

    private Level currentLevel;


    public void Initialize() {

    }

    public void Update() {
        PlayerManager.Instance.Update();
    }


}
