﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public Vector3 force;
    public Vector3 velocity;
    public float maxVelocity = 100f;

    private List<SteeringBehaviour> behaviours;

    // Use this for initialization
    void Start()
    {
        // This is Scott
        SteeringBehaviour[] ohBehave = GetComponents<SteeringBehaviour>();
        behaviours = new List<SteeringBehaviour>(ohBehave);
    }

    // Update is called once per frame
    void Update()
    {
        ComputeForces();
        ApplyVelocity();
    }

    void ComputeForces()
    {
        // Reset the force before computing
        force = Vector3.zero;
        // FOR each behaviour attached to AIAgent
        foreach (SteeringBehaviour ohBehave in behaviours)
        {
            // IF behavior is not active
            if (!isActiveAndEnabled)
            {
                // CONTINUE to next behavior
                continue;
            }
            // Append force from behavior (multiply by behaviour weighting)
            ;
            // IF forces are too big
            if (force.magnitude > maxVelocity)
            {
                // Clamp force to the max velocity
                Mathf.Clamp(force.magnitude, 0, maxVelocity);
            }
            // EXIT function
            return;
        }
    }
    
    void ApplyVelocity()
    {
        // Append force to velocity with deltaTime
        velocity += force * Time.deltaTime;
        // IF velocity is greater than max vel
        if (velocity.magnitude > maxVelocity)
        {
            // Clamp velocity
            Mathf.Clamp(velocity.magnitude, 0, maxVelocity);
        }
        // IF velocity is not zero
        if(velocity.magnitude != 0)
        {
            // velocity.magnitude > 0
            if (velocity.magnitude > 0)
            {
                // Append transform position by velocity
                transform.position += velocity;
                // transform rotate by velocity
                transform.Rotate(velocity);
            }
        }

    }
}