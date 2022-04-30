using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float timeBetweenSpawns;
    private float countDown;
    private GameObject spawnedObject;


    // Start is called before the first frame update
    void Start()
    {
        countDown = timeBetweenSpawns;

        //Turn on and off ragdoll
        /*Rigidbody[] allTheRigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in allTheRigidBodies)
        {
            rb.isKinematic = false;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedObject != null)
        {
            // Do nothing
            return;
        }
        else
        {
            // Countdown 
            countDown -= Time.deltaTime;
            // If countdown <0, then spawn the time and reset the countdown
            if (countDown <= 0)
            {
                spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation) as GameObject;
                countDown = timeBetweenSpawns;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        //New quardinate system, 0, rotation, that setup
        Matrix4x4 MyNewCordinateSystem = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = Color.Lerp(Color.red, Color.clear, 0.7f);
        
        Gizmos.matrix = MyNewCordinateSystem;
        Gizmos.DrawCube(new Vector3(0, 1, 0), new Vector3(1, 2, 1));
    }
}
