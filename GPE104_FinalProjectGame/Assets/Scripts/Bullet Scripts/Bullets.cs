using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float bulletSpeed = 6;
    public float bulletDuration = 5;
    public int damage = 10;
    public AudioClip fireSound;
    // Update is called once per frame
    void Update()
    {
        
    }

   public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //if the bullets hit the enemy
        if (collision.gameObject.tag == "Enemy")
        {
            //grab their controller
            AIController controller = collision.gameObject.GetComponent<AIController>();
            //reduce their health
            controller.TakeDamage(damage);
            //destroy the bullet
            Destroy(this.gameObject);
        }
        //if it's a player
        else if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlatformerController>().bulletImmunity == false)
        {
            //grab their controller
            PlatformerController controller = collision.gameObject.GetComponent<PlatformerController>();
            //reduce their health
            controller.TakeDamage(damage);
            //destroy the bullet
            Destroy(this.gameObject);
        }
        //hits anything else
        else
        {
            Destroy(this.gameObject);
        }
    }
}
