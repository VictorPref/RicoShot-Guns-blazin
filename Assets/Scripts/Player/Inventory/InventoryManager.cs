using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    #region Singleton
    private static InventoryManager instance = null;

    public static InventoryManager Instance
    {
        get
        {   
                if (instance == null)
                {
                    instance = new InventoryManager();
                    Initialize();

                }
                return instance;
        }
    }
    #endregion

    public static Inventory inventory;

    static void Initialize()
    {
        inventory = new Inventory();
        inventory.Initialize();
    }
}
