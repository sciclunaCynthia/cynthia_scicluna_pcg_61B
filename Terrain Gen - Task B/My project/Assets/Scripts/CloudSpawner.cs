using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab; 
    public float maxHeightOffset = 1.0f;
    public float spacing = 10.0f; 
    public int cloudAmount = 10; 
    public float minHeight = 10.0f; 
    public float maxHeight = 100.0f;

    private Terrain terrain;

    void Start()
    {
        terrain = GetComponent<Terrain>();
        SpawnCloudsOnTerrain();
    }

    void SpawnCloudsOnTerrain()
    {
        float[,] heights = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);

        for (int i = 0; i < cloudAmount; i++)
        {
            int x = Random.Range(0, terrain.terrainData.heightmapResolution);
            int y = Random.Range(0, terrain.terrainData.heightmapResolution);

            float worldX = x * terrain.terrainData.size.x / terrain.terrainData.heightmapResolution + terrain.transform.position.x;
            float worldZ = y * terrain.terrainData.size.z / terrain.terrainData.heightmapResolution + terrain.transform.position.z;
            float worldY = terrain.transform.position.y + terrain.terrainData.GetHeight(x, y) * terrain.terrainData.size.y;

            // Clamp the cloud spawn height within the specified range
            worldY = Mathf.Clamp(worldY, minHeight, maxHeight);

            Vector3 spawnPosition = new Vector3(worldX, worldY + maxHeightOffset, worldZ);

            GameObject cloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
            cloud.transform.parent = transform;
        }
    }
}
