using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowType { None, GameFlow, MainMenuFlow }
public class FlowManager
{
    FlowType currentActiveFlow = FlowType.None;
    Flow currentFlow;

    //Initialize type of Flow
    public void InitializeFlow(FlowType _FlowType)
    {

        if (_FlowType == FlowType.None)
        {
            Debug.Log("Cannot initialize with type none");
            return;
        }

        //flow Already opened
        if (currentActiveFlow == _FlowType)
        {
            Debug.Log("Already initialized");
            return;
        }

        //if flow exist it close
        if (currentFlow != null)
        {
            currentFlow.EndFlow();
            currentFlow = null;
            currentActiveFlow = FlowType.None;
        }

        switch (_FlowType)//New Flow
        {
            case FlowType.GameFlow:

                currentFlow = new GameFlow();

                break;
            case FlowType.MainMenuFlow:
                //Flow for the menu
                currentFlow = new MainMenuFlow();
               
                break;
            default:
                Debug.LogError("Unhandled switch: " + _FlowType);
                break;
        }
        currentActiveFlow = _FlowType;
        currentFlow.InitializeFlow();
    }

    public void UpdateFlow(float dt)
    {
        if (currentFlow != null)
        {
            currentFlow.Update(dt);
        }
    }
    public void FixedUpdateFlow(float dt)
    {
        currentFlow.FixedUpdate(dt);
    }
}
