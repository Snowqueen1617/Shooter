using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent OnShoot;
    public UnityEvent OnPullTrigger;
    public UnityEvent OnReleaseTrigger;
    public UnityEvent OnAlternateAttackStart;
    public UnityEvent OnAlternateAttackEnd;
    public UnityEvent OnReload;
    [Header("States")]
    public bool isAutoFiring;
    [Header("Data")]
    public float fireDelay; // Seconds between shots
    private float countDown; 

    // Start is called before the first frame update
    public virtual void Start()
    {
        countDown = fireDelay;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Subtract the time it took to play the last frame from our countdown
        countDown -= Time.deltaTime;

        if (isAutoFiring && countDown <= 0)
        {
            //Shoot
            Shoot();
            // Reset the timer
            countDown = fireDelay;
        }
    }

    public void Shoot()
    {
        OnShoot.Invoke();
    }

    public void StartAutoFire()
    {
        isAutoFiring = true;
    }

    public void EndAutoFire()
    {
        isAutoFiring = false;
    }

    public void ToggleAutoFire()
    {
        isAutoFiring = !isAutoFiring;
    }
}
