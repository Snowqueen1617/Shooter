using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponPickup : Pickup
{
    public GameObject weaponToPickup;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
    }

    public override void OnTriggerEnter(Collider other)
    {
        Pawn otherPawn = other.GetComponent<Pawn>();
        if(otherPawn != null)
        {
            otherPawn.EquipWeapon(weaponToPickup);
        }

        base.OnTriggerEnter(other);
    }
}
