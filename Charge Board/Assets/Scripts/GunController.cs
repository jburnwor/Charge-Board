using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public bool isFiring;

    public Rigidbody bullet;
    public float bulletSpeed;

    public float timeBetweenShots;
    private float shotCounter;

    public Transform firePoint;

    Rigidbody player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                Rigidbody newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                newBullet.velocity = transform.forward * bulletSpeed + player.velocity;
                Destroy(newBullet.gameObject, 3);
            }
        }
        else
        {
            shotCounter = 0;
        }
    }
}
