using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    private static InventoryManager instance = null;

    public static InventoryManager Instance
    {
        get
        {   
                if (instance == null)
                {
                    instance = new InventoryManager();

                }
                return instance;
        }
    }

    Inventory inventory;

    public void initialization()
    {
        inventory = new Inventory();
    }


}
