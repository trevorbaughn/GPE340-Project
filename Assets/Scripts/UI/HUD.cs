using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthBarTextBox;
    public Image[] lifeIcons;
    
    private Health _health;
    
    public Image weaponIconImage;

    void Update()
    {
        if (GameManager.instance.player != null
            && GameManager.instance.player.controlledPawn != null
            && GameManager.instance.player.controlledPawn.weapon != null
            && GameManager.instance.player.controlledPawn.weapon.weaponIcon != null)
        {
            //if holding weapon with icon
            weaponIconImage.enabled = true;
            weaponIconImage.sprite = GameManager.instance.player.controlledPawn.weapon.weaponIcon;
        } else
        {
            //else
            weaponIconImage.enabled = false;
        }
    }

    public void OnSpawn()
    {
        _health = GameManager.instance.player.controlledPawn.GetComponent<Health>();
        _health.OnTakeDamage.AddListener(UpdateHealthBar);
        _health.OnHealDamage.AddListener(UpdateHealthBar);
        _health.OnDeath.AddListener(UpdateLifeIcons);

        UpdateHUD();
    }

    public void UpdateHUD()
    {
        UpdateHealthBar();
        TurnOffAllLifeIcons();
        TurnOnLifeIcons();
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = _health.currentHealth / _health.maxHealth;
        healthBarTextBox.text = _health.currentHealth + " / " + _health.maxHealth;
    }

    void UpdateLifeIcons()
    {
        TurnOffAllLifeIcons();
        TurnOnLifeIcons();
    }
    
    private void TurnOffAllLifeIcons()
    {
        for (int i = 0 ; i < lifeIcons.Length ; i++)
        {
            lifeIcons[i].enabled = false;
        }
    }
    
    private void TurnOnLifeIcons()
    {
        //for each icon by index
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            //if the index is less than the current number of lives, enable the icon at that index
            if (i < GameManager.instance.player.lives)
            {
                lifeIcons[i].enabled = true;
            }
        }
    }
    
}
