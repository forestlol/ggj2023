using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public int maxHealth;
    [Header("Movement variables")]
    public int moveSpeed;
    [SerializeField]
    int turnSpeed;

    [HideInInspector]
    public Rigidbody rb;

    private Transform m_camera_Transform;
    [HideInInspector]
    public int currentHealth;

    // Start is called before the first frame update
    public void Start()
    {
        if(!rb)
            rb = GetComponent<Rigidbody>();

        m_camera_Transform = Camera.main.transform;
        currentHealth = maxHealth;
    }

    public virtual void Move(float moveX, float moveZ)
    {
        //rb.MovePosition(transform.position + (Vector3.forward * moveSpeed * Time.deltaTime * moveZ) + (Vector3.right * moveSpeed * Time.deltaTime * moveX));

        Vector3 forward = m_camera_Transform.forward;
        forward.y = 0;

        Vector3 right = m_camera_Transform.right;
        right.y = 0;

        forward.Normalize();
        right.Normalize();


        rb.MovePosition(transform.position + (forward * moveSpeed * Time.deltaTime * moveZ) + (right * moveSpeed * Time.deltaTime * moveX));
    }


    public virtual void Rotate(float value)
    {
        rb.MoveRotation(Quaternion.Euler(0, value * moveSpeed * Time.deltaTime, 0));
    }
}
