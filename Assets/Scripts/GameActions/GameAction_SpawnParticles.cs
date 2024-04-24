using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAction_SpawnParticles : GameAction
{
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private float lifespan;
    [SerializeField] private Transform target;
    [Tooltip("Set this if these particles should be parented somewhere")]
    [SerializeField] private Transform parent;

    public void SpawnParticles()
    {
        GameObject particles = Instantiate<GameObject>(particlePrefab, target.position, target.rotation);
        if(parent != null) particles.transform.parent = parent;

        //set to destroy if lifespan exists
        if (lifespan > 0)
        {
            Destroy(particles, lifespan);
        }
    }
}
