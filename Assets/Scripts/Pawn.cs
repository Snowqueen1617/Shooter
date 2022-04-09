using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    private Animator anim;
    public float moveSpeed = 1; //Meters per second
    public float rotateSpeed = 1; //Degrees per second

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move ( Vector3 moveVector)
    {
        //Make sure keyboards aren't faster than joysticks
        //moveVector = moveVector.ClampMagnitude01(moveVector);
        
        // Apply Speed //move vector is our controller position
        moveVector = moveVector * moveSpeed;

        //Send parameters in to animator
        anim.SetFloat("Right", moveVector.x);
        anim.SetFloat("Forward", moveVector.z);
    }

    public void RotateTowards ( Vector3 targetPoint)
    {
        // Find the rotation that would be looking at that target point
        //Find the Vector to the point (end - start point)
        Vector3 targetVector = targetPoint - transform.position;

        // Find rotation down that vector
        Quaternion targetRotation = Quaternion.LookRotation(targetVector, Vector3.up);

        // change my rotation (slowly) towards that targeted rotation
        transform.rotation = Quaternion.RotateTowards( transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

}
