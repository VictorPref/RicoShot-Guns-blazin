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

    // Use this for initialization
    void Start()
    {
        flowManager.InitializeFlow(flowTypeInit);
    }

    // Update is called once per frame
    void Update()
    {
        flowManager.UpdateFlow(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        flowManager.FixedUpdateFlow(Time.deltaTime);
    }
}
