using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TerrainSquitch : MonoBehaviour
{
    [Header("Pip Settings")]
    
    //Pip Setting
    public int Pip_X;
    public int Pip_Z;
    public float Pip_Height;

    //Set Elevation Settings
    [Header("SetElevationSettings")]
    public float SetElevation_Elevation;

    //Extrude Box Settings
    [Header("Extrude Bos Settings")]
    public int Box_zMin = 30;
    public int
        Box_zMax = 100,
        Box_xMin = 30,
        Box_xMax = 100;
    public float Box_Height;


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
            for(mapPos.x = 0; mapPos.x < heightMapWidth; mapPos.x++)
            {
                 if(mapPos.z > Box_zMin && mapPos.z < Box_zMax &&
                    mapPos.x > Box_xMin && mapPos.x < Box_xMax)
                 {
                    heights[(int)mapPos.z, (int)mapPos.x] = Box_Height;
                 }
            }
           
        }
        thisTerrain.terrainData.SetHeights(0, 0, heights);
    }

    public void ThreeStairs()
    {
        //Setup Vars for first step
        Box_zMin = 0;
        Box_zMax = 10;
        Box_xMin = 0;
        Box_xMax = 10;
        Box_Height = .1f;
        ExtrudeBox();

        //Setup vars for second step
        Box_zMin = 0;
        Box_zMax = 10;
        Box_xMax = 10;
        Box_xMax = 20;
        Box_Height = .2f;
        ExtrudeBox();

        //Setup vars for thrid step
        Box_zMin = 0;
        Box_zMax = 10;
        Box_xMax = 20;
        Box_xMax = 30;
        Box_Height = .2f;
        ExtrudeBox();
    }
}
