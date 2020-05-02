using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class AIController : CharacterController
{
    //NonDesigner Friendly variables
    public Rigidbody2D rbody;
    public SpriteRenderer sr;
    public Transform groundPointTransform;
    public Transform tf;
    public HealthBar healthBar;

    //waypoints
    public Transform wayPointOne;
    public Transform wayPointTwo;
    public Transform destinationWayPoint;
    public bool canShoot
    {
        get
        {
            return (shotTimer <= 0);
        }
    }
    public float timeBetweenShots = 1;
    private float shotTimer;
    public float fieldOfView = 45;

    public enum enemyState
    {
        patrol,
        Attack,
        Dead
    };

    public enemyState currentState = enemyState.patrol;

    // Start is called before the first frame update
    void Start()
    {
        //get the neccesary components
        rbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
        tf = GetComponent<Transform>();

        //player starts with single fire
        equippedWeapon = weapon.SingleFire;
        animator.SetBool("SingleShot", true);

        //set up health and health bar
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //set up firstwaypoint;
        destinationWayPoint = wayPointOne;
    }

    // Update is called once per frame
    void Update()
    {
        //update the health bar to player's current health
        healthBar.SetHealth(health);

        //face the player in the direction he's moving
        //when moving right
        if (rbody.velocity.x > 0)
        {
            //face right
            tf.rotation = Quaternion.Euler(0, 0, 0);
        }
        //when moving left
        else if (rbody.velocity.x < 0)
        {
            //face left
            tf.rotation = Quaternion.Euler(0, 180, 0);
        }
        animator.SetFloat("Speed", Mathf.Abs(rbody.velocity.x));
        animator.SetFloat("VerticalSpeed", rbody.velocity.y);

        //AI handler
        // if the enemy state is attack
        if (currentState == enemyState.Attack)
        {
            //stop moving
            rbody.velocity = new Vector2(0, rbody.velocity.y);

            if (canShoot)
            {
                animator.SetTrigger("shoot");
                shotTimer = timeBetweenShots;
            }
        }
        else if (currentState == enemyState.patrol)
        {
            //enemy patrols to waypoint one then two
            //if the destination's is to the right
            if (destinationWayPoint.position.x > tf.position.x)
            {
                //move right
                rbody.velocity = new Vector2(movementSpeed, rbody.velocity.y);
                if ((destinationWayPoint.position.x - 1) < tf.position.x)
                {
                    //switch to the new waypoint
                    //if it was the first
                    if (destinationWayPoint == wayPointOne)
                    {
                        destinationWayPoint = wayPointTwo;
                    }
                    else
                    {
                        destinationWayPoint = wayPointOne;
                    }
                }
            }
            //if the destination is to the left
            else if (destinationWayPoint.position.x < tf.position.x)
            {
                //move left
                rbody.velocity = new Vector2(-movementSpeed, rbody.velocity.y);
                if ((destinationWayPoint.position.x + 1) > tf.position.x)
                {
                    //switch to the new waypoint
                    //if it was the first
                    if (destinationWayPoint == wayPointOne)
                    {
                        destinationWayPoint = wayPointTwo;
                    }
                    else
                    {
                        destinationWayPoint = wayPointOne;
                    }
                }
            }
        }
        else if (currentState == enemyState.Dead)
        {
            Die();
        }


        //cooldown the time between shots
        if (!canShoot)
        {
            shotTimer -= Time.deltaTime;
        }
        //player detection
        if (CanSee(GameManager.instance.player))
        {
            currentState = enemyState.Attack;
        }
        else
        {
            currentState = enemyState.patrol;
        }

        //send a raycast to see if the player is close enough to the ground to be consider "grounded"
        RaycastHit2D hitInfo = Physics2D.Raycast(groundPointTransform.position, Vector2.down, 0.1f);
        //if on the ground
        if (hitInfo.collider != null)
        {
            grounded = true;

        }
        //if not on the ground
        else
        {
            grounded = false;
        }
        animator.SetBool("Grounded", grounded);
        //if the player has no health
        if (health <= 0)
        {
            //stop moving
            rbody.velocity = new Vector2(0, 0);
            //die
            currentState = enemyState.Dead;
        }
    }
    public bool CanSee(GameObject target)
    {
        Vector3 vectorToTarget = target.transform.position - tf.position;
        //detect if target is inside FOV
        float angleToTarget = Vector3.Angle(vectorToTarget, tf.right);
        //if within angle of view
        if (angleToTarget <= fieldOfView)
        {
            //detect if target is in line of sight
            RaycastHit2D hitinfo = Physics2D.Raycast(tf.position, target.transform.position - tf.position);
            //if it detects the player
            if (hitinfo.collider == GameManager.instance.player.GetComponent<BoxCollider2D>())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //if not
        else
        {
            return false;
        }

    }

}

