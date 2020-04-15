using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBullet : Bullets
{
    
    // Start is called before the first frame update
    void Start()
    {
        
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
}
