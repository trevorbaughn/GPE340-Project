using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponAction_Raygun : WeaponAction
{
    [Header("Weapon Variables")]
    [SerializeField] private float fireDistance; 
    [SerializeField] private Transform firepoint;

    private bool _isAutoAttackActive;
    private LineRenderer _lineRenderer;

    [SerializeField] private GameObject _laserPrefab;

    [SerializeField] private UnityEvent OnFire;
    
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
        RaycastHit hit;

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
            
            // raycast
            if (Physics.Raycast(firepoint.position, newFireDirection, out hit, fireDistance)) {
                
                // if hit and other has Health
                Health otherHealth = hit.collider.gameObject.GetComponent<Health>();
                if ( otherHealth != null) {
                    otherHealth.TakeDamage(damageDone); //take damage
                }
            }

            //instantiate a laser child, get component, and set positions
            LaserBeam laser = Instantiate(_laserPrefab, this.transform).GetComponent<LaserBeam>();
            laser.startPoint = firepoint.position;
            laser.endPoint = laser.startPoint + (newFireDirection * fireDistance);
            
            //has fired raygun
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
