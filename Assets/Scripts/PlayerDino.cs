using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerDino : MonoBehaviour {

    public GameObject flag;
    NavMeshAgent agent;
    EPLAYERDINOSTATE dinoState = EPLAYERDINOSTATE.IDLE;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Move(Vector3 destination)
    {
        agent.SetDestination(destination);
        flag.SetActive(true);
        flag.transform.position = destination;  
    }

    public void Selected()
    {
        if (!agent.isStopped)
        {
            //TODO:  Enable UI (Portait, menu options etc.)
            flag.transform.position = agent.destination;
            flag.SetActive(true);
        }
    }

    public void UnSelected()
    {
        flag.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Flag")
        {
            agent.isStopped = true;
            flag.SetActive(false);
        }
    }
    
}