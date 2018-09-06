using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntry : MonoBehaviour {

    public FlowType flowTypeInit;
    FlowManager flowManager;

    private void Awake()
    {
        flowManager = new FlowManager();    
    }

    /// <summary>
    /// Initializes a flow
    /// </summary>
    void Start()
    {
        flowManager.InitializeFlow(flowTypeInit);
    }

    void Update()
    {
        flowManager.UpdateFlow(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        flowManager.FixedUpdateFlow(Time.deltaTime);
    }
}
