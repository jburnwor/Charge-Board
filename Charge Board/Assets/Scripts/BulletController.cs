using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float speed;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Environment")
        {

            Debug.Log("destroy");
            Destroy(gameObject);
            
        }
    }
}
