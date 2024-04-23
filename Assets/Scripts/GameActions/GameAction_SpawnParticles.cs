using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAction_SpawnParticles : GameAction
{
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private float lifespan;
    [SerializeField] private Transform target;

    public void SpawnParticles()
    {
        GameObject particles = Instantiate<GameObject>(particlePrefab, target.position, target.rotation);

        //set to destroy if lifespan exists
        if (lifespan > 0)
        {
            Destroy(particles, lifespan);
        }
    }
}
