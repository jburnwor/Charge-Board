using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using Complete;

public class StateController : MonoBehaviour {

    //state controller variables
    public State currrentState;
	public State remainState;
    [HideInInspector] public bool playAnim;
    [HideInInspector] public bool freezeTime;
    [HideInInspector] public Animator anim;
    public float lookRadius = 15f;
    public float minRadius = 1f;
    public float health;
    [HideInInspector] float timerTime = 2f;
    [HideInInspector] float time;

    [HideInInspector] GameObject player;                          // Reference to the player GameObject.

    //attack state variables
    [HideInInspector] public bool tookDamage;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Transform chaseTarget;

    //shooting variables
    public Rigidbody bullet;
    public float bulletSpeed;

    public float timeBetweenShots;
    [HideInInspector] public float shotCounter;

    public Transform firePoint;

    [HideInInspector] public Rigidbody RB;

    public ParticleSystem hit;


    void Awake () 
	{
        player = GameObject.FindGameObjectWithTag("Player");
        RB = gameObject.GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent> ();
        anim = GetComponentInChildren<Animator>();

        chaseTarget = player.transform;

        freezeTime = false;

        health = 100f;
    }

    void Update()
    {

        if(health <= 0)
        {
            GameManager.enemiesLeft--;
            Debug.Log(GameManager.enemiesLeft);

            Destroy(this.gameObject);
        }
 
        currrentState.UpdateState(this);
       
    }
    
	public void TransitionToState(State nextState)
	{
		//if the next state isnt current state, change state
		if(nextState != remainState)
		{
			currrentState = nextState;
		}
	}


    public void FaceTarget()
    {

        Vector3 direction = (chaseTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, minRadius);
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("enemy took damage");

        health -= amount;
        tookDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet")
        {
            health -= 5f;

            Instantiate(hit, transform.position, Quaternion.identity);
        }
        else if (other.tag == "Player")
        {

            if (!GameManager.lockControls)
            {
                
            }
            else
            {

            }
            
        }
    }
}