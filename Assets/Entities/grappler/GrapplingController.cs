using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingController : MonoBehaviour
{
    [SerializeField] private PlayerMovement pl;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer line;
    [SerializeField] private DistanceJoint2D joint;

    [SerializeField] private LayerMask grappable;
    private void Awake()
    {
        joint.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position), Mathf.Infinity, grappable);
            if (hit)
            {
                line.SetPosition(0, firePoint.position);
                line.SetPosition(1, hit.point);
                joint.connectedAnchor = hit.point;
                joint.enabled = true;
                line.enabled = true;
            }
            pl.isGrappled = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            joint.enabled = false;
            line.enabled = false;
            pl.isGrappled = false;
        }
        if (joint.enabled)
        {
            line.SetPosition(0, firePoint.position);
        }
    }
}
