using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastRifle : Weapon
{
    public Transform firePoint;
    public GameObject muzzleFlashParticlePrefab;
    public GameObject hitTargetParticlePrefab;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public void RaycastShoot()
    {
        // Instantiate Particle at Forepoint
        Instantiate(muzzleFlashParticlePrefab, firePoint.position, firePoint.rotation);

        // Raycast to see if anything is in front of our gun
        // If so...
        RaycastHit hitInfo;
        if (Physics.Raycast(firePoint.position, firePoint.transform.forward, out hitInfo))
        {
            // instantiate the particle effect at the hit point
            Vector3 hitPoint = hitInfo.point;
            Instantiate(hitTargetParticlePrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            // Check if that object we hit has a health component -- if so:
            Health otherHealth= hitInfo.collider.gameObject.GetComponent<Health>();
            // Make it take damage
            if(otherHealth != null)
            {
                otherHealth.TakeDamage(damageDone);
            }
        }
            
    }
}
