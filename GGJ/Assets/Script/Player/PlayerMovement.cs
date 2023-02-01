using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    [Space]

    [SerializeField]
    LayerMask hitLayer;

    float moveX, moveZ;

    Camera cam;
    Ray ray;
    Vector3 lookDir;
    RaycastHit hitPosition;
    float angle; 

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        ray = cam.ScreenPointToRay(Input.mousePosition);

        bool hit = Physics.Raycast(ray, out hitPosition, Mathf.Infinity, hitLayer);

        if (!hit)
            return;

        lookDir = hitPosition.point - transform.position;
        lookDir.y = 0;
        transform.forward = lookDir;
        
    }

    private void FixedUpdate()
    {
        Move(moveX, moveZ);
    }
}
