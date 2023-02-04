using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField]
    int moveSpeed;
    [SerializeField]
    int turnSpeed;

    [HideInInspector]
    public Rigidbody rb;

    private Transform m_camera_Transform;

    // Start is called before the first frame update
    public void Start()
    {
        if(!rb)
            rb = GetComponent<Rigidbody>();

        m_camera_Transform = Camera.main.transform;
    }

    public virtual void Move(float moveX, float moveZ)
    {
        //rb.MovePosition(transform.position + (Vector3.forward * moveSpeed * Time.deltaTime * moveZ) + (Vector3.right * moveSpeed * Time.deltaTime * moveX));
        rb.MovePosition(transform.position + (m_camera_Transform.forward * moveSpeed * Time.deltaTime * moveZ) + (m_camera_Transform.right * moveSpeed * Time.deltaTime * moveX));
    }


    public virtual void Rotate(float value)
    {
        rb.MoveRotation(Quaternion.Euler(0, value * moveSpeed * Time.deltaTime, 0));
    }
}
