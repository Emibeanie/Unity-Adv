using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Animator animator;
    [SerializeField] float speedAnimMin;
    [SerializeField] float moveDetect;

    bool _isMoving;
    bool _isNavMeshMoving => navMeshAgent.velocity.magnitude > moveDetect;

    private void OnValidate()
    {
        navMeshAgent ??= GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        HandleMove();
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        if (_isNavMeshMoving)
        {
            var speedRatio = Mathf.InverseLerp(0, navMeshAgent.speed, navMeshAgent.velocity.magnitude);
            if (speedRatio > 0.7f) animator.SetFloat("MoveSpeed", speedRatio);
        }
        if (_isNavMeshMoving == _isMoving) return;
        animator.SetBool("IsMoving", _isNavMeshMoving);
        _isMoving = _isNavMeshMoving;
    }

    private void HandleMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main == null) return;
            var cam = Camera.main;
            var mousePos = Input.mousePosition;
            var dir = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
            if (Physics.Raycast(cam.transform.position, dir - cam.transform.position, out var hit))
            {
                navMeshAgent.SetDestination(hit.point);
                // Debug.Log($"Destination: {hit.point}");
                // Debug.Log($"Mouse: {Input.mousePosition}");
            }
        }
    }
}
