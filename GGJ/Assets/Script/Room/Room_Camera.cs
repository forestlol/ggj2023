using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Camera : MonoBehaviour
{
    private Camera m_cam;

    [SerializeField]
    private Room_Manager room_manager;

    [SerializeField]
    public Vector3 camera_offset;

    [SerializeField]
    private float camera_speed = 5;

    private void Start()
    {
        m_cam = Camera.main;
    }

    private void Update()
    {
        m_cam.transform.position = Vector3.Lerp(m_cam.transform.position, room_manager.GetCurrentRoom.transform.position + camera_offset, Time.deltaTime * camera_speed);
    }
}
