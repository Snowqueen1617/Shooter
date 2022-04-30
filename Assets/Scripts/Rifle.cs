using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileMoveSpeed;
    public float projectileLifespan;

    // Start is called before the first frame update
    public override void Start()
    {
        //Run the start function from the parent class
        base.Start(); 
    }

    // Update is called once per frame
    public override void Update()
    {
        //Run the update function from the parent class
        base.Update();
    }

    public void ShootBullets()
    {
        // Instantiate bullets at the fire location of this rifle -- the sphere will handle the rest
        Debug.Log("Pew Pew Pew");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation) as GameObject;
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        //Transfer important information like damage done to the sphere
        if (projectile != null)
        {
            // The projectile (bullet) layer is the same as the pawn layer
            projectile.layer = gameObject.layer;

            projectileScript.damageDone = damageDone;
            projectileScript.lifespan = projectileLifespan;
            projectileScript.moveSpeed = projectileMoveSpeed;
        }
        // -- the sphere will handle the rest

    }
}
