using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject bigBulletPrefab;
    public GameObject smallBulletPrefab;
    public Animator animator;

    //designer variables
    public float movementSpeed = 5;
    public bool grounded = false;
    public float jumpForce = 350;
    public float health = 100;

    /// <summary>
    /// Shoots a single bullet type desired per time(s) called
    /// </summary>
    /// <param name="bulletType"> The type of bullet desired to shoot</param>
    public void SingleShoot(GameObject bulletType)
    {
        //spawns the bullet at the firepoint position and direction
        Instantiate(bulletType, firePoint.position, firePoint.rotation);
    }

    /// <summary>
    /// Shoots 3 small bullets in 3 seperate directions, one straight, one spreadDegrees up, and one spreadDegrees down.
    /// </summary>
    /// <param name="spreadDegrees"> The difference up and down from straight forward in terms of degrees</param>
    public void ShotgunShoot(float spreadDegrees, Quaternion direction)
    {
        float shootDirection = direction.y * 180;
        //spawns the bullet at the firepoint position and direction
        Instantiate(smallBulletPrefab, firePoint.position, firePoint.rotation);
        //spawns the bullet at the firepoint position and direction + spreadDegrees
        Instantiate(smallBulletPrefab, firePoint.position, Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, (shootDirection - spreadDegrees)));
        Debug.Log("Y direction " + shootDirection);
        Debug.Log("Shoot direction " + (shootDirection + spreadDegrees));
        //spawns the bullet at the firepoint position and direction - spreadDegrees
        Instantiate(smallBulletPrefab, firePoint.position, Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, (shootDirection + spreadDegrees)));
    }

    /// <summary>
    /// The player or enemy will take set number of damage
    /// </summary>
    /// <param name="damage">The amount of damage the player enemy will take</param>
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    /// <summary>
    /// Die function
    /// </summary>
    public void Die()
    {
        Destroy(this.gameObject);
    }
    public enum weapon
    {
        Shotgun,
        AssualtRifle,
        SingleFire,
        BFG
    }
    public weapon equippedWeapon = weapon.SingleFire;
    //switch to shotgun mode
    public void ShotgunSwitch()
    {
        //set controller bools
        equippedWeapon = weapon.Shotgun;
        //set animator bools
        animator.SetBool("DoubleShot", true);
        animator.SetBool("ChargeShot", false);
        animator.SetBool("SingleShot", false);
    }
    //switch to single shot mode
    public void SingleSwitch()
    {
        //set controller bools
        equippedWeapon = weapon.SingleFire;
        //set animator bools
        animator.SetBool("DoubleShot", false);
        animator.SetBool("ChargeShot", false);
        animator.SetBool("SingleShot", true);
    }
    //switch to BFG mode
    public void BFGSwitch()
    {
        //set controller bools
        equippedWeapon = weapon.BFG;
        //set animator bools
        animator.SetBool("DoubleShot", true);
        animator.SetBool("ChargeShot", true);
        animator.SetBool("SingleShot", false);
    }
    //switch to assualt rifle mode
    public void AssualtSwitch()
    {
        //set controller bools
        equippedWeapon = weapon.AssualtRifle;
        //set animator bools
        animator.SetBool("DoubleShot", true);
        animator.SetBool("ChargeShot", false);
        animator.SetBool("SingleShot", false);
    }
}
