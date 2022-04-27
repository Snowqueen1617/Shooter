using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Pawn pawn;
    Vector3 offset;
 
    void Start()
     {
        offset = transform.position - pawn.transform.position;
    }
 
    void LateUpdate()
    {
        float newXPosition = pawn.transform.position.x - offset.x;
        float newZPosition = pawn.transform.position.z - offset.z;
 
        transform.position = new Vector3(newXPosition, transform.position.y, newZPosition);
    }
}
