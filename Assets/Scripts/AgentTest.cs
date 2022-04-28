using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentTest : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform targetTransform;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(targetTransform.position);
        // Get the target velocity of the navmesh agent
        Vector3 desiredVelocity = agent.desiredVelocity;
         // Set from world direction to local
         desiredVelocity = transform.InverseTransformDirection(desiredVelocity);

        // TODO: Change this to use the pawns speed - so it is exactly like the player
        float tempSpeedDeleteMe = 3;
        desiredVelocity = desiredVelocity.normalized * tempSpeedDeleteMe;
        // Pass this in to the animator
        anim.SetFloat("Forward", desiredVelocity.z );
        anim.SetFloat("Right", desiredVelocity.x);
        // Rotate towards movement direction
        Quaternion rotationToMovementDirection = Quaternion.LookRotation(agent.desiredVelocity, Vector3.up);
        
        // TODO: Get rotation speed from pawn
        float maxRotateSpeedTempDELETEME = 360;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMovementDirection, maxRotateSpeedTempDELETEME * Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        // This runs everytime the animator causes us to move in position
        // Tell the navmeshagent taht we already moved (so it doesn't have to)
        agent.velocity = anim.velocity;
    }

}
