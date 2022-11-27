using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoController : MonoBehaviour
{

    public VideoPlayer _videoPlayer;
    public int state = 0;

    [Header("Keybinds")]
    public KeyCode playKey = KeyCode.LeftControl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(playKey) && state == 0)
        {
            Debug.Log("Called to play");
            _videoPlayer.Play();
            state = 1;
        }
        else if (Input.GetKey(playKey) && state == 1)
        {
            Debug.Log("Called to pause");
            _videoPlayer.Pause();
            state = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Called to stop");
            _videoPlayer.Stop();
            state = 0;
        }
    }
}
