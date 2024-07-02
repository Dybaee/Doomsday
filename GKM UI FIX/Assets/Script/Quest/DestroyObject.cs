using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    public Item2OnGround item2OnGround;

    // Start is called before the first frame update
    void Start()
    {
        item2OnGround = GetComponent<Item2OnGround>();
    }

    private void OnDestroy()
    {
        
    }
}
