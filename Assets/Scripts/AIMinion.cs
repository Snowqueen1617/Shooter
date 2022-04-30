using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMinion : AIController
{
    private Pawn pawn;
    private NavMeshAgent agent;

    private PlayerController player;

    public float maxShootingError = 1f;
    public float maxShootingDistance = 5f;
    public float minShootingDistance = 1f;
    public float leadModifier = 1f;
    public float noLeadDistance = 0.0f; // Use 0% of lead modifier
    public float maxLeadDistance = 25f; // use 100% of lead modifier
    private Vector3 leadvector; // how far in front of(or otherwise away from) player to lead

    public override void Awake()
    {
        // Load components
        pawn = GetComponent<Pawn>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        if (player == null)
        {
            FindPlayer();
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if (player == null)
        {
            FindPlayer();
        }
        else
        {
            SetLeadVector();
            MoveToPlayer();
            RotateTowardsPlayer();
            if (pawn.weapon != null)
            {
                ShootAtPlayer();
            }
        }
    }

    public void RotateTowardsPlayer()
    {
         // Find vector to player
        Vector3 vectorToPlayer = (player.pawn.transform.position + leadvector) - pawn.transform.position;
        // Find Quaternion that is looking down that vector
        Quaternion lookRotation = Quaternion.LookRotation(vectorToPlayer, Vector3.up);
        // Rotate a little bit towards that rotation per frame drop
        pawn.transform.rotation = Quaternion.RotateTowards(pawn.transform.rotation, lookRotation, pawn.rotateSpeed * Time.deltaTime);
    }

    public void SetLeadVector()
    {
        // Find distance to player
        float distanceToPlayer = Vector3.Distance(player.transform.position, pawn.transform.position);

        // Clamp that distance between our zero lead and max lead distances
        distanceToPlayer = Mathf.Clamp(distanceToPlayer, noLeadDistance, maxLeadDistance);

        //dTP = distance to player, how far away I am from the beginning
        float dTPFromMin = distanceToPlayer - noLeadDistance;

        //How far that entire lead distance range is, max - min
        float totalLeadDistanceRange = maxLeadDistance - noLeadDistance;

        // what percent of distance range, how far I am out of the total
        float percentOfDistanceRange = dTPFromMin / totalLeadDistanceRange;

        // now that I have that range, I can multiply my lead modifier by it
        // Find a few "seconds" in front of the player
        leadvector = player.pawn.anim.velocity * (leadModifier * percentOfDistanceRange);
    }

    public void ShootAtPlayer()
    {
        // Distance to player is the distance from my player to my current pawn
        float distanceToPlayer = Vector3.Distance(player.transform.position, pawn.transform.position);

        if (distanceToPlayer > minShootingDistance && distanceToPlayer < maxShootingDistance)
        {
            // Allows the AI to miss their target by a certain amount, randomly
            float shootingError = Random.Range(-maxShootingError * 0.5f, maxShootingError * 0.5f);
            // Rotates the error amount of the Y-Axis
            pawn.weapon.transform.Rotate(0, shootingError, 0);
            // Shoot
            pawn.weapon.Shoot();
            // Rotates the weapon back to normal posiiton
            pawn.weapon.transform.Rotate(0, -shootingError, 0);
        }
    }

    public void FindPlayer()
    {
        // Searches through the heiarchy, go through all components till it finds the type -- PlayerController
        player = FindObjectOfType<PlayerController>();
    }


    public void MoveToPlayer()
    {
        // Set the player as the destination
        agent.SetDestination(player.pawn.transform.position + leadvector);

        // Find the Vector of the path
        Vector3 desiredMovement = agent.desiredVelocity;

        // Set it to local direction (forward/right) not world direction (noth/west)
        desiredMovement = pawn.transform.InverseTransformDirection(desiredMovement);

        // Adjust it for our pawn speed
        desiredMovement = desiredMovement.normalized * pawn.moveSpeed;

        // Send to animator
        pawn.anim.SetFloat("Forward", desiredMovement.z);
        pawn.anim.SetFloat("Right", desiredMovement.x);
    }

    public void OnAnimatorMove()
    {
        // Tell the agent that the animator moved us (so it doesnt have to
        agent.velocity = pawn.anim.velocity;
    }

}
