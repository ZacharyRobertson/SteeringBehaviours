using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public Transform target;
    public float stoppingDistance = 0f;

    public override Vector3 GetForce()
    {
        Vector3 desiredForce;

        // IF there is no target, then return force
        if (target == null)
        {
            return force;
        }
        // SET desiredForce to direction from target to position
        desiredForce = transform.position - target.position;
        // SET desiredForce y to zero
        desiredForce.y = 0;

        // IF direction is greater than stopping distance
        if (desiredForce.magnitude > stoppingDistance)
        {
            // SET desiredForce to normalized and multiply by weighting
            desiredForce = desiredForce.normalized * weighting;
            // SET force to desiredForce and subtract owner's velocity
            force = desiredForce - owner.velocity;
        }
        return force;
    }
}
