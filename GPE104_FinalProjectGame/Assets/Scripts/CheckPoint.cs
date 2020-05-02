using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CircleCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = this.gameObject.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //set the this checkpoint to be the new spawnpoint
        collider.enabled = false;
        GameManager.instance.spawnPoint = this.gameObject;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
