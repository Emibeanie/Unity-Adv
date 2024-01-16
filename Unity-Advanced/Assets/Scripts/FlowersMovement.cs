using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowersMovement : MonoBehaviour
{
    [SerializeField] Transform flowerTransform;
    [SerializeField] List<Transform> destinations;
    int currentDestinationIndex;
    float speed = 3.0f; // Adjust the speed as needed

    void Start()
    {
        ShuffleDestinations();
        SetNextDestination();
    }

    void Update()
    {
        // Move the flower only if there is at least one destination
        if (destinations.Count > 0)
        {
            // Check if the flower has arrived at its destination
            if (Vector3.Distance(flowerTransform.position, destinations[currentDestinationIndex].position) < 0.1f)
            {
                SetNextDestination();
            }

            // Move the flower manually towards the destination
            MoveFlower();
        }
    }

    void ShuffleDestinations()
    {
        destinations.Sort((x, y) => Random.Range(-1, 2));
    }

    void SetNextDestination()
    {
        // Move to the next destination index (loop back to the start if reached the end)
        currentDestinationIndex = (currentDestinationIndex + 1) % destinations.Count;
    }

    void MoveFlower()
    {
        // Calculate the direction to the current destination
        Vector3 direction = (destinations[currentDestinationIndex].position - flowerTransform.position).normalized;

        // Move the flower towards the destination
        flowerTransform.Translate(direction * Time.deltaTime * speed);
    }
}
