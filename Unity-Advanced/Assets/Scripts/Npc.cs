using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Npc : Unit
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitTimeBetweenPoints;

    private bool _isMoving;

    private void Start()
    {
        _isMoving = true;
        HandleMove();
    }

    protected override void HandleMove()
    {
        StartCoroutine(Patrol());
    }

    private IEnumerator Patrol()
    {
        while (_isMoving)
        {
            foreach (var point in patrolPoints)
            {
                navMeshAgent.SetDestination(point.position);
                yield return new WaitUntil(() => Vector3.Distance(transform.position, point.position) <= 1);
                yield return new WaitForSeconds(waitTimeBetweenPoints);
            }
        }
    }
}