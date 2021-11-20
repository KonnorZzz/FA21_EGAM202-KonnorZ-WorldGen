using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiScript : MonoBehaviour
{

    public enum MotionTypeT { NA, WaypointPatrol, RandomWalk }

    public MotionTypeT MotionType;

    public Transform[] Waypoints;
    public int CurrentDestinationIndex;

    public GameObject DestinationMarker;
    public float MaxStepSize;
    public float prevRemaining;
    public Animator animator;

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        switch (MotionType)
        {
            case MotionTypeT.WaypointPatrol:
                CurrentDestinationIndex = 0;
                navMeshAgent.SetDestination(Waypoints[0].position);
                break;

            case MotionTypeT.RandomWalk:
                DestinationMarker = new GameObject();
                DestinationMarker.name = "DestinationMarker_for_" + name;

                Vector3 randomStep = MaxStepSize * Random.onUnitSphere;
                DestinationMarker.transform.position = transform.position + randomStep;
                navMeshAgent.SetDestination(DestinationMarker.transform.position);
                break;
        }
        
       
    }



    // Update is called once per frame
    void Update()
    {
        switch (MotionType)
        {
            case MotionTypeT.WaypointPatrol:
                WaypointPatrolMotion();
                break;
            case MotionTypeT.RandomWalk:
                RandomWalkMotion();
                break;
        }
    }
    private void WaypointPatrolMotion()
    {
        if (navMeshAgent.remainingDistance < 0.5f)
        {
            CurrentDestinationIndex++;
            if (CurrentDestinationIndex == Waypoints.Length) 
            CurrentDestinationIndex = 0;
            navMeshAgent.SetDestination(Waypoints[CurrentDestinationIndex].position);
       
        }
    }

    private void RandomWalkMotion()
    {
        bool asCloseAsPossible = false;
        if (navMeshAgent.velocity.magnitude < .01f)
            asCloseAsPossible = true;

        //if (asCloseAsPossible)
            //Debug.Log("Finding new place to go");
        Vector3 randomStep = MaxStepSize * Random.onUnitSphere;
        DestinationMarker.transform.position = transform.position + randomStep;
        navMeshAgent.SetDestination(DestinationMarker.transform.position);
    }
}
