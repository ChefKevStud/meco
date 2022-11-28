using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoController : MonoBehaviour
{

    public VideoPlayer _videoPlayer;
    private bool isReset = true;

    [Header("Keybinds")]
    private KeyCode playKey = KeyCode.LeftControl;
    
    void Update()
    {
        if (Input.GetKeyUp(playKey) && (isReset || _videoPlayer.isPaused))
        {
            _videoPlayer.Play();
            isReset = false;
        }
        else if (Input.GetKeyUp(playKey) && _videoPlayer.isPlaying)
        {
            _videoPlayer.Pause();
        }
        else if (Input.GetKey(KeyCode.F1))
        {
            _videoPlayer.Stop();
            isReset = true;
        }
    }
}
