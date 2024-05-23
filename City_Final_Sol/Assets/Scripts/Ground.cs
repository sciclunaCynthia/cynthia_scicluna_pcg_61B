using UnityEngine;

public class Ground : MonoBehaviour
{
    public Terrain terrain; // Reference to the terrain
    public Texture2D sandTexture; // Reference to the sand texture
    public Texture2D normalMap; // Reference to the normal map (optional)
    public float tileSize = 10f; // Size of the texture tiles

    void Start()
    {
        ApplyTextureToTerrain();
    }

    void ApplyTextureToTerrain()
    {
        TerrainData terrainData = terrain.terrainData;

        // Create a new TerrainLayer
        TerrainLayer terrainLayer = new TerrainLayer();
        terrainLayer.diffuseTexture = sandTexture;
        terrainLayer.normalMapTexture = normalMap;
        terrainLayer.tileSize = new Vector2(tileSize, tileSize);

        // Apply the new TerrainLayer to the terrain
        terrainData.terrainLayers = new TerrainLayer[] { terrainLayer };

        // Refresh the terrain to apply the changes
        terrain.Flush();
    }
}

