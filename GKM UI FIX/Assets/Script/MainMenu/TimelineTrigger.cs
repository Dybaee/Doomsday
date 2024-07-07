using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineTrigger : MonoBehaviour
{
    public PlayableDirector playableDirector;

    private void Start()
    {
        playableDirector.gameObject.SetActive(true);

        playableDirector.time = 0;
        playableDirector.initialTime = 0;
        playableDirector.Stop();
        playableDirector.Play();
    }
}
