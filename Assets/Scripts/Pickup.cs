using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Pickup : MonoBehaviour
{
    public UnityEvent OnSpawn;
    public UnityEvent OnPickup;

    // Start is called before the first frame update
    public virtual void Start()
    {
        OnSpawn.Invoke();
    }

    // Update is called once per frame
    public abstract void Update();

    public virtual void OnTriggerEnter(Collider other)
    {
        //Make sure only the player can pickup pickups
        Pawn otherPawn = other.GetComponent<Pawn>();
        if( otherPawn != null)
        {
            OnPickup.Invoke();
            //After pickup, destroy itself
            Destroy(gameObject);
        }
    }
}
