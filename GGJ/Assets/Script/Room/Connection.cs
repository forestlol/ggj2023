using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public CONNECTION_DIRECTION m_direction;

    public Connection otherSide_Connection;

    [SerializeField]
    private GameObject Decoration;

    public Room m_room;

    public void ToggleConnection(bool istrue)
    {
        Decoration.SetActive(!istrue);
        GetComponent<Collider>().enabled = istrue;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            int offset_strength = 2;
            Vector3 offset = Vector3.zero;

            switch (m_direction)
            {
                case CONNECTION_DIRECTION.LEFT:
                    offset += Vector3.left * offset_strength;
                    break;
                case CONNECTION_DIRECTION.RIGHT:
                    offset += Vector3.right * offset_strength;
                    break;
                case CONNECTION_DIRECTION.UP:
                    offset += Vector3.up * offset_strength;
                    break;
                case CONNECTION_DIRECTION.DOWN:
                    offset += Vector3.down * offset_strength;
                    break;
            }

            other.transform.position = otherSide_Connection.transform.position + offset;
            Room_Manager.instance.GetCurrentRoom = otherSide_Connection.m_room;
        }
    }
}
