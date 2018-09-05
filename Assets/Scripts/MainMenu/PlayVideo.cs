using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour {

    public RawImage rawImage;
    public VideoPlayer videoPlayer;

   public void initialization()
    {
        StartCoroutine(Video());

    }

    IEnumerator Video()
    {
        if (videoPlayer)
        {
            videoPlayer.Prepare();
            WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
            while (!videoPlayer.isPrepared)
            {
                yield return waitForSeconds;
                break;
            }
            rawImage.texture = videoPlayer.texture;
            videoPlayer.Play();
        }
    }
}
