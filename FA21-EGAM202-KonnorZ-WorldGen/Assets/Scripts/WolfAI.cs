using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfAI : MonoBehaviour
{

    public enum WolfStateT
    {
        RandomWalking,SeekingSheep,

    }

    public float Food = 25, FoodLostPerSecond = 1, EatingSpeed = 10;

    public WolfStateT currentState;

    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    public float maxDistance;

    public GameObject DestinationMarker;
    public float MaxStepSize;

    public GameObject wolfpre;

    public GameObject targo;


    // Start is called before the first frame update
    void Start()
    {
        Food = Random.Range(40f, 60f);

        navMeshAgent = GetComponent<NavMeshAgent>();

        currentState = WolfStateT.RandomWalking;

        targo = null;

    }

    // Update is called once per frame
    void Update()
    {
        Food -= FoodLostPerSecond * Time.deltaTime;

        switch (currentState)
        {
            case WolfStateT.RandomWalking:
                RandomWalking();
                break;

            case WolfStateT.SeekingSheep:
                SeekingSheep();
                break;

            

            default:
                throw new System.NotImplementedException("Ran off the end of the case statement");
        }
    }

    public void RandomWalking()
    {
        if (Food < 20)
        {
            currentState = WolfStateT.SeekingSheep;           
        }
        else
        {

            bool asCloseAsPossible = false;
            if (navMeshAgent.velocity.magnitude < .01f)
            {
                asCloseAsPossible = true;
            }


            if (asCloseAsPossible)
            {
                //Debug.Log("Finding new place to go");

                Vector3 randomStep = MaxStepSize * Random.onUnitSphere;
                DestinationMarker.transform.position = transform.position + randomStep;
                navMeshAgent.SetDestination(DestinationMarker.transform.position);

            }


        }

    }


    public void SeekingSheep()
    {
        
        if (targo == null)
        {
            GameObject[] preyObjects = GameObject.FindGameObjectsWithTag("Prey");
            targo = preyObjects[0];
        }
        else
        {
            navMeshAgent.SetDestination(targo.transform.position);
        }

   

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Prey"))
        {
            Destroy(other.gameObject);
            Food += 40;
            targo = null;
            currentState = WolfStateT.RandomWalking;
            Vector3 randomNearbyPosition;
            randomNearbyPosition = transform.position + maxDistance * Random.insideUnitSphere;
            Instantiate(wolfpre, randomNearbyPosition, Quaternion.identity, transform.parent);
        }
    }
}
