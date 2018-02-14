using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharController : MonoBehaviour
{ 
    public GameObject spawnPoint;
    public Transform farEnd;
    public Transform nearEnd;
    private float secondsForOneLength = 10f;
    bool canRespawn = true;

    void Start()
    {
    }

    void Update()
    {
        transform.position = Vector3.Lerp(nearEnd.position, farEnd.position,
          Mathf.SmoothStep(0f, 1f,
            Mathf.PingPong(Time.time / secondsForOneLength, 1f)
         ));
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
