using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogdollController : MonoBehaviour
{
    public bool isRagdoll;

    private Rigidbody mainRigidbody;
    private Collider mainCollider;
    private Animator animator;
    private List<Rigidbody> ragdollRigidbodies;
    private List<Collider> ragdollColliders;


    // Start is called before the first frame update
    void Start()
    {
        // Load all variables
        mainRigidbody = GetComponent<Rigidbody>();
        mainCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        // Grabs all of the children and current object's Collider & Rigidbody
        ragdollColliders = new List<Collider>(GetComponentsInChildren<Collider>());
        ragdollRigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());

        //Removes the Main Rigidbody & Collider
        ragdollRigidbodies.Remove(mainRigidbody);
        ragdollColliders.Remove(mainCollider);


        // Start in correct Ragdoll Setting
        if (isRagdoll)
        {
            ActivateRagdoll();
        }
        else
        {
            DeactivateRagdoll();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TEST COD -- DELETE ME!
        if(Input.GetKeyDown(KeyCode.P))
        {
            ToggleRagdoll();
        }
        // END TEST CODE -- DELETE ME!
    }

    public void ToggleRagdoll()
    {
        isRagdoll = !isRagdoll;
        
        if (isRagdoll)
        {
            ActivateRagdoll();
        }
        else
        {
            DeactivateRagdoll();
        }
    }

    public void ActivateRagdoll()
    {
        // Turn ON ALL of the ragdoll colliders
        foreach(Collider collider in ragdollColliders)
        {
            collider.enabled = true;
        }

        // Turn ON ALL of the ragdoll rigidbodies
        foreach(Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }

        // Turn OFF the main collider
        mainCollider.enabled = false;
        // Turn OFF Animator
        animator.enabled = false;
        // Turn OFF main rigidbody
        mainRigidbody.isKinematic = true;
    }

    public void DeactivateRagdoll()
    {
        // Turn OFF ALL of the ragdoll coliders
        foreach(Collider collider in ragdollColliders)
        {
            collider.enabled = false;
        }
        // Turn OFF ALL of the ragdoll rigidbodies
        foreach(Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }

        // Turn ON main collider
        mainCollider.enabled = true;
        // Turn ON Animator
        animator.enabled = true;
        // Turn ON main rigidbody
        mainRigidbody.isKinematic = false;
    }
}
