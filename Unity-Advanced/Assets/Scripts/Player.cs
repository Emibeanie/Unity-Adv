using System;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private ParticleSystem clickEffect;
    protected override void Update()
    {
        base.Update();
        HandleMove();
    }

    protected override void HandleMove()
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
                clickEffect.transform.position = hit.point;
                clickEffect.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            WinUI.Instance.WinGame();
        }
    }
}
