using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPathGenerator : MonoBehaviour
{
    public int numSegments = 10;
    public GameObject roadPrefab;
    public Terrain terrain; // Reference to your terrain

    void Start()
    {
        GenerateRoad();
    }

    void GenerateRoad()
    {
        Vector3 terrainSize = terrain.terrainData.size;

        for (int i = 0; i < numSegments; i++)
        {
            float xPos = i * 10f; // Adjust as needed for segment spacing
            float yPos = 700; //terrain.SampleHeight(new Vector3(xPos, 0, 0)) + terrain.transform.position.y;
            Vector3 position = new Vector3(xPos, yPos, 0);
            Quaternion rotation = Quaternion.identity;
            GameObject roadSegment = Instantiate(roadPrefab, position, rotation);
            roadSegment.transform.SetParent(transform); // Optional: Set the road sentgme as a child of this object
        }
    }
}
