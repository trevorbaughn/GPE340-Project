using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    private Health _health;
    public PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnSpawn()
    {
        _health = player.controlledPawn.GetComponent<Health>();
        _health.OnTakeDamage.AddListener(UpdateHealthBar);

        UpdateHUD();
    }

    public void UpdateHUD()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = _health.currentHealth / _health.maxHealth;
    }
}
