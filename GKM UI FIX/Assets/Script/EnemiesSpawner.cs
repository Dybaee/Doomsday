using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{

    public int counter;
    public GameObject[] enemies;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 1f);
    }


    public void SpawnEnemy()
    {
        if (--counter == 0) CancelInvoke("SpawnEnemy");
        Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5)), Quaternion.identity);
    }    


    // Update is called once per frame
    void Update()
    {
        
    }
}
