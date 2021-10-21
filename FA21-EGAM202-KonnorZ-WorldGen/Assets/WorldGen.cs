using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    [Header("Pip Settings")]
    [Header("SetElevationSettings")]
    [Header("Extrude Bos Settings")]
    //Pip Setting
    public int Pip_X;
    public int Pip_Z;
    public float Pip_Height;

    //Set Elevation Settings
    public float SetElevation_Elevation;

    //Extrude Box Settings
    public int Box_zMin = 30;
    public int
        Box_zMax = 100,
        Box_xMin = 30,
        Box_xMax = 100;
    public float Box_Height;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pip()
    {
        Terrain thisTerrain = GetComponent<Terrain>();
        if(thisTerrain == null)
        {
            throw new System.Exception("TerrainSquitch requires a Terrain. Please add a Terrain to " + "and length = " + gameObject.name);
        }

        int heightMapWidth, heightMapLength;
        heightMapWidth = thisTerrain.terrainData.heightmapResolution;
        heightMapLength = thisTerrain.terrainData.heightmapResolution;
        Debug.Log("This Terrain has a heightMap with width = " + heightMapWidth + "and length = " + heightMapLength);

        float[,] heights;
        heights = thisTerrain.terrainData.GetHeights(0, 0, heightMapWidth, heightMapLength);

        heights[Pip_Z, Pip_X] = Pip_Height;
        thisTerrain.terrainData.SetHeights(0, 0, heights);
        thisTerrain.terrainData.SetHeights(0, 0, heights);
    }

    public void SetElevation()
    {
        Terrain thisTerrain = GetComponent<Terrain>();
        if (thisTerrain == null)
        {
            throw new System.Exception("TerrainSquitch requires a Terrain. Please add a Terrain to " + "and length = " + gameObject.name);
        }

        int heightMapWidth, heightMapLength;
        heightMapWidth = thisTerrain.terrainData.heightmapResolution;
        heightMapLength = thisTerrain.terrainData.heightmapResolution;
        Debug.Log("This Terrain has a heightMap with width = " + heightMapWidth + "and length = " + heightMapLength);

        float[,] heights;
        heights = thisTerrain.terrainData.GetHeights(0, 0, heightMapWidth, heightMapLength);

        Vector3 mapPos;
        mapPos.z = 0;
        for(mapPos.z = 0;mapPos.z < heightMapLength; mapPos.z++)
        {
            for (mapPos.x = 0; mapPos.x < heightMapWidth; mapPos.x++)
            {
                heights[(int)mapPos.z, (int)mapPos.x] = SetElevation_Elevation;
            }
        }
        thisTerrain.terrainData.SetHeights(0, 0, heights);
    }

    public void ExtrudeBox()
    {
        Terrain thisTerrain = GetComponent<Terrain>();
        if (thisTerrain == null)
        {
            throw new System.Exception("TerrainSquitch requires a Terrain. Please add a Terrain to " + "and length = " + gameObject.name);
        }

        int heightMapWidth, heightMapLength;
        heightMapWidth = thisTerrain.terrainData.heightmapResolution;
        heightMapLength = thisTerrain.terrainData.heightmapResolution;
        Debug.Log("This Terrain has a heightMap with width = " + heightMapWidth + "and length = " + heightMapLength);

        float[,] heights;
        heights = thisTerrain.terrainData.GetHeights(0, 0, heightMapWidth, heightMapLength);

        Vector3 mapPos;
        for(mapPos.z = 0;mapPos.z < heightMapLength; mapPos.z++)
        {
            if(mapPos.z > Box_zMin && mapPos.z < Box_zMax &&
               mapPos.x > Box_xMin && mapPos.x < Box_xMax)
            {
                heights[(int)mapPos.z, (int)mapPos.x] = Box_Height;
            }
        }
        thisTerrain.terrainData.SetHeights(0, 0, heights);
    }
}
