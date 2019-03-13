using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public float enemyHealth;
    StateController enemy;

    public void Start()
    {
        enemy = GetComponent<StateController>();
    }

    public void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("hit enemy");
            enemy.TakeDamage(10);
            Destroy(other.gameObject);
        }
    }
}
