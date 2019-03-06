using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverController : MonoBehaviour
{

    private Rigidbody rigidBody;

    public float speed = 90f;
    public float turnSpeed = 5f;
    public float hoverForce = 65f;
    public float hoverHeight = 3.5f;

    private float powerInput;
    private float turnInput;

    private float force;
    private float torque;

    public float maxSpeed = 10;
    
    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

        //set max rotation velocity
        rigidBody.maxAngularVelocity = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        powerInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        //create brake
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.velocity = rigidBody.velocity * 0.9f;
        }
    }

    void FixedUpdate()
    {

        //create ray facing down
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        //push up if ray hits ground
        if(Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            //apply force ignoring mass 
            rigidBody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }

        force = powerInput * speed;
        torque = turnInput * turnSpeed;

        //add force from input
        rigidBody.AddRelativeForce(0f, 0f, force);
        rigidBody.AddRelativeTorque(0f, torque, 0f);

        //set max speed
        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }

        
        //reduce drag when rotating in the oposite direction
        if((rigidBody.angularVelocity.y > 0 && turnInput < 0))
        {
            rigidBody.AddRelativeTorque(0f, -0.9f, 0f);
        }
        else if((rigidBody.angularVelocity.y < 0 && turnInput > 0))
        {
            rigidBody.AddRelativeTorque(0f, 0.9f, 0f);
        }
        
    }
}
