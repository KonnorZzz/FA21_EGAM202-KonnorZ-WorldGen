using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TerrainSquitch : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CityofKonnor();
            GetComponent<UnityEngine.AI.NavMeshSurface>().BuildNavMesh();
        }
    }


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
    public float SingleStep_MaxStepSize;

    [Header("Many Step Up Settings")]
    public float ManySteps_MaxStepHeight;
    public float ManySteps_NSteps;

    [Header("Smooth Function")]
    public int smoothAmount = 2;
    public TerrainData terrainData;

    [Header("install Water Settings")]
    public int InstallWater_WaterLevel;
    public GameObject WaterPrefab;
    public float WaterPrefabSize;
    public Transform InstallWater_Parent;

    [Header("Fill Niche Settings")]
    public Niche FillNiche_Niche;
    public Transform FillNiche_ParentTransform;

    [Header("Hill of landscape")]

    public float scale;


    [Header("Triangular column")]
    public float MinHeight = 10.0f;
    public float MaxHeight = 50.0f;
    public bool resetTerrain = true;

    [Header("Mountain")]
    public float MoutainMinHeight = 0.1f;
    public float MountainMaxHeight = 0.5f;
    public bool resetMountain = true;

    [Header("Fill Sheep")]
    public Niche FillNiche_Sheep;
    public Transform FillSheep_ParentTransform;

    [Header("Fill Tree")]
    public Niche FillNiche_Tree;
    public Transform FillTree_ParentTransform;



    public void GenerateHills()
    {
        Terrain thisTerrain = GetComponent<Terrain>();
        if (thisTerrain == null)
            throw new System.Exception("TerrainSquitch need a terrain, please add a terrain");

        int heightMapWidth, heightMapLength;
        heightMapWidth = thisTerrain.terrainData.heightmapResolution;
        heightMapLength = thisTerrain.terrainData.heightmapResolution;
        Debug.Log("This terrain has a heightMap with width = " + heightMapWidth + " and length = " + heightMapLength);

        float[,] heights;
        heights = thisTerrain.terrainData.GetHeights(0, 0, heightMapWidth, heightMapLength);

        Vector3 mapPos = Vector3.zero;



        for (mapPos.z = 0; mapPos.z < heightMapLength; mapPos.z++)
        {
            for (mapPos.x = 0; mapPos.x < heightMapWidth; mapPos.x++)
            {

                heights[(int)mapPos.z, (int)mapPos.x] = CalculateHeight(mapPos.z, mapPos.x);

            }
        }
        thisTerrain.terrainData.SetHeights(0, 0, heights);


        float CalculateHeight(float x, float y)
        {
            float xCoord = (float)x / heightMapWidth * scale;
            float yCoord = (float)y / heightMapLength * scale;
            return Mathf.PerlinNoise(xCoord, yCoord);
        }

    }


    public void Pip()
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
        for (mapPos.z = 0; mapPos.z < heightMapLength; mapPos.z++)
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
        for (mapPos.z = 0; mapPos.z < heightMapLength; mapPos.z++)
        {
            for (mapPos.x = 0; mapPos.x < heightMapWidth; mapPos.x++)
            {
                if (mapPos.z > Box_zMin && mapPos.z < Box_zMax &&
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
                if ((mapPos.z - Cyl_z) * (mapPos.z - Cyl_z) + (mapPos.x - Cyl_x) * (mapPos.x - Cyl_x) <= r * r)
                {
                    heights[(int)mapPos.z, (int)mapPos.x] = Cyl_Height;
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
            for (mapPos.x = 0; mapPos.x < heightMapWidth; mapPos.x++)
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

        Vector3 mapPos;
        mapPos = Vector3.zero;

        Plane dividingPlane;
        Vector3 planePoint, planeNormal;
        planePoint = new Vector3(Random.Range(0, heightMapWidth), 0, Random.Range(0, heightMapLength));
        planeNormal = Random.onUnitSphere;

        dividingPlane = new Plane(planeNormal, planePoint);
        float stepSize = Random.Range(-SingleStep_MaxStepSize, SingleStep_MaxStepSize);
        //stepSize = Random.Range(-SingleStep_MaxStepHeight, SingleStep_MaxStepHeight);

        for (mapPos.x = 0; mapPos.x < heightMapLength; mapPos.x++)
        {
            for (mapPos.z = 0; mapPos.z < heightMapWidth; mapPos.z++)
            {
                if (dividingPlane.GetSide(mapPos))
                {
                    heights[(int)mapPos.z, (int)mapPos.x] += stepSize;
                }
            }
        }

        
        thisTerrain.terrainData.SetHeights(0, 0, heights);

    }

    public void ManySteps()
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


        for (int stepCount = 0; stepCount < ManySteps_NSteps; stepCount++)
        {
            planePoint = new Vector3(Random.Range(0, heightMapWidth), Random.Range(0, 100), Random.Range(0, heightMapLength));
            planeNormal = Random.onUnitSphere;
            dividingPlane = new Plane(planeNormal, planePoint);


            stepSize = Random.Range(-ManySteps_MaxStepHeight, ManySteps_MaxStepHeight);

            for (mapPos.z = 0; mapPos.z < heightMapLength; mapPos.z++)
            {
                for (mapPos.x = 0; mapPos.x < heightMapWidth; mapPos.x++)
                {
                    if (dividingPlane.GetSide(mapPos))
                    {
                        heights[(int)mapPos.z, (int)mapPos.x] += stepSize;
                    }
                }
            }
        }
        thisTerrain.terrainData.SetHeights(0, 0, heights);

    }

    List<Vector2> GenerateNeighbours(Vector2 pos, int width, int height)
    {
        List<Vector2> neighbours = new List<Vector2>();
        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                if (!(x == 0 && y == 0))
                {
                    Vector2 nPos = new Vector2(Mathf.Clamp(pos.x + x, 0, width - 1),
                                                Mathf.Clamp(pos.y + y, 0, height - 1));
                    if (!neighbours.Contains(nPos))
                        neighbours.Add(nPos);
                }
            }
        }
        return neighbours;
    }


    public void SmoothFunction()
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

        terrainData = Terrain.activeTerrain.terrainData;
        float[,] heights = thisTerrain.terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);

        heights = thisTerrain.terrainData.GetHeights(0, 0, heightMapWidth, heightMapLength);

        for (int s = 0; s < smoothAmount; s++)
        {
            for (int y = 0; y < heightMapWidth; y++)
            {
                for (int x = 0; x < heightMapLength; x++)
                {
                    float avgHeight = heights[x, y];
                    List<Vector2> neighbours = GenerateNeighbours(new Vector2(x, y), heightMapWidth, heightMapLength);

                    foreach (Vector2 n in neighbours)
                    {
                        avgHeight += heights[(int)n.x, (int)n.y];
                    }
                    heights[x, y] = avgHeight / ((float)neighbours.Count + 1);
                }
            }
        }
        thisTerrain.terrainData.SetHeights(0, 0, heights);
    }

    public void InstallWater()
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

        float heightMapWidthWorld, heightMapLengthWorld;
        heightMapWidthWorld = heightMapWidth * thisTerrain.terrainData.heightmapScale.x;
        heightMapLengthWorld = heightMapLength * thisTerrain.terrainData.heightmapScale.z;

        Vector3 worldPos;
        worldPos = new Vector3(0, InstallWater_WaterLevel, 0);
        for (worldPos.z = 0; worldPos.z < heightMapLengthWorld; worldPos.z += WaterPrefabSize)
        {
            for (worldPos.x = 0; worldPos.x < heightMapWidthWorld; worldPos.x += WaterPrefabSize)
            {
                worldPos.y = thisTerrain.SampleHeight(worldPos);
                if (worldPos.y < InstallWater_WaterLevel)
                {
                    Instantiate(WaterPrefab, new Vector3(worldPos.x, InstallWater_WaterLevel, worldPos.z), Quaternion.identity, InstallWater_Parent);
                }
            }
        }
    }

    public void FillNiche()
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

        float heightMapWidthWorld, heightMapLengthWorld;
        heightMapWidthWorld = heightMapWidth * thisTerrain.terrainData.heightmapScale.x;
        heightMapLengthWorld = heightMapLength * thisTerrain.terrainData.heightmapScale.z;

        Vector3 worldPos;
        worldPos = new Vector3(0, InstallWater_WaterLevel, 0);
        for (worldPos.z = 0; worldPos.z < heightMapLengthWorld; worldPos.z += WaterPrefabSize)
        {
            for (worldPos.x = 0; worldPos.x < heightMapWidthWorld; worldPos.x += WaterPrefabSize)
            {
                worldPos.y = thisTerrain.SampleHeight(worldPos);
                if (worldPos.x > FillNiche_Niche.MinX && worldPos.x < FillNiche_Niche.MaxX &&
                    worldPos.z > FillNiche_Niche.MinZ && worldPos.z < FillNiche_Niche.MaxZ &&
                    worldPos.y > FillNiche_Niche.MinElev && worldPos.y < FillNiche_Niche.MaxElev)
                {
                    if (Random.value < FillNiche_Niche.ProbabilityPerMeter)
                    {
                        Instantiate(FillNiche_Niche.NicheOccupant, worldPos, Quaternion.identity, FillNiche_ParentTransform);
                    }
                }
            }
        }
    }

    public void FillSheep()
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

        float heightMapWidthWorld, heightMapLengthWorld;
        heightMapWidthWorld = heightMapWidth * thisTerrain.terrainData.heightmapScale.x;
        heightMapLengthWorld = heightMapLength * thisTerrain.terrainData.heightmapScale.z;

        Vector3 worldPos;
        worldPos = new Vector3(0, InstallWater_WaterLevel, 0);
        for (worldPos.z = 0; worldPos.z < heightMapLengthWorld; worldPos.z += WaterPrefabSize)
        {
            for (worldPos.x = 0; worldPos.x < heightMapWidthWorld; worldPos.x += WaterPrefabSize)
            {
                worldPos.y = thisTerrain.SampleHeight(worldPos);
                if (worldPos.x > FillNiche_Sheep.MinX && worldPos.x < FillNiche_Sheep.MaxX &&
                    worldPos.z > FillNiche_Sheep.MinZ && worldPos.z < FillNiche_Sheep.MaxZ &&
                    worldPos.y > FillNiche_Sheep.MinElev && worldPos.y < FillNiche_Sheep.MaxElev)
                {
                    if (Random.value < FillNiche_Sheep.ProbabilityPerMeter)
                    {
                        Instantiate(FillNiche_Sheep.NicheOccupant, worldPos, Quaternion.identity, FillSheep_ParentTransform);
                    }
                }
            }
        }
    }

    public void FillTree()
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

        float heightMapWidthWorld, heightMapLengthWorld;
        heightMapWidthWorld = heightMapWidth * thisTerrain.terrainData.heightmapScale.x;
        heightMapLengthWorld = heightMapLength * thisTerrain.terrainData.heightmapScale.z;

        Vector3 worldPos;
        worldPos = new Vector3(0, InstallWater_WaterLevel, 0);
        for (worldPos.z = 0; worldPos.z < heightMapLengthWorld; worldPos.z += WaterPrefabSize)
        {
            for (worldPos.x = 0; worldPos.x < heightMapWidthWorld; worldPos.x += WaterPrefabSize)
            {
                worldPos.y = thisTerrain.SampleHeight(worldPos);
                if (worldPos.x > FillNiche_Tree.MinX && worldPos.x < FillNiche_Tree.MaxX &&
                    worldPos.z > FillNiche_Tree.MinZ && worldPos.z < FillNiche_Tree.MaxZ &&
                    worldPos.y > FillNiche_Tree.MinElev && worldPos.y < FillNiche_Tree.MaxElev)
                {
                    if (Random.value < FillNiche_Tree.ProbabilityPerMeter)
                    {
                        Instantiate(FillNiche_Tree.NicheOccupant, worldPos, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), FillTree_ParentTransform);
                    }
                }
            }
        }
    }

    public void MakeItFancy()
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

        int alphaMapSize;
        alphaMapSize = thisTerrain.terrainData.alphamapResolution;
        Debug.Log("This Terrain has a alphaMap with width = " + alphaMapSize + "and length = " + alphaMapSize);

        terrainData = Terrain.activeTerrain.terrainData;
        float[,] heights = thisTerrain.terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);

        heights = thisTerrain.terrainData.GetHeights(0, 0, heightMapWidth, heightMapLength);

        const int SAND = 0;
        const int ROCK = 1;
        const int FOREST = 2;

        const int NTERRAINLAYERS = 3;

        float[,,] alphaAtMapPos = new float[alphaMapSize, alphaMapSize, NTERRAINLAYERS];
        Vector3 mapPos;
        mapPos.z = 0;
        for (mapPos.z = 0; mapPos.z < alphaMapSize; mapPos.z++)
        {
            for (mapPos.x = 0; mapPos.x < alphaMapSize; mapPos.x++)
            {
                if (heights[(int)mapPos.z, (int)mapPos.x] < .3f)
                {
                    alphaAtMapPos[(int)mapPos.z, (int)mapPos.x, SAND] = 1.0f;
                    alphaAtMapPos[(int)mapPos.z, (int)mapPos.x, ROCK] = 0;
                    alphaAtMapPos[(int)mapPos.z, (int)mapPos.x, FOREST] = 0;
                }
                if (heights[(int)mapPos.z, (int)mapPos.x] >= 0.3f && heights[(int)mapPos.z, (int)mapPos.x] <= 0.5f)
                {
                    alphaAtMapPos[(int)mapPos.z, (int)mapPos.x, SAND] = 0;
                    alphaAtMapPos[(int)mapPos.z, (int)mapPos.x, ROCK] = 1.0f;
                    alphaAtMapPos[(int)mapPos.z, (int)mapPos.x, FOREST] = 0;
                }
                if (heights[(int)mapPos.z, (int)mapPos.x] > .5f)
                {
                    alphaAtMapPos[(int)mapPos.z, (int)mapPos.x, SAND] = 0;
                    alphaAtMapPos[(int)mapPos.z, (int)mapPos.x, ROCK] = 0;
                    alphaAtMapPos[(int)mapPos.z, (int)mapPos.x, FOREST] = 1.0f;
                }

            }
        }
        thisTerrain.terrainData.SetAlphamaps(0, 0, alphaAtMapPos);

    }

    public void CityofKonnor()
    {
        SetElevation_Elevation = 0;
        SetElevation();

        ManySteps_MaxStepHeight = 0.08f;
        ManySteps_NSteps = 300;
        ManySteps();

        smoothAmount = 5;
        SmoothFunction();

        InstallWater();
        FillNiche();
        FillSheep();
        FillTree();
        MakeItFancy();
    }

    public void TriangularColumn()
    {

        float[,] GetHeightMap()
        {
            if (!resetTerrain)
            {
                return terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
            }
            else
            {
                return new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
            }
        }
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

        float[,] heightMap = GetHeightMap();
        Vector3 peak = new Vector3(Random.Range(0, terrainData.heightmapResolution), Random.Range(MinHeight, MaxHeight), Random.Range(0, terrainData.heightmapResolution));
        if (heightMap[(int)peak.x, (int)peak.z] < peak.y)
        {
            heightMap[(int)peak.x, (int)peak.z] = peak.y;
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    public void Mountain()
    {
        float[,] GetHeightMap()
        {
            if (!resetMountain)
            {
                return terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
            }
            else
            {
                return new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
            }
        }
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

        float[,] heightMap = GetHeightMap();
        Vector3 peak = new Vector3(Random.Range(0, heightMapWidth), Random.Range(MinHeight, MaxHeight), Random.Range(0, heightMapLength));
        if (heightMap[(int)peak.x, (int)peak.z] < peak.y)
        {
            heightMap[(int)peak.x, (int)peak.z] = peak.y;
        }
        //peak location on the height map
        Vector2 peakLocation = new Vector2(peak.x, peak.z);
        float maxDistance = Vector2.Distance(new Vector2(0, 0), new Vector2(heightMapWidth, heightMapLength));
        for (int y = 0; y < heightMapLength; y++)
        {
            for (int x = 0; x < heightMapWidth; x++)
            {
                if (!(x == peak.x && y == peak.z))
                {
                    float distanceToPeak = Vector2.Distance(peakLocation, new Vector2(x, y));
                    heightMap[x, y] = peak.y - (distanceToPeak / maxDistance);
                }
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    public void DeleteWater()
    {
        for (int w = 0; w <100;w++)
        {
            DestroyImmediate(InstallWater_Parent.GetChild(0).gameObject);

        }

    }
    public void DeleteNiche()
    {
        for (int w = 0; w < 100; w++)
        {
            DestroyImmediate(FillNiche_ParentTransform.GetChild(0).gameObject);

        }

    }

    public void DeleteSheep()
    {
        for (int w = 0; w < 100; w++)
        {
            DestroyImmediate(FillSheep_ParentTransform.GetChild(0).gameObject);

        }

    }

    public void DeleteTree()
    {
        for (int w = 0; w < 100; w++)
        {
            DestroyImmediate(FillTree_ParentTransform.GetChild(0).gameObject);

        }

    }

    

}
