using System.Collections.Generic;
using UnityEngine;

public class FlowersMovement : MonoBehaviour
{
    [SerializeField] Transform flowerTransform;
    [SerializeField] List<Transform> destinations;

    int currentDesIndex;
    float speed = 3.0f;

    void Start()
    {
        transform.position = destinations[0].position;
        ShuffleDestinations();
        SetNextDestination();
    }

    void Update()
    {
        if (destinations.Count > 0)
        {
            if (Vector3.Distance(flowerTransform.position, destinations[currentDesIndex].position) < 0.1f)
            {
                SetNextDestination();
            }

            MoveFlower();
        }
    }

    void ShuffleDestinations()
    {
        destinations.Sort((x, y) => Random.Range(-1, 2));
    }

    void SetNextDestination()
    {
        currentDesIndex = (currentDesIndex + 1) % destinations.Count;
    }

    void MoveFlower()
    {
        Vector3 dir = (destinations[currentDesIndex].position - flowerTransform.position).normalized;

        flowerTransform.Translate(dir * Time.deltaTime * speed);
    }
}
