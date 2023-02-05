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

    [SerializeField]
    float focusRange = 10;

    Unit_Player player;

    private FireController fireControl;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        cam = Camera.main;
        GameManager.instance.playerStats = this;

        if (!player)
        {
            player = GetComponent<Unit_Player>();
            fireControl = GetComponent<FireController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");


        if(FocusOnClosestEnemy(focusRange)){
            fireControl.ToggleFire(true);
            return;
        }

        fireControl.ToggleFire(false);

        FocusOnMousePointer();
    }

    public void IncreaseHealth(int value)
    {
        player.IncreaseHealth(value);
    }
    public void IncreaseSpeed(int value)
    {
        moveSpeed += value;
    }

    private void FixedUpdate()
    {
        Move(moveX, moveZ);
    }

    void FocusOnMousePointer()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        bool hit = Physics.Raycast(ray, out hitPosition, Mathf.Infinity, hitLayer);

        if (!hit)
            return;

        lookDir = hitPosition.point - transform.position;
        lookDir.y = 0;
        transform.forward = lookDir;
    }

    bool FocusOnClosestEnemy(float radius)
    {
        LayerMask worldLayer = LayerMask.NameToLayer("Enemy");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, 1 << worldLayer);
        
        if(hitColliders.Length == 0)
        {
            return false;
        }

        Collider closest = hitColliders[0];

        foreach (Collider hitCollider in hitColliders)
        {
            if(Vector3.Distance(hitCollider.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position))
            {
                closest = hitCollider;
            }
        }

        lookDir = closest.transform.position- transform.position;
        lookDir.y = 0;
        transform.forward = lookDir;

        return true;
    }
}
