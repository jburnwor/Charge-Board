using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public float holdTime;
    float maxVelocity = 5f;
    float chargeVelocity = 20f;

    Camera mainCamera;

    public Vector3 lookToPoint;

    CharacterRotationController charRot;

    float time;
    float timerTime;

    public Image chargeBar;

    public ParticleSystem chargedParticles;


    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

        mainCamera = FindObjectOfType<Camera>();

        charRot = GetComponent<CharacterRotationController>();
        //set max rotation velocity
        rigidBody.maxAngularVelocity = maxVelocity;

        timerTime = 2f;

        holdTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

        chargeBar.fillAmount = (holdTime * 2)/100;

        if (!GameManager.lockControls)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                holdTime++;

                rigidBody.velocity = rigidBody.velocity * 0;

                //create ray from camera to where the mouse is pointing
                Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                float rayLength;

                //if ray hits the ground plane
                if (groundPlane.Raycast(cameraRay, out rayLength))
                {
                    //get point of intersection
                    Vector3 pointToLook = cameraRay.GetPoint(rayLength);

                    //make character look at the point
                    transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
                    lookToPoint = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);
                }

                GameManager.stopEnemies = true;
            }
            else
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
            
        }
        else
        {
            //lock controls while the board is charging
            time += Time.deltaTime;
            //Debug.Log(time);
            if (time >= timerTime)
            {
                time = 0;
                GameManager.lockControls = false;
                GameManager.stopEnemies = false;
                rigidBody.maxAngularVelocity = maxVelocity;
            }
        }

        //create particle when charge is ready
        if(holdTime > 50 && !(holdTime > 55))
        {
            Debug.Log("charge particle");
            Instantiate(chargedParticles, transform.position, Quaternion.identity);
        }
        
    }

    void FixedUpdate()
    {

        //create ray facing down
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        //push up if ray hits ground
        if (Physics.Raycast(ray, out hit, hoverHeight))
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
        if (!GameManager.lockControls)
        {
            rigidBody.AddRelativeTorque(0f, torque, 0f);
        }
        else
        {
            rigidBody.AddRelativeTorque(0f, 0f, 0f);
        }
        

        //set max speed
        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }


        //reduce drag when rotating in the oposite direction
        if ((rigidBody.angularVelocity.y > 0 && turnInput < 0))
        {
            rigidBody.AddRelativeTorque(0f, -0.9f, 0f);
        }
        else if ((rigidBody.angularVelocity.y < 0 && turnInput > 0))
        {
            rigidBody.AddRelativeTorque(0f, 0.9f, 0f);
        }



        if (!GameManager.lockControls)
        {
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //rotate hover board to mouse 
                Vector3 targetDelta = lookToPoint - transform.position;

                //get the angle between transform.forward and target delta
                float angleDiff = Vector3.Angle(transform.forward, targetDelta);

                // get its cross product, which is the axis of rotation to
                // get from one vector to the other
                Vector3 cross = Vector3.Cross(transform.forward, targetDelta);

                // apply torque along that axis according to the magnitude of the angle.
                rigidBody.AddTorque(cross * angleDiff * force);

            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {

                if (holdTime > 50)
                {
                    rigidBody.maxAngularVelocity = 20f;
                    //rigidBody.velocity = transform.forward * chargeVelocity;
                    rigidBody.AddRelativeForce(0f, 0f, 1000f);
                    GameManager.lockControls = true;

                }
                else
                {
                    GameManager.stopEnemies = false;
                }


                holdTime = 0;
            }
        }
        
        
    }

}
