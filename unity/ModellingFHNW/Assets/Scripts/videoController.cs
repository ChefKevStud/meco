using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoController : MonoBehaviour
{

    public VideoPlayer _videoPlayer;
    private bool isReset = true;

    [Header("Keybinds")]
    public KeyCode playKey = KeyCode.Tab;
    public KeyCode resetKey = KeyCode.Backspace;
    
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
        else if (Input.GetKey(resetKey))
        {
            _videoPlayer.Stop();
            isReset = true;
        }
    }
}
