using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float waitTime = 2f;
    private bool waitTimeIsRunning = true;
    
    // Update is called once per frame
    void Update()
    {
        if (waitTimeIsRunning == true)
        {
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                waitTimeIsRunning = false;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        waitTimeIsRunning = true;
        waitTime = 2f;
    }
}