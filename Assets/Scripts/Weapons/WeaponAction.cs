using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponAction : GameAction
{
    protected Weapon weapon;
    
    [Header("Variables")] 
    [SerializeField] protected float damageDone;
    [SerializeField] protected float attackRate;
    protected float lastAttackTime;

    public override void Awake()
    {
        weapon = GetComponent<Weapon>();
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
}
