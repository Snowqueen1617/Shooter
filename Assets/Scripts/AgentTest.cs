using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentTest : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform targetTransform;
    private Animator anim;
    public Pawn pawn;

    private float timeForNextNavigationCheck;
    [SerializeField] private float timeBetweenNavigationCkecks = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set my Timer
        timeForNextNavigationCheck = Time.time + timeBetweenNavigationCkecks;
        //Load my components
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= timeForNextNavigationCheck)
        {
            //Do check
            agent.SetDestination(targetTransform.position);
            // Reset timer
            timeForNextNavigationCheck = Time.time + timeBetweenNavigationCkecks;        
        }

        // Get the target velocity of the navmesh agent
        Vector3 desiredVelocity = agent.desiredVelocity;
         // Set from world direction to local
         desiredVelocity = transform.InverseTransformDirection(desiredVelocity);

        // Change this to use the pawns speed - so it is exactly like the player
        //float tempSpeedDeleteMe = 3;
        desiredVelocity = desiredVelocity.normalized * pawn.moveSpeed;
        // Pass this in to the animator
        anim.SetFloat("Forward", desiredVelocity.z);
        anim.SetFloat("Right", desiredVelocity.x);
        // Rotate towards movement direction
        Quaternion rotationToMovementDirection = Quaternion.LookRotation(agent.desiredVelocity, Vector3.up);
        
        // Get rotation speed from pawn
        float maxRotateSpeed = pawn.rotateSpeed;
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMovementDirection, maxRotateSpeed * Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        // This runs everytime the animator causes us to move in position
        // Tell the navmeshagent taht we already moved (so it doesn't have to)
        agent.velocity = anim.velocity;
    }

}
