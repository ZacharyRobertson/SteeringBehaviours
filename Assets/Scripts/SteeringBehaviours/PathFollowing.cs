using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

public class PathFollowing : SteeringBehaviour
{
    public Transform target;
    // distance to current node
    public float nodeRadius = 5f;
    // Distance to end node
    public float targetRadius = 3f;
    private Graph graph;
    private int currentNode = 0;
    private bool isAtTarget = false;
    private List<Node> path;

    // Use this for initialization
    void Start()
    {
        // Set graph to FindObjectOfType Graph
        graph = FindObjectOfType<Graph>();
        // If graph is null
        if (graph == null)
        {
            // Call Debug.LogError() and pass error message
            Debug.LogError("There's an error or something Manny, this is probably your fault");
            // Call Debug.Break() (pauses the editor)
            Debug.Break();
        }
    }


    public void UpdatePath()
    {
        // Set path to graph.FindPath() and pass transform's positon and target position
        path = graph.FindPath(transform.position, target.position);
        // Set currentNode to zero
        currentNode = 0;
    }

    #region Seek
    // Special version of seek that takes into account the node radius and target radius
    Vector3 Seek(Vector3 target)
    {
        // Set Force to zero
        Vector3 force = Vector3.zero;

        // Set desiredForce to target - transform.position
        Vector3 desiredForce = target - transform.position;
        // Set desiredForce.y to zero
        desiredForce.y = 0;

        // Set distance to zero
        float distance = 0f;
        // If is at target is true (ternary operator)
        // Set distance to targetRadius, else set distance to nodeRadius
        distance = isAtTarget ? (targetRadius) : (nodeRadius);

        if (desiredForce.magnitude > distance)
        {
            desiredForce = desiredForce.normalized * weighting;
            force = desiredForce - owner.velocity;
        }

        return force;
    }
    #endregion
    #region GetForce
    // Calculates force for behaviour
    public override Vector3 GetForce()
    {
        // Set force to zero
        Vector3 force = Vector3.zero;

        // If  a path is not null AND path count is greater than zero
        if (path != null && path.Count > 0)
        {
            // Set currentPos to path[currentNode] position
            Vector3 currentPos = path[currentNode].position;
            // if distance between transfom.position and currentPos is less than or equal to nodeRadius
            if (Vector3.Distance(transform.position, currentPos) <= nodeRadius)
            {
                currentNode++;
                if( currentNode >= path.Count)
                {
                    currentNode = path.Count - 1;
                }
            }
            force = Seek(currentPos);
            #region Gizmos
            // Set previous position to path[0].position
           Vector3 previousPos =  path[0].position;
            // Foreach Node in path
            foreach (Node node in path)
            {
                // Call GizmosGl.AddSphere() and pass node.position, graph.nodeRadius, identity, any color
                GizmosGL.AddSphere(node.position, graph.nodeRadius, Quaternion.identity, Color.blue);
                // Call GizmosGl.AddLine() and pass previousPos, node.position, 0.1f,0.1f, any color, any color
                GizmosGL.AddLine(previousPos, node.position, .1f, .1f, Color.red, Color.green);
                //set previousPos to node.position
                previousPos = node.position;
            }
            #endregion
        }
        // RETURN force
        return force;
    }
    #endregion
    // Update is called once per frame
    void Update()
    {

    }
}
