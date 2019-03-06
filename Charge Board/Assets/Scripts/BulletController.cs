using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{



    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Environment")
        {

            Debug.Log("destroy");
            Destroy(gameObject);
            
        }
    }
}
