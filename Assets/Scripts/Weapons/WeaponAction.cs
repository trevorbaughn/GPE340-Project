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
    [SerializeField] protected float maxAccuracyRotation;
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
    
    protected virtual float GetAccuracyRotationDegrees(float accuracyModifier = 1)
    {
        //random value between 0 and 1
        float accuracyDeltaPercentage = UnityEngine.Random.value;

        // return percentage between left and right of perfect angle
        return Mathf.Lerp(-maxAccuracyRotation, maxAccuracyRotation, accuracyDeltaPercentage) * accuracyModifier;
    }
}
