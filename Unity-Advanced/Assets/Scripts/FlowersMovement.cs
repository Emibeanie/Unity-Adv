using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlowersMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent flower;
    [SerializeField] List<Transform> destinations;

    int currentDesIndex;

    void Start()
    {
        ShuffleDes();
        SetNextDes();
    }

    void Update()
    {
        if(!flower.pathPending && flower.remainingDistance < 0.1f)
        {
            SetNextDes() ;
        }
    }

    private void ShuffleDes()
    {
        destinations.Sort((x, y) => Random.Range(-1, 2));
    }

    private void SetNextDes()
    {
        flower.SetDestination(destinations[currentDesIndex].position);
        currentDesIndex = (currentDesIndex + 1) % destinations.Count;
    }



}
