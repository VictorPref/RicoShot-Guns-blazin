using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    public Text inventory;
    public Text bullet;

    public void UpdatePlayerUI(UIPlayerPKG pkg)
    {
        inventory.text = "" + pkg.inventaire.ToString();
        bullet.text = "" + pkg.bullet.ToString();
        if (pkg.id == 1) {
            Debug.Log(pkg.inventaire.ToString());
        }
    }

    public class UIPlayerPKG
    {
       public int inventaire;
       public int bullet;
        public int id;


    }

    

}
