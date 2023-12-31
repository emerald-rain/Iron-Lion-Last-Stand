using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidPlayer : MonoBehaviour
{
    [SerializeField] string videoFilename;
    [SerializeField] VideoPlayer videoPlayer; 
    [SerializeField] RawImage rawImage;
    [SerializeField] SceneTransition sceneTransition;
 
    public void PlayVideo() {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer) {
            ToggleSetActive(); // Obj should be deactivated before starting the game

            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFilename);
            videoPlayer.url = videoPath;

            videoPlayer.loopPointReached += SwitchSceneOnEnd;
            videoPlayer.Play();
        }
    }

    public void SkipVideo() {
        if (videoPlayer.isPlaying) {
            videoPlayer.Stop(); SwitchSceneOnEnd(videoPlayer);
        }
    }

    private void ToggleSetActive() {
        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }

    private void SwitchSceneOnEnd(VideoPlayer vp) {
        if (SceneManager.GetActiveScene().name == "WaveAttack") {
            gameObject.SetActive(false);
        } else {
            sceneTransition.LoadScene("WaveAttack");
        }
    }
}
