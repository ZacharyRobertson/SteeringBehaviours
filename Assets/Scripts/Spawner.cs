using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject prefab;
    public float spawnRate = 1f;

    private float spawnTimer = 0f;
    public List<GameObject> objects = new List<GameObject>();


    void OnDrawGizmos()
    {
        // Draw a Cube to indicate where the box is that we're spawning objects
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        // Set spawnTimer to spawnTimer + deltaTime
        spawnTimer = spawnTimer + Time.deltaTime;
        // If spawnTimer > spawnRate 
        if (spawnTimer > spawnRate)
        {
            // Set randomPoint to GenerateRandomPoint
            Vector3 randomPoint = GenerateRandomPoint();
            // Call Spawn and pass randomPoint, Quaternion identity
            Spawn(randomPoint, Quaternion.identity);
            // Set spawnTimer to zero
            spawnTimer = 0;
        }
    }
    // Generates a random point within transform's local scale
    Vector3 GenerateRandomPoint()
    {
        // Set halfScale to half of the transform's scale
        Vector3 halfScale = transform.localScale * 0.5f;
        // Set Random point vector to zero
        Vector3 randomPoint = Vector3.zero;
        // Set random point x,y & z to RandomRange between -halfScale to halfScale(Can do individually)
        randomPoint.x = Random.Range(-halfScale.x, halfScale.x);
        randomPoint.y = Random.Range(-halfScale.y, halfScale.y);
        randomPoint.z = Random.Range(-halfScale.z, halfScale.z);
        // Return the randomPoint
        return randomPoint;
    }
    // Spawn the prefab at a given position and with rotation
    public void Spawn(Vector3 position, Quaternion rotation)
    {
        // Set clone to new instance of prefab
        GameObject clone = Instantiate(prefab);
        // add clone to objects list
        objects.Add(clone);
        // Set clone's position to spawner position + position
        clone.transform.position = transform.position + position;
        // Set clone's rotation to rotation
        clone.transform.rotation = rotation;
    }
}
