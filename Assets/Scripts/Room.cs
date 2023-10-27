using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public GameObject mapObject = null;

    public bool hasTopExit = false;
    public bool hasRightExit = false;
    public bool hasBottomExit = false;
    public bool hasLeftExit = false;
    public bool isStart = false;
    public bool isEnd = false;
    public bool isRoute = false;
    public int x = 0;
    public int y = 0;

    public Room topRoom = null;
    public Room rightRoom = null;
    public Room bottomRoom = null;
    public Room leftRoom = null;

    public RoomController currentRoom;
}
