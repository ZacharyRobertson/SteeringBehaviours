﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AIAgent))]
public class SteeringBehaviour : MonoBehaviour
{
    public float weighting = 7.5f;
    
    [HideInInspector]
    public AIAgent owner;

    // Use this for initialization
    void Start()
    {
        owner = GetComponent<AIAgent>();
    }

    public virtual Vector3 GetForce()
    {
        return Vector3.zero;
    }
}
