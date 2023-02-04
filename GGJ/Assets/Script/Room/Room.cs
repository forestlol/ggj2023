using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CONNECTION_DIRECTION { LEFT, RIGHT, UP, DOWN };

public class Room : MonoBehaviour
{
    public Vector2Int room_coordinate = new Vector2Int(0, 0);

    [SerializeField]
    private Connection left, right, up, down;

    Room connection_left, connection_right, connection_up, connection_down;
    bool hasCleared = false;

    private void Start()
    {
        left.m_room = this;
        right.m_room = this;
        up.m_room = this;
        down.m_room = this;
    }

    public void AddConnection(CONNECTION_DIRECTION direction, Room room)
    {
        switch (direction)
        {
            case CONNECTION_DIRECTION.LEFT:
                connection_left = room;

                left.m_direction = direction;
                left.otherSide_Connection = connection_left.right;
                break;

            case CONNECTION_DIRECTION.RIGHT:
                connection_right = room;

                right.m_direction = direction;
                right.otherSide_Connection = connection_right.left;
                break;

            case CONNECTION_DIRECTION.UP:
                connection_up = room;

                up.m_direction = direction;
                up.otherSide_Connection = connection_up.down;
                break;

            case CONNECTION_DIRECTION.DOWN:
                connection_down = room;

                down.m_direction = direction;
                down.otherSide_Connection = connection_down.up;
                break;
        }
    }

    public void ToggleConnection(bool istrue)
    {
        if(connection_left != null)
        {
            left.ToggleConnection(istrue);
        }

        if (connection_right != null)
        {
            right.ToggleConnection(istrue);
        }

        if (connection_up != null)
        {
            up.ToggleConnection(istrue);
        }

        if (connection_down != null)
        {
            down.ToggleConnection(istrue);
        }
    }
}
