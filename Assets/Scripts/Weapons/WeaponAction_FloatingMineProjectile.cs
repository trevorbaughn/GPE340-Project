using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WeaponAction_FloatingMineProjectile : WeaponAction
{
    [Header("Weapon Variables")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject _pelletPrefab;
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

    }
    
    public void Shoot()
    {        
        // check if can shoot
        float secondsPerShot = 1/attackRate;
        if (Time.time >= lastAttackTime + secondsPerShot) {

            //instantiate a pellet child and get it's component
            GameObject pellet = Instantiate(_pelletPrefab, firepoint.position, firepoint.rotation);
            pellet.gameObject.layer = this.gameObject.layer;
            
            Projectile pelletData = pellet.GetComponent<Projectile>();
            
            //set damage on projectile
            if (pelletData != null)
            {
                pelletData.damage = damageDone;
                
                //TODO: make these look a bit nicer.
                //pellet.GetComponentInChildren<MeshRenderer>().enabled = false;
                pelletData.moveSpeed = 0; // shouldn't move
            }
            
            //has fired pelletgun
            OnFire.Invoke();
            
            
            lastAttackTime = Time.time;
        }
    }

}