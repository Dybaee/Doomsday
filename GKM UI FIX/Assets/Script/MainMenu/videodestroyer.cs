using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("Stage 1");
    }
}
