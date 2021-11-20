using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{

    public enum CrystalfieldStateT { Seed, Seedling,Grow,Branch, Adult}
    public CrystalfieldStateT currentState;

    
    public float Crystal, CreateCrystal, CrystalGainPerSecond, MaxNum;
    public float CrowdingDistance, MaxDispersalDistance;
    public float BirthTime, Age;

    public GameObject CrystalfieldPrefab;
    public GameObject CrystalfieldPrefab1;

    public GameObject CrystalfieldPrefab2;
    private Collider[] crystalfieldHits;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        float terrainHeight = GameObject.Find("Terrain").GetComponent<Terrain>().SampleHeight(transform.position);
        transform.position = new Vector3(transform.position.x, terrainHeight, transform.position.z);
        animator = GetComponent<Animator>();

        crystalfieldHits = Physics.OverlapSphere(transform.position,CrowdingDistance,LayerMask.GetMask("Crystal"));
        if(crystalfieldHits.Length > 1)
        {
            gameObject.SetActive(false);
            Object.Destroy(this.gameObject);
            return;
        }

        BirthTime = Time.time;
        Age = 0;
        currentState = CrystalfieldStateT.Seed;
        transform.localScale = new Vector3(2, 2, 2);

        //transform.position = new Vector3(transform.position.x, terrainHeight, transform.position.z);
        Crystal = CreateCrystal;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case CrystalfieldStateT.Seed:
                SeedUpdate();
                break;
            case CrystalfieldStateT.Seedling:
                SeedlingUpdate();
                break;
            case CrystalfieldStateT.Grow:
                GrowUpdate();
                break;
            case CrystalfieldStateT.Branch:
                BranchUpdate();
                break;
            case CrystalfieldStateT.Adult:
                AdultUpdate();
                break;
        }
    }

    public void SeedUpdate()
    {
        Crystal += CrystalGainPerSecond * Time.deltaTime;
        Age = Time.time - BirthTime;
        
        if(Age > 5)
        {
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetTrigger("Seedling");
            currentState = CrystalfieldStateT.Seedling;
            
        }

    }

    public void SeedlingUpdate()
    {
        Crystal += CrystalGainPerSecond * Time.deltaTime;
        Age = Time.time - BirthTime;
        
        if(Age > 10)
        {
            //transform.localScale = new Vector3(4, 4, 4);
            animator.SetTrigger("Grow");
            //transform.GetChild(0).GetComponent<MeshRender>().material = SeedlingMaterial;
            currentState = CrystalfieldStateT.Grow;
        }
    }
    public void GrowUpdate()
    {
        Crystal += CrystalGainPerSecond * Time.deltaTime;
        Age = Time.time - BirthTime;

        if (Age > 15)
        {
            //transform.localScale = new Vector3(5, 5, 5);
            animator.SetTrigger("Branch");
            //transform.GetChild(0).GetComponent<MeshRender>().material = SeedlingMaterial;
            currentState = CrystalfieldStateT.Branch;

            Vector3 randomNearbyPosition;
            randomNearbyPosition = transform.position + MaxDispersalDistance * Random.insideUnitSphere;
            Instantiate(CrystalfieldPrefab1, randomNearbyPosition, Quaternion.identity, transform.parent);
            Crystal -= 2f * CreateCrystal;

            Crystal -= 2f * CreateCrystal;
        }

    }

    public void BranchUpdate()
    {
        Crystal += CrystalGainPerSecond * Time.deltaTime;
        Age = Time.time - BirthTime;

        if (Age > 20)
        {
            //transform.localScale = new Vector3(6, 6, 6);
            animator.SetTrigger("Adult");
            //transform.GetChild(0).GetComponent<MeshRender>().material = SeedlingMaterial;
            currentState = CrystalfieldStateT.Adult;

            Vector3 randomNearbyPosition;
            randomNearbyPosition = transform.position + MaxDispersalDistance * Random.insideUnitSphere;
            Instantiate(CrystalfieldPrefab2, randomNearbyPosition, Quaternion.identity, transform.parent);
            Crystal -= 2f * CreateCrystal;
        }

    }

    public void AdultUpdate()
    {
        Crystal += CrystalGainPerSecond * Time.deltaTime;
        Age = Time.time - BirthTime;

        if(Crystal > MaxNum)
        {
            Vector3 randomNearbyPosition;
            randomNearbyPosition = transform.position + MaxDispersalDistance * Random.insideUnitSphere;
            Instantiate(CrystalfieldPrefab, randomNearbyPosition, Quaternion.identity, transform.parent);

            Crystal -= 2f * CreateCrystal;
        }
    }
}
