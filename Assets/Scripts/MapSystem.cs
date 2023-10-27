using UnityEngine;

public class MapSystem : MonoBehaviour
{
    public GameObject TDLR;
    public GameObject TDL;
    public GameObject TDR;
    public GameObject TLR;
    public GameObject DLR;
    public GameObject TD;
    public GameObject TR;
    public GameObject TL;
    public GameObject DL;
    public GameObject DR;
    public GameObject LR;
    public GameObject T;
    public GameObject D;
    public GameObject L;
    public GameObject R;

    public Transform minimapCamera;

    public void UpdateMap(Room room, bool setPosition = true)
    {
        if (room.mapObject == null)
        {
            if (room.hasTopExit && room.hasBottomExit && room.hasLeftExit && room.hasRightExit)
            {
                room.mapObject = Instantiate(TDLR, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasTopExit && room.hasBottomExit && room.hasLeftExit)
            {
                room.mapObject = Instantiate(TDL, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasTopExit && room.hasBottomExit && room.hasRightExit)
            {
                room.mapObject = Instantiate(TDR, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasTopExit && room.hasLeftExit && room.hasRightExit)
            {
                room.mapObject = Instantiate(TLR, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasBottomExit && room.hasLeftExit && room.hasRightExit)
            {
                room.mapObject = Instantiate(DLR, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasTopExit && room.hasBottomExit)
            {
                room.mapObject = Instantiate(TD, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasTopExit && room.hasLeftExit)
            {
                room.mapObject = Instantiate(TL, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasTopExit && room.hasRightExit)
            {
                room.mapObject = Instantiate(TR, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasBottomExit && room.hasLeftExit)
            {
                room.mapObject = Instantiate(DL, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasBottomExit && room.hasRightExit)
            {
                room.mapObject = Instantiate(DR, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasLeftExit && room.hasRightExit)
            {
                room.mapObject = Instantiate(LR, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasTopExit)
            {
                room.mapObject = Instantiate(T, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasBottomExit)
            {
                room.mapObject = Instantiate(D, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasLeftExit)
            {
                room.mapObject = Instantiate(L, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }
            else if (room.hasRightExit)
            {
                room.mapObject = Instantiate(R, new Vector3(room.x, room.y, 0), Quaternion.identity);
            }            
        }
        if(setPosition)minimapCamera.position = new Vector3(room.x, room.y, minimapCamera.position.z);
        if (room.mapObject != null)
        {
            if (room.isStart)
            {
                room.mapObject.GetComponent<SpriteRenderer>().color = new Color(.5f, 1f, .5f);
            }
            else if (room.isEnd)
            {
                room.mapObject.GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, 1f);
            }
            else if (room.isRoute)
            {
                room.mapObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, .5f);
            }
            else if(room.currentRoom == null)
            {
                room.mapObject.GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
            }
            else
            {
                room.mapObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            }
        }
    }
    public void ShowAll(Level lvl)
    {
        foreach (Room r in lvl.level.Values) UpdateMap(r, false);
    }
}
