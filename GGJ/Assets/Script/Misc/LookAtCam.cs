using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    Transform m_target;
    private void Awake()
    {
        m_target = Camera.main.transform;
    }
    private void LateUpdate()
    {
        transform.LookAt(m_target);
    }
}
