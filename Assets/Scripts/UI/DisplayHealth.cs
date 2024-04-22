using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Health health;
    
    // Start is called before the first frame update
    void Start()
    {
        health.OnTakeDamage.AddListener(UpdateHealthBar);
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = health.currentHealth / health.maxHealth;
    }
}
