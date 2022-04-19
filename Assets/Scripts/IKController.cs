using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    // Where it is going to try to move our hand to
    public Transform rightHandPoint;
    public Transform leftHandPoint;
    // Weight - How much it blends between going to that point and staying with out current animation
    public float rightHandWeight;
    public float leftHandWeight;

    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnAnimatorIK(int layerIndex)
    {
        // TODO: Check that we have a weapon, becaus eif no weapon, no IK

        // However, if weapon, then use the rightHandPoint and leftHandPoint 
        //  NOTE: In the game itself, these will come from the weapon. The weapon tells you it's points

        // Tell the animator to move the hand to the position and rotation, not telling it how much (weight)
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandPoint.position);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPoint.position);

        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandPoint.rotation);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandPoint.rotation);

        //Tells it how much (weight)
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);

        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);


    }
}
