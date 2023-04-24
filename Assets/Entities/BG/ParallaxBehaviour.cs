using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField, Range(0, 1)] float parallaxStrength = 0.1f;
    [SerializeField] private bool disableVerticalParallax;
    Vector3 targetPreviousPosition;

    private void Start()
    {
        if (!followTarget)
        {
            followTarget = Camera.main.transform;
        }

        targetPreviousPosition = followTarget.position;

    }

    private void Update()
    {
        Vector3 delta = followTarget.position - targetPreviousPosition;
        if (disableVerticalParallax)
        {
            delta.y = 0;
        }
        targetPreviousPosition = followTarget.position;
        transform.position += delta * parallaxStrength;
    }
}
