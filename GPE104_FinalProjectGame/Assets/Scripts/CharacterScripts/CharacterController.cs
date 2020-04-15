using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject bigBulletPrefab;
    public GameObject smallBulletPrefab;

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
    public void ShotgunShoot(float spreadDegrees)
    {
        //spawns the bullet at the firepoint position and direction
        Instantiate(smallBulletPrefab, firePoint.position, firePoint.rotation);
        //spawns the bullet at the firepoint position and direction + spreadDegrees
        Instantiate(smallBulletPrefab, firePoint.position, Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z + spreadDegrees));
        //spawns the bullet at the firepoint position and direction - spreadDegrees
        Instantiate(smallBulletPrefab, firePoint.position, Quaternion.Euler(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z - spreadDegrees));
    }
}
