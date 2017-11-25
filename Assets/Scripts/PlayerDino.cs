using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerDino : MonoBehaviour {

    public GameObject flag;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Move(Vector3 destination)
    {
        agent.SetDestination(destination);
        flag.SetActive(true);
        flag.transform.position = destination;
        flag.transform.position = new Vector3(flag.transform.position.x, 0, flag.transform.position.z);      
    }

    public void Selected()
    {
        if (!agent.isStopped)
        {
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