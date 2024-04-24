using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;
    public UnityEvent OnHealDamage;

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

    public void HealDamage(float toHeal)
    {
        currentHealth = Mathf.Clamp(currentHealth + toHeal, 0, maxHealth);
        OnHealDamage.Invoke();
    }
}
