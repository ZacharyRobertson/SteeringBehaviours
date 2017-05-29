using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

public class Wander : SteeringBehaviour
{
    public float radius = 1.0f;
    public float offset = 1.0f;
    public float jitter = 0.2f;

    private Vector3 targetDir;
    private Vector3 randomDir;
    public override Vector3 GetForce()
    {
        //Set force to zero
        Vector3 force = Vector3.zero;
        // Create a random Range between -half max value to half max value
        float randX = Random.Range(0, 0x7fff) - (0x7fff * 0.5f);
        float randZ = Random.Range(0, 0x7fff) - (0x7fff * 0.5f);

        #region RandomDir
        // Set randomDir to new Vector 3 x = randX & z = randZ
        randomDir = new Vector3(randX, 0, randZ);
        // Set randomDir to normalized randomDir
        randomDir = randomDir.normalized;
        // Set randomDir to randomDir  x jitter
        randomDir = randomDir * jitter;
        #endregion

        #region Calculate TargetDir
        // Set targetDir to targetDir + randomDir
        targetDir = targetDir + randomDir;
        // Set targetDir to normalized targetDir
        targetDir = targetDir.normalized;
        // Set targetDir to targetDir to targetDir x radius
        targetDir = targetDir * radius;
        #endregion

        #region Calculate Force
        // Set SeekPos to owner's position + targetDir
        Vector3 seekPos = owner.transform.position + targetDir;
        // Set SeekPos to seekPos + owner's forward x offset
        seekPos = seekPos + owner.transform.forward * offset;
        #region Gizmos
        // Set OffsetPos to position + forward x offset
        Vector3 offsetPos = transform.position + transform.forward.normalized * offset;
        // Add circle with offsetPos + up x amount
        GizmosGL.AddCircle(offsetPos + Vector3.up * 0.1f, radius, Quaternion.LookRotation(Vector3.down), 16, Color.red);
        GizmosGL.AddCircle(seekPos + Vector3.up * 0.15f, radius * 0.6f, Quaternion.LookRotation(Vector3.down), 16, Color.blue);
        #endregion
        // Set DesiredForce to seekPos - position
        Vector3 desiredForce = seekPos - owner.transform.position;
        // If desiredForce != zero
        if (desiredForce != Vector3.zero)
        {
            // Set desiredForce to desiredForce normalized * weighting
            desiredForce = desiredForce.normalized * weighting;
            // Set force to desiredForce - owner's velocity
            force = desiredForce - owner.velocity;
        }
        #endregion
        return force;
    }
}
