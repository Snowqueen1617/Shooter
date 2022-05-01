using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public enum dropTypes { Random, Weighted, PercentWeighted };
    public dropTypes fropType;
    public RandomWeightedObject[] objectsToSpawn;
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
                spawnedObject = Instantiate(ChooseSpawnObject(), transform.position, transform.rotation) as GameObject;
                countDown = timeBetweenSpawns;
            }
        }
    }

    public GameObject ChooseSpawnObject()
    {
        // Var to hold spawn object
        GameObject objectToSpawn;

        // Create a second parallel array - this holds the cutoffs
        //      (where it changes to the next type).
        //      Thus, anything below this cutoff is the parallel weighted object

        //Cumulitive density function
        float[] CDFArray = new float[objectsToSpawn.Length];

        // Var to hold cumulative density (total of weights so far)
        float cumulativeDensity = 0;
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            // Add this object's weight, so we know where the cutoff is
            cumulativeDensity += objectsToSpawn[i].weight;
            // Store that in the CDF Array
            CDFArray[i] = cumulativeDensity;
        }

        // Choose a random number up to the max cutoff
        float rand = Random.Range(0.0f, cumulativeDensity);

        /**** Old One At A Time Method - Slower But Works

        // Look thorugh my CDF to find where our random number would fall == which CDF index would it fall under
        for(int i = 0; i < CDFArray.Length; i++)
        {
            if(rand < CDFArray[i])
            {
                objectToSpawn = objectsToSpawn[i].objectToSpawn;
                return objectToSpawn;
            }
        }
        ****/

        // binary search will look for that number
        int selectedIndex = System.Array.BinarySearch(CDFArray, rand);

        // If Selected Index is negative...
        if (selectedIndex < 0)
        {
            // It's not the exact value, we have to FLIP (bitwise not) the value to find the index we want
            selectedIndex = ~selectedIndex;
        }

        objectToSpawn = objectsToSpawn[selectedIndex].objectToSpawn;
        return objectToSpawn;
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
