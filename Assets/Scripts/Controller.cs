using UnityEngine;

public class Controller : MonoBehaviour
{
    public Pawn pawn;
    public Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        //make sure the camera is loaded
        if (playerCamera == null) Debug.LogWarning("ERROR: No Camera Set!");
    }

    // Update is called once per frame
    void Update()
    {
        //Send my move command to my pawn - get joystick position
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        // Adjust this move vector, so that it is based on the direction the character is facing --
                // Find out what "local" (Forward/Right) would move me in the world (north/south) that matches my input
        moveVector = pawn.transform.InverseTransformDirection(moveVector);
        
        // Limit the vectr magnetude to 1
        moveVector = Vector3.ClampMagnitude(moveVector, 1.0f);

        //Tell the pawn to move
        pawn.Move(moveVector);

        //Rotate player to face mouse
        RotateToMouse();
        
    }

    void RotateToMouse()
    {
        // Create a plane object (a mathmatical represenattion of all the points in 2D)
        Plane groundPlane;

        //Set that plane so that it is X,Z plane the player is standing on
        groundPlane = new Plane(Vector3.up, pawn.transform.position);

        //Cast a ray from the camera toward the plane, through our mouse cursor
        float distance;
        Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        groundPlane.Raycast(cameraRay,  out distance);

        //Find where that ray hits the plane
        Vector3 raycastPoint = cameraRay.GetPoint(distance);

        //Rotate towward that point
        pawn.RotateTowards(raycastPoint);
    }
}
