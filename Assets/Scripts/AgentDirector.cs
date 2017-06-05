using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

public class AgentDirector : MonoBehaviour
{
    public Transform selectedTarget;
    public float rayDistance = 1000f;
    public LayerMask selectionLayer;
    private AIAgent[] agents;
    // Use this for initialization
    void Start()
    {
        // Set Agents to FindObjectsOfType AiAgent
        agents = FindObjectsOfType<AIAgent>();
    }

    // Apply selected targets to all agents
    void ApplySelection()
    {
        // Set Agents to FindObjectsOfType AiAgent
        agents = FindObjectsOfType<AIAgent>();
        // FOREACH agent in agents
        foreach (AIAgent agent in agents)
        {
            // Set path following to agent.GetComponent<PathFollowing>();
            PathFollowing pathFollowing = agent.GetComponent<PathFollowing>();
            // If pathFollowing is not null
            if(pathFollowing != null)
            {
                //Set pathFollowing.target to selected Target
                pathFollowing.target = selectedTarget;
                // Call pathFollowing.UpdatePath()
                pathFollowing.UpdatePath();
            }
        }
    }

    // Constantly checking inputs
    void CheckSelection()
    {
        // Set ray to cray from camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Set hit to new RaycastHit
        RaycastHit hit = new RaycastHit();
        // If Physics.Raycast() and pass ray, out hit, ray distance, selectionLayer
        if (Physics.Raycast(ray, out hit, rayDistance, selectionLayer))
        {
            // Call GizmosGL.AddSphere() and pass hit.point, 5f, Quaternion.identity, any color
            GizmosGL.AddSphere(hit.point, 5f, Quaternion.identity, Color.black);
            // If user clicks left mouse button
            if(Input.GetMouseButtonDown(0))
            {
                // Set selectedTarget to hit.collider.transform
                selectedTarget = hit.collider.transform;
                // Call ApplySelection()
                ApplySelection();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Call CheckSelection()
        CheckSelection();
    }
}
