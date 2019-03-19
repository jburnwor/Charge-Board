using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{

    public bool isFiring;

    public float gunCharge = 100;

    public BulletController bullet;
    public float bulletSpeed;

    public float timeBetweenShots;
    private float shotCounter;

    public Transform firePoint;

    Rigidbody player;

    public Image gunChargeBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.stopEnemies)
        {
            if (isFiring && gunCharge > 0)
            {
                gunChargeBar.fillAmount = gunCharge / 100;
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    gunCharge -= 5;

                    shotCounter = timeBetweenShots;
                    BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
                    //newBullet.velocity = transform.forward * bulletSpeed + player.velocity;
                    newBullet.speed = bulletSpeed;
                    Destroy(newBullet.gameObject, 2);
                }
            }
            else
            {
                shotCounter = 0;
            }
        }
        
    }
}
