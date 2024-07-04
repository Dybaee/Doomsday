using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration;
    public float magnitude;
    private Player2Controller player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2Controller>();
    }

    private void Update()
    {
        if (player.knockbackTimer > 0)
        {
            StartCoroutine(Shake());
        }
    }

    public IEnumerator Shake()
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
