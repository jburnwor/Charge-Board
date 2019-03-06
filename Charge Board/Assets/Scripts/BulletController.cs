using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{



    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Environment")
        {

            Debug.Log("destroy");
            Destroy(gameObject);
            
        }
    }
}
