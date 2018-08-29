using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Manager  {

    Phase1 phase1;

    public void Initialize()
    {


        phase1 = new Phase1();
    }

    public void Update()
    {
        if (phase1 != null)
        {
            phase1.UpdatePhase1();
        }
    }

    public void DeleteManager()
    {

    }
}
