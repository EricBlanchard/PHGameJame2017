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
        flag.SetActive(true);
        flag.transform.position = destination;
        flag.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        agent.SetDestination(destination);
    }

    public void Selected()
    {
        if (!agent.isStopped)
        {
            flag.SetActive(true);
        }
    }

    private void UnSelected()
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