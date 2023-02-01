using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform m_target;

    [SerializeField, Range(0.1f, 1)]
    float smoothSpeed;

    Vector3 offset;
    Vector3 finalPos;
    Vector3 smoothPos;
    Vector3 velocity = Vector3.zero;

    private void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_target)
            return;

        finalPos = m_target.position + offset;
        smoothPos = Vector3.SmoothDamp(transform.position, finalPos, ref velocity, smoothSpeed);

        transform.position = smoothPos;

        transform.LookAt(m_target);
    }
}
