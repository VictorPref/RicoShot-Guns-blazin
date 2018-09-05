using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntry : MonoBehaviour {

    public FlowType flowTypeInit;
    FlowManager flowManager;


    delegate void TestDelegate(string s);
    static void M(string s)
    {
        Debug.Log(s);
    }

    private void Awake()
    {
        flowManager = new FlowManager();

        TestDelegate testDelC = (x) => { Debug.Log(x);Debug.Log(19); };

        testDelC("Yolo");
    
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
