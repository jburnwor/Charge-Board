using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotationController : MonoBehaviour
{

    //private Rigidbody characterRB;

    private Camera mainCamera;

    public GunController theGun;

    // Start is called before the first frame update
    void Start()
    {
        //characterRB = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //create ray from camera to where the mouse is pointing
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        //if ray hits the ground plane
        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            //get point of intersection
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            //make character look at the point
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        if (Input.GetMouseButtonDown(0))
        {
            theGun.isFiring = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            theGun.isFiring = false;
        }
    }
}
