using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 position;

    [Header("Numbers")]
    public int gridX, gridZ;
    public int gCost, hCost; // AKA Heuristic
    public int fCost
    {
      get
        {
            return gCost + hCost;
        }
    }
    public Node parent;
    //Constructor
    /// <summary>
    /// Constructor for Node
    /// </summary>
    /// <param name="walkable">the bool that detects whether node is walkable</param>
    /// <param name="position">the point where node is located</param>
    /// <param name="gridX">x coordinate in 2D array</param>
    /// <param name="gridZ">z coordinate in 2D array</param>
    public Node(bool walkable, Vector3 position, int gridX, int gridZ)
    {
        this.walkable = walkable;
        this.position = position;
        this.gridX = gridX;
        this.gridZ = gridZ;
    }
}
