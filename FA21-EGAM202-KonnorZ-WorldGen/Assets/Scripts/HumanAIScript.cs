using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanAIScript : MonoBehaviour
{

    public enum HumanStateT
    {
        DecidingWhatToDoNext,
        SeekingFuel, MovingToFuel, CollectingTillFull, Reproducers,Die,
        Resting,

    }
    public float Fuel = 50, FuelLostPerSecond = 1, CollectingSpeed = 10;
    public float Food = 50, FoodLostPerSecond = 1, EatingSpeed = 10;

    public HumanStateT currentState;
    public float maxDistance;
    public float Dragon,CreateDragon;

    public GameObject DragonPrefab;

    NavMeshAgent navMeshAgent;

    public GameObject DestinationMarker;
    public float MaxStepSize;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Fuel = Random.Range(40f, 60f);
        Food = Random.Range(40f, 60f);

        navMeshAgent = GetComponent<NavMeshAgent>();

        currentState = HumanStateT.DecidingWhatToDoNext;

    }

    // Update is called once per frame
    void Update()
    {

        Fuel -= FuelLostPerSecond * Time.deltaTime;
        Food -= FoodLostPerSecond * Time.deltaTime;

        if (Food < 0 || Fuel < 0)
        {
            Destroy(this.gameObject);
        }
        switch (currentState)
        {
            case HumanStateT.DecidingWhatToDoNext:
                DecidingWhatToDoNext();
                break;

            case HumanStateT.SeekingFuel:
                SeekFuel();
                break;

            case HumanStateT.MovingToFuel:
                //Nothing needs doing
                break;

            case HumanStateT.CollectingTillFull:
                CollectFromFuel();
                break;
            case HumanStateT.Reproducers:
                Reproducers();
                break;
            case HumanStateT.Resting:
                Resting();
                break;
            case HumanStateT.Die:
                Die();
                break;
            

            default:
                throw new System.NotImplementedException("Ran off the end of the case statement");
        }
    }

    public void DecidingWhatToDoNext()
    {
        if (Fuel < 50)
        {
            currentState = HumanStateT.SeekingFuel;
            return;
        }
    }

    public void SeekFuel()
    {
        GameObject[] fuelObjects = GameObject.FindGameObjectsWithTag("Fuel");
        GameObject targetfuelObjects = fuelObjects[0];
        Debug.Log("Human is going to" + targetfuelObjects.name);

        navMeshAgent.SetDestination(targetfuelObjects.transform.position);
        currentState = HumanStateT.MovingToFuel;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (currentState == HumanStateT.MovingToFuel && other.tag == "Fuel")
        {
            Debug.Log("Collide");
            currentState = HumanStateT.CollectingTillFull;
        }
    }

    public void CollectFromFuel()
    {
        Fuel += CollectingSpeed * Time.deltaTime;
        if(Fuel > 100)
        {
            currentState = HumanStateT.Reproducers;
        }
    }
    public void Reproducers()
    {
        
        Fuel += CollectingSpeed * Time.deltaTime;
        animator.SetTrigger("Reproduce");
        if (Fuel > 101)
        {
            
            Vector3 randomNearbyPosition;
            randomNearbyPosition = transform.position + maxDistance * Random.insideUnitSphere;
            Instantiate(DragonPrefab, randomNearbyPosition, Quaternion.identity, transform.parent);
            Fuel -= CollectingSpeed * Time.deltaTime;
            currentState = HumanStateT.Resting;

        }
        
    }

    public void Resting()
    {
        animator.SetTrigger("Return");
        if (Fuel < 95)
        {
            bool asCloseAsPossible = false;
            if (navMeshAgent.velocity.magnitude < .01f)
                asCloseAsPossible = true;

            if (asCloseAsPossible)
                Debug.Log("Finding new place to go");
            Vector3 randomStep = MaxStepSize * Random.onUnitSphere;
            DestinationMarker.transform.position = transform.position + randomStep;
            navMeshAgent.SetDestination(DestinationMarker.transform.position);

            currentState = HumanStateT.Die;
        }
        
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        if (Fuel < 90)
        {
            
            Destroy(this.gameObject);
        }
        
        
    }
}
