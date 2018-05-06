using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFollower : MonoBehaviour
{
    [SerializeField]
    private Transform lookFrom;
    [SerializeField]
    private Transform lookTo;
    [SerializeField]
    private Space offsetPositionSpace = Space.Self;
    [SerializeField]
    private bool lookAt = true;
    [SerializeField]
    private float height;
    [SerializeField]
    private float offset;

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (lookFrom == null && lookTo)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // Compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = lookTo.position + (((lookFrom.position - lookTo.position) + new Vector3(0, height, 0)).normalized * (Vector3.Distance(lookFrom.position, lookTo.position) + offset));
        }
        else
        {
            //transform.position = lookFrom.position + offsetPosition;
        }

        // Compute rotation
        if (lookAt)
        {
            transform.LookAt(lookTo);
        }
        else
        {
            transform.rotation = lookFrom.rotation;
        }
    }
}