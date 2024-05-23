using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LoadHeightMap : MonoBehaviour
{

    private Terrain terrain;

    private TerrainData terrainData;

    [SerializeField]

    private Texture2D heightMapImage;

    [SerializeField]

    private Vector3 heightMapScale = new Vector3(1, 1, 1);

    [Header("Play Mode")]
    [SerializeField]

    private bool loadHeightMap = true;

    [SerializeField]

    private bool flattenTerrainOnExit = true;

    [Header("EditorMode")]
    [SerializeField]

    private bool loadHeightMapinEditMode = false;

    [SerializeField]

    private bool flattenTerrainInEditMode = false;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.IsPlaying(gameObject) && loadHeightMap)
        {
            LoadHeightMapImage();
        }
    }

    void LoadHeightMapImage()
    {

        if (terrain == null)
        {
            terrain = this.GetComponent<Terrain>();
        }

        if (terrainData == null)
        {
            terrainData = Terrain.activeTerrain.terrainData;
        }

        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int width = 0; width < terrainData.heightmapResolution; width++)
        {
            for (int height = 0; height < terrainData.heightmapResolution; height++)
            {
                heightMap[width, height] = heightMapImage.GetPixel((int)(width * heightMapScale.x), (int)(height * heightMapScale.z)).grayscale * heightMapScale.y;
            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    void FlattenTerrain()
    {
        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int width = 0; width < terrainData.heightmapResolution; width++)
        {
            for (int height = 0; height < terrainData.heightmapResolution; height++)
            {
                heightMap[width, height] = 0;
            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    private void OnValidate()
    {
        //Debug.Log("onValidate called!");
        if (flattenTerrainInEditMode)
        {
            FlattenTerrain();
        }
        else if (loadHeightMapinEditMode)
        {
            LoadHeightMapImage();
        }
    }

    private void OnDestroy()
    {
        //Debug.Log("onDestroy called!");
        if (flattenTerrainOnExit)
        {
            FlattenTerrain();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
