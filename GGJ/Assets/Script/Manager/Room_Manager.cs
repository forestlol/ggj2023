using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Manager : MonoBehaviour
{
    public static Room_Manager instance;

    [SerializeField]
    private Transform map_transform;

    [SerializeField]
    private List<Room> m_room_prefab;

    [SerializeField]
    private Room m_EndRoom_prefab;

    [SerializeField]
    private Room current_room;

    [SerializeField]
    private Dictionary<Vector2Int, Room> map = new Dictionary<Vector2Int, Room>();

    private int room_left = 0, endRoomIndex = 0;

    public Room GetCurrentRoom
    {
        get {
            return current_room;
        }

        set
        {
            current_room = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        room_left = GameManager.instance.room_size;
        endRoomIndex = Random.Range(1, GameManager.instance.room_size - 1);

        map.Add(Vector2Int.zero, current_room);
        
        GenerateLevel(current_room);
        ConnectAllRooms();

        current_room.hasCleared = true;
        current_room.ToggleConnection(true);

        //foreach (KeyValuePair<Vector2Int, Room> room in map)
        //{
        //    room.Value.ToggleConnection(true);
        //}
    }

    public void GenerateLevel(Room root_room)
    {
        List<Room> generatedRooms = GenerateRoomConnection(root_room);

        if (room_left <= 0)
        {
            return;
        }

        foreach (Room room in generatedRooms)
        {
            GenerateLevel(room);
        }
    }

    private List<Room> GenerateRoomConnection(Room room)
    {
        List<Room> generated_Rooms = new List<Room>();

        if (room_left <= 0)
        {
            return generated_Rooms;
        }


        //Up
        GenerateRoom(room.room_coordinate + Vector2Int.up, ref generated_Rooms);

        if (room_left <= 0)
        {
            return generated_Rooms;
        }


        //Down
        if (GenerateChance())
        {
            GenerateRoom(room.room_coordinate + Vector2Int.down, ref generated_Rooms);

            if (room_left <= 0)
            {
                return generated_Rooms;
            }
        }

        //Left
        if (GenerateChance())
        {
            GenerateRoom(room.room_coordinate + Vector2Int.left, ref generated_Rooms);

            if (room_left <= 0)
            {
                return generated_Rooms;
            }
        }

        //Right
        if (GenerateChance())
        {
            GenerateRoom(room.room_coordinate + Vector2Int.right, ref generated_Rooms);

            if (room_left <= 0)
            {
                return generated_Rooms;
            }
        }

        return generated_Rooms;
    }

    private Room GenerateRoom(Vector2Int coordinate, ref List<Room> roomList)
    {
        if (map.ContainsKey(coordinate))
        {
            return null;
        }
        Room newRoom;

        if (room_left == endRoomIndex)
        {
            newRoom = GameObject.Instantiate(m_EndRoom_prefab, map_transform);
        }
        else
        {
            newRoom = GameObject.Instantiate(m_room_prefab[Random.Range(0, m_room_prefab.Count)], map_transform);
        }

        newRoom.room_coordinate = coordinate;
        newRoom.gameObject.name = "Room " + coordinate;
        newRoom.transform.position = new Vector3(coordinate.x * 30, 0, coordinate.y * 30);

        map.Add(coordinate, newRoom);
        roomList.Add(newRoom);

        room_left--;
        return newRoom;
    }

    private bool GenerateChance()
    {
        int value = Random.Range(0, 100);

        return value > 15;
    }

    private void ConnectAllRooms()
    {
        foreach(KeyValuePair<Vector2Int, Room> room in map)
        {
            //Up
            if(map.ContainsKey(room.Value.room_coordinate + Vector2Int.up))
            {
                room.Value.AddConnection(CONNECTION_DIRECTION.UP ,map[room.Value.room_coordinate + Vector2Int.up]);
            }

            //Down
            if (map.ContainsKey(room.Value.room_coordinate + Vector2Int.down))
            {
                room.Value.AddConnection(CONNECTION_DIRECTION.DOWN, map[room.Value.room_coordinate + Vector2Int.down]);
            }

            //Left
            if (map.ContainsKey(room.Value.room_coordinate + Vector2Int.left))
            {
                room.Value.AddConnection(CONNECTION_DIRECTION.LEFT, map[room.Value.room_coordinate + Vector2Int.left]);
            }

            //Right
            if (map.ContainsKey(room.Value.room_coordinate + Vector2Int.right))
            {
                room.Value.AddConnection(CONNECTION_DIRECTION.RIGHT, map[room.Value.room_coordinate + Vector2Int.right]);
            }
        }
    }
}
