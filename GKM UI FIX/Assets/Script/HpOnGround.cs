using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpOnGround : MonoBehaviour
{
    public GameObject hpModel;
    public float health = 100f;

    Player2Controller pcontroller;


    // Start is called before the first frame update
    void Start()
    {
        pcontroller = GetComponent<Player2Controller>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropHP()
    {
        Vector3 position = transform.position;
        GameObject hp = Instantiate(hpModel, position ,Quaternion.identity);
        hp.SetActive(true);
    }
}
