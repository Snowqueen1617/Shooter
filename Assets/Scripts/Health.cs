using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //Event Systems can be used on EVERYTHING

    public Text text;

    [Header("Values")]
    public float maxHealth;
    public float currentHealth;
    [Header("Events")]
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDie;
    public UnityEvent OnHeal;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        // Do whatever I need to do to find the cube
        //LameExplosion exploderToUse = GameObject.FindObjectOfType<LameExplosion>;
        // Avoid using Find, REALLY bad on the processor, unless no other choice, should just pull off the game manager
        //LameExplosion exploderToUse = GameObject.Find("cube").GetComponent<LameExplosion>();
        //OnDie.AddListener(exploderToUse.Explode);
        //OnDie.RemoveAllListeners();
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }*/

    public void Heal( float amountToHeal)
    {
        currentHealth += amountToHeal;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        OnHeal.Invoke();
    }

    public void TakeDamage( float amountOfDamage)
    {
        // Call the OnTakeDamage Event
        OnTakeDamage.Invoke();


        // Subtract Health
        currentHealth -= amountOfDamage;
    
        // If our health <= 0, we die
        if ( currentHealth <= 0)
        {
            // Call Die Event
            OnDie.Invoke();
        }
        else
        {
            // Don't go over max health
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            //HealthDisplay(currentHealth);
        }
    }

    /*public void HealthDisplay( float currentHealth)
    {
        text.text = "Health " + currentHealth.ToString() + ".";
    }*/
}
