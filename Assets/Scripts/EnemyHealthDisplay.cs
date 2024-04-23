using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthDisplay : MonoBehaviour
{
    public Health enemyHealth;
    public Image enemyHealthCircle;
    
    void Update()
    {
        if (enemyHealth != null)
        {
            if (enemyHealthCircle != null)
            {
                enemyHealthCircle.fillAmount = enemyHealth.currentHealth / enemyHealth.maxHealth;
            }
        }        
    }
}
