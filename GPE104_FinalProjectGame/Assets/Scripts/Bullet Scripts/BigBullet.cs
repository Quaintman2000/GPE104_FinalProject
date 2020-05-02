using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBullet : Bullets
{
    //number of enemies/players it can go through
    int enemyPenNum = 3;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(fireSound, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //have it shoot forward at the desinated speed over time
        this.transform.position += transform.right * bulletSpeed * Time.deltaTime;
        
        //count down timer
        bulletDuration -= Time.deltaTime;
        //if the duration is less than or equal to 0
        if (bulletDuration <= 0)
        {
            //destroy the bullet
            Destroy(this.gameObject);
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        //if the bullets hit the enemy
        if (collision.gameObject.tag == "Enemy")
        {
            //grab their controller
            AIController controller = collision.gameObject.GetComponent<AIController>();
            //reduce their health
            controller.TakeDamage(damage);
           
            //if it hits the last enemy enemy
            if (enemyPenNum == 1)
            {
                //destroy the bullet
                Destroy(this.gameObject);
            }
            //if not, subtract 1
            else
            {
                enemyPenNum -= 1;
            }

        }  
       else if(collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
        //hits anything else
        else
        {
            Destroy(this.gameObject);
        }
    }

}
