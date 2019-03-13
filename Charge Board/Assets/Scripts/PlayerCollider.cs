using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public float playerHealth;

    public void Start()
    {
        playerHealth = 100;
    }

    public void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Debug.Log("player hit");
            playerHealth -= 5;
            Destroy(other.gameObject);
        }
    }
}
