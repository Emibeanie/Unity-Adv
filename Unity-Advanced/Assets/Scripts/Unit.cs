using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Animator animator;
    
    [SerializeField] float speedAnimMin;
    [SerializeField] float moveDetect;

    bool _moveChange;
    bool _jumpChange;
    bool _isNavMeshMoving => navMeshAgent.velocity.magnitude > moveDetect;
    
    private void OnValidate()
    {
        navMeshAgent ??= GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        if (navMeshAgent.isOnOffMeshLink && !animator.GetBool("Jumping"))
        {
            animator.SetTrigger("Jump");
            animator.SetBool("Jumping", true);
        }
        if (_isNavMeshMoving)
        {
            var speedRatio = Mathf.InverseLerp(0, navMeshAgent.speed, navMeshAgent.velocity.magnitude);
            if (speedRatio > speedAnimMin) animator.SetFloat("MoveSpeed", speedRatio);
        }
        if (_isNavMeshMoving == _moveChange) return;
        animator.SetBool("IsMoving", _isNavMeshMoving);
        _moveChange = _isNavMeshMoving;
    }

    protected abstract void HandleMove();
}
