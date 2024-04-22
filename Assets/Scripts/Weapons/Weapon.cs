using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [Header("Events")] 
    public UnityEvent OnPrimaryAttackBegin;
    public UnityEvent OnPrimaryAttackEnd;
    public UnityEvent OnSecondaryAttackBegin;
    public UnityEvent OnSecondaryAttackEnd;

    [Header("Inverse Kinematics Targets")]
    public Transform RightHandIKTarget;
    public Transform LeftHandIKTarget;

}
