using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videodestroyer : MonoBehaviour
{
    [SerializeField] VideoPlayer myVideoPlayer;

    public GameObject _vImage;

    // Start is called before the first frame update
    void Start()
    {
        myVideoPlayer.loopPointReached += Destroyvideo;
    }

    void Destroyvideo(VideoPlayer vp)
    {
        _vImage.SetActive(false);
    }
}
