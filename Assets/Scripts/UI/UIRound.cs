using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIRound : MonoBehaviour {

    // Use this for initialization
    public Text p1Round;
    public Text p2Round;
    public Text round;

    public void UpdateUIRound(UIRoundPKG pkg)
    {
        p1Round.text = "" + pkg.p1Round;
        p2Round.text = "" + pkg.p2Round;
        round.text = "" + pkg.round;
    }



    public class UIRoundPKG
    {
        public int round;
        public int p1Round;
        public int p2Round;


    }

}
