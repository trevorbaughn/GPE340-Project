using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WeaponAction_Pelletgun : WeaponAction
{
    [Header("Weapon Variables")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject _pelletPrefab;
    [SerializeField] private UnityEvent OnFire;

    private bool _isAutoAttackActive;
    
    
    public override void Awake()
    {
        base.Awake();
    }
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (_isAutoAttackActive)
        {
            Shoot();
        }
    }
    
    public void Shoot()
    {  
        // check if can shoot
        float secondsPerShot = 1/attackRate;
        if (Time.time >= lastAttackTime + secondsPerShot) {
            
            #region accuracy
            Vector3 newFireDirection = firepoint.forward; //technically oldDirection, but will be set to new

            // get rotation change based on accuracy
            Quaternion accuracyFireDelta = Quaternion.Euler(0, GetAccuracyRotationDegrees(weapon.owner.pawnController.hitAccuracy), 0);
        
            //Quaternion * Vector = actual new direction... order matters
            newFireDirection = accuracyFireDelta * newFireDirection;
            #endregion

            //instantiate a pellet child and get it's component, set layer and rotate for accuracy
            GameObject pellet = Instantiate(_pelletPrefab, firepoint.position, firepoint.rotation);
            pellet.gameObject.layer = this.gameObject.layer;
            pellet.transform.Rotate(0, GetAccuracyRotationDegrees(weapon.owner.pawnController.hitAccuracy), 0);
            
            Projectile pelletData = pellet.GetComponent<Projectile>();
            
            //set damage on projectile
            if (pelletData != null)
            {
                pelletData.damage = damageDone;
            }
            
            //has fired pelletgun
            OnFire.Invoke();
            
            lastAttackTime = Time.time;
        }
    }

    public void AutofireBegin()
    {
        _isAutoAttackActive = true;
    }

    public void AutofireEnd()
    {
        _isAutoAttackActive = false;

    }
}
