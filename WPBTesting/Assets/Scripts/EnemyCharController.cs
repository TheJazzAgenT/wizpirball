using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharController : MonoBehaviour
{ 
    public Transform target;
    public GameObject spawnPoint;
    Vector3 destination;
    NavMeshAgent agent;
    bool canRespawn = true;

    void Start()
    {
        // Cache agent component and destination
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
    }

    void Update()
    {
        // Update destination if the target moves one unit
        if (Vector3.Distance(destination, target.position) > 1.0f)
        {
            destination = target.position;
            agent.destination = destination;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "WATER" && canRespawn)
        {
            canRespawn = false;
            Invoke("respawnPlayer", 5);
        }
    }
    void respawnPlayer()
    {
        this.transform.position = spawnPoint.transform.position;
        this.transform.rotation = spawnPoint.transform.rotation;
        canRespawn = true;
    }
}
