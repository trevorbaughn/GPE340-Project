using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;

    public float maxHealth;
    public float currentHealth;

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth); 
        OnTakeDamage.Invoke();

        //death
        if (currentHealth <= 0)
        {
            OnDeath.Invoke();
        }
    }
}
