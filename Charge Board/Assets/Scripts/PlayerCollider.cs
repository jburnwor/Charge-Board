using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollider : MonoBehaviour
{
    public float playerHealth;
    public GunController gun;
    public ParticleSystem hit;
    public Image healthBar;
    public Text deathText;
    public float time;
    public float timerTime = 5f;
    public ParticleSystem playerHit;

    public void Start()
    {
        playerHealth = 100;
        gun = GetComponentInChildren<GunController>();
        deathText.enabled = false;
        time = 0;
    }

    public void Update()
    {
        

        if (playerHealth <=0)
        {
            deathText.enabled = true;
            time += Time.deltaTime;
            //Debug.Log(time);
            if (time >= timerTime)
            {
                time = 0;
                SceneManager.LoadScene("Menu 3D");
            }
        }
        else
        {
            healthBar.fillAmount = playerHealth/100;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Debug.Log("player hit");
            playerHealth -= 10;
            Instantiate(playerHit, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if(other.tag == "Enemy")
        {
            if (!GameManager.lockControls)
            {
                playerHealth -= 5;
            }
            else
            {
                gun.gunCharge = 100;
                if(Time.timeScale == 1.0)
                {
                    Time.timeScale = 0.3f;
                    Instantiate(hit, transform.position, Quaternion.identity);
                }
                
            }

            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(Time.timeScale < 1.0f)
        {
            Time.timeScale = 1.0f;
        }
    }
}
