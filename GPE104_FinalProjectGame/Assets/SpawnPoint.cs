using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //set this as the spawn point at the start
        GameManager.instance.spawnPoint = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
