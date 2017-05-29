using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteeringBehaviour
{

    public Transform target;
    public float safeDistance = 0f;
    public Vector3 desiredForce;

    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;
        
        // IF there is no target, then return force
        if (target == null)
        {
            return force;
        }
        // SET desiredForce to direction from position to target
        desiredForce = transform.position - target.position;
        // SET desiredForce y to zero
        desiredForce.y = 0;

        // IF direction is lesser than safe distance
        if (desiredForce.magnitude < safeDistance)
        {
            // SET desiredForce to normalized and multiply by weighting
            desiredForce = desiredForce.normalized * weighting;
            // SET force to desiredForce and subtract owner's velocity
            force = desiredForce - owner.velocity;
        }
        return force;
    }
}
