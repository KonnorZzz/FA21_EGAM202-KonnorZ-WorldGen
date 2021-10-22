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

    [Header("ExtrudeCylinder")]
    public int Cyl_x;
    public int Cyl_y;
    public int Cyl_z;
    public int r;
    public float Cyl_Height;

    //[Header("ExtrudeTraingle")]

    [Header("Random Walk Profile Settings")]
    public float RandomWalk_StartingElevation;
    public float RandomWalk_MaxStepSize;

    [Header("Single Step Settings")]
    public float SingleStep_MaxStepHeight;
    

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
        Box_xMin = 10;
        Box_xMax = 20;
        Box_Height = .2f;
        ExtrudeBox();

        //Setup vars for thrid step
        Box_zMin = 0;
        Box_zMax = 10;
        Box_xMin = 20;
        Box_xMax = 30;
        Box_Height = .3f;
        ExtrudeBox();
    }

    public void ExtrudeCylinder()
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
        for (mapPos.z = 0; mapPos.z < heightMapLength; mapPos.z++)
        {
            for (mapPos.x = 0; mapPos.x < heightMapWidth; mapPos.x++)
            {
                if ((mapPos.z - Cyl_z) * (mapPos.z - Cyl_z)+(mapPos.x - Cyl_x)*(mapPos.x - Cyl_x) <= r * r)
                {
                    heights[(int)mapPos.z,(int)mapPos.x] = Cyl_Height;
                }
            }

        }
        thisTerrain.terrainData.SetHeights(0, 0, heights);
    }

    public void RandomIndependentProfile()
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
        float heightAtThisZ;
        for (mapPos.z = 0; mapPos.z < heightMapLength; mapPos.z++)
        {
            heightAtThisZ = Random.value;
            for(mapPos.x = 0; mapPos.x < heightMapWidth; mapPos.x++)
            {
                heights[(int)mapPos.z, (int)mapPos.x] = heightAtThisZ;
            }

        }
        thisTerrain.terrainData.SetHeights(0, 0, heights);
    }

    public void RandomWalkProfile()
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
        float heightAtThisZ = RandomWalk_StartingElevation;
        for (mapPos.z = 0; mapPos.z < heightMapLength; mapPos.z++)
        {
            heightAtThisZ += Random.Range(-RandomWalk_MaxStepSize, RandomWalk_MaxStepSize);
            for (mapPos.x = 0; mapPos.x < heightMapLength; mapPos.x++)
            {
                heights[(int)mapPos.z, (int)mapPos.x] = heightAtThisZ;
            }
            

        }
        thisTerrain.terrainData.SetHeights(0, 0, heights);
    }

    public void SingleStep()
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

        Vector3 mapPos = Vector3.zero;

        Plane dividingPlane;
        Vector3 planePoint, planeNormal;
        float stepSize;

        planePoint = new Vector3(Random.Range(0, heightMapWidth), 0, Random.Range(0, heightMapLength));
        planeNormal = Random.onUnitSphere;
        dividingPlane = new Plane(planeNormal, planePoint);

        stepSize = Random.Range(-SingleStep_MaxStepHeight, SingleStep_MaxStepHeight);
        for(mapPos.x = 0;mapPos.x < heightMapLength; mapPos.x++)
        {
            for (mapPos.z = 0;mapPos.z < heightMapWidth; mapPos.z++)
            {
                if (dividingPlane.GetSide(mapPos))
                {
                    heights[(int)mapPos.z,(int)mapPos.x] += stepSize;
                }
            }
        }

    }


}
