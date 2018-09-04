using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour {

    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public AudioSource audio;


	// Use this for initialization
	void Start () {

        StartCoroutine(Video());

	}

    IEnumerator Video()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        audio.Play();
    }
}
