using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Level level;
    public bool levelGenerated = false;

    private static bool isCreated;

    public List<RoomController> roomsTDLR;
    public List<RoomController> roomsTDR;
    public List<RoomController> roomsTDL;
    public List<RoomController> roomsTLR;
    public List<RoomController> roomsDLR;
    public List<RoomController> roomsTD;
    public List<RoomController> roomsTL;
    public List<RoomController> roomsTR;
    public List<RoomController> roomsDL;
    public List<RoomController> roomsDR;
    public List<RoomController> roomsLR;
    public List<RoomController> roomsT;
    public List<RoomController> roomsD;
    public List<RoomController> roomsL;
    public List<RoomController> roomsR;

    public GameObject portalPrefab;

    PlayerController player;
    MapSystem map;

    public Coords currentCoords = new Coords(0,0);
    public Room currentRoom;

    private void Awake()
    {
        if (isCreated)
        {
            Destroy(transform.gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
            isCreated = true;
            player = FindObjectOfType<PlayerController>();
            map = GetComponent<MapSystem>();
            CreateLevel();
        }
    }
    
    public void CreateLevel()
    {
        if (map == null || levelGenerated) return;
        Debug.Log("CreateLevel");
        currentCoords = new Coords(0, 0);
        Time.timeScale = 0;        

        level = new Level();
        level.GenerateLevel(5,10,2,5,1f,.5f,.5f,3);

        //map.ShowAll(level);
        GotoRoom(currentCoords,true);        

        player.Respawn();

        levelGenerated = true;

        Time.timeScale = 1;
    }
    public void Warp(int warpSide)
    {
        switch (warpSide)
        {
            case 0:
                GotoRoom(new Coords(currentCoords.x - 1, currentCoords.y),false,warpSide);
                break;
            case 1:
                GotoRoom(new Coords(currentCoords.x, currentCoords.y + 1), false, warpSide);
                break;
            case 2:
                GotoRoom(new Coords(currentCoords.x + 1, currentCoords.y), false, warpSide);
                break;
            case 3:
                GotoRoom(new Coords(currentCoords.x, currentCoords.y - 1), false, warpSide);
                break;
            default:
                break;
        }
    }
    public void GotoRoom(Coords coords, bool startRoom, int warpSide = -1)
    {
        currentCoords = coords;
        if(currentRoom!=null && currentRoom.currentRoom != null)
        {
            currentRoom.currentRoom.gameObject.SetActive(false);
        }
        currentRoom = level.getRoom(coords);
        

        if (currentRoom.currentRoom == null)
        {
            currentRoom.currentRoom = CreateRoom();
            if(currentRoom.currentRoom != null) currentRoom.currentRoom.startRoom = startRoom;
        }
        else
        {
            currentRoom.currentRoom.gameObject.SetActive(true);
        }

        map.UpdateMap(currentRoom);

        switch (warpSide)
        {
            case 0:
                player.transform.position = currentRoom.currentRoom.rightPoint.position;
                break;
            case 1:
                player.transform.position = currentRoom.currentRoom.bottomPoint.position;
                break;
            case 2:
                player.transform.position = currentRoom.currentRoom.leftPoint.position;
                break;
            case 3:
                player.transform.position = currentRoom.currentRoom.topPoint.position;
                break;
            default:
                player.transform.position = Vector3.zero;
                break;
        }
        
        DestroyOnLoad[] objects = FindObjectsOfType<DestroyOnLoad>();
        for (int i = objects.Length - 1; i >= 0; i--)
        {
            Destroy(objects[i].gameObject);
        }
        FindObjectOfType<CameraController>().SetCameraPosition();
    }

    private RoomController CreateRoom()
    {
        if (currentRoom.hasTopExit && currentRoom.hasBottomExit && currentRoom.hasLeftExit && currentRoom.hasRightExit)
        {
            return InstantiateRoom(roomsTDLR);
        }
        else if (currentRoom.hasTopExit && currentRoom.hasBottomExit && currentRoom.hasLeftExit)
        {
            return InstantiateRoom(roomsTDL);
        }
        else if (currentRoom.hasTopExit && currentRoom.hasBottomExit && currentRoom.hasRightExit)
        {
            return InstantiateRoom(roomsTDR);
        }
        else if (currentRoom.hasTopExit && currentRoom.hasLeftExit && currentRoom.hasRightExit)
        {
            return InstantiateRoom(roomsTLR);
        }
        else if (currentRoom.hasBottomExit && currentRoom.hasLeftExit && currentRoom.hasRightExit)
        {
            return InstantiateRoom(roomsDLR);
        }
        else if (currentRoom.hasTopExit && currentRoom.hasBottomExit)
        {
            return InstantiateRoom(roomsTD);
        }
        else if (currentRoom.hasTopExit && currentRoom.hasLeftExit)
        {
            return InstantiateRoom(roomsTL);
        }
        else if (currentRoom.hasTopExit && currentRoom.hasRightExit)
        {
            return InstantiateRoom(roomsTR);
        }
        else if (currentRoom.hasBottomExit && currentRoom.hasLeftExit)
        {
            return InstantiateRoom(roomsDL);
        }
        else if (currentRoom.hasBottomExit && currentRoom.hasRightExit)
        {
            return InstantiateRoom(roomsDR);
        }
        else if (currentRoom.hasLeftExit && currentRoom.hasRightExit)
        {
            return InstantiateRoom(roomsLR);
        }
        else if (currentRoom.hasTopExit)
        {
            return InstantiateRoom(roomsT);
        }
        else if (currentRoom.hasBottomExit)
        {
            return InstantiateRoom(roomsD);
        }
        else if (currentRoom.hasLeftExit)
        {
            return InstantiateRoom(roomsL);
        }
        else if (currentRoom.hasRightExit)
        {
            return InstantiateRoom(roomsR);
        }
        else
        {
            return null;
        }
    }

    private RoomController InstantiateRoom(List<RoomController> rooms)
    {
        int index = Random.Range(0, rooms.Count);

        RoomController room = Instantiate(rooms[index], Vector3.zero, Quaternion.identity);
        if (currentRoom.isEnd)
        {
            PortalSpawner portalPoint = room.gameObject.GetComponentInChildren<PortalSpawner>();
            Instantiate(portalPrefab, portalPoint.transform.position, Quaternion.identity,room.gameObject.transform);
        }
        return room;
    }
}
