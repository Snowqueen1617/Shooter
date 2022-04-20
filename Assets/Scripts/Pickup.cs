using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Pickup : MonoBehaviour
{
    public UnityEvent OnSpawn;
    public UnityEvent OnPickup;

    // Start is called before the first frame update
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();
}
