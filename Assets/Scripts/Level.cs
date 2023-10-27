using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Coords
{
    public int x;
    public int y;

    public Coords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class Level
{
    public Dictionary<Coords, Room> level = new Dictionary<Coords, Room>();

    public Room getRoom(Coords coords)
    {
        if (level.ContainsKey(coords))
        {
            return level[coords];
        }
        else
        {
            return null;
        }
    }
    void Generate(Coords start, int length, bool isStart, bool mainPath)
    {
        bool ret = false;
        if (length <= 0) ret = true;
        if (ret)
        {
            if (mainPath && level.ContainsKey(start))
            {
                level[start].isEnd = true;
            }
            return;
        }
        if (!level.ContainsKey(start)) level.Add(start, new Room());
        if (mainPath && isStart)
        {
            level[start].isStart = true;
        }
        List<int> list = new List<int>();
        if (!level.ContainsKey(new Coords(start.x + 1, start.y))) list.Add(1);
        if (!level.ContainsKey(new Coords(start.x, start.y - 1))) list.Add(2);
        if (!level.ContainsKey(new Coords(start.x - 1, start.y))) list.Add(3);
        if (!level.ContainsKey(new Coords(start.x, start.y + 1))) list.Add(4);
        if (list.Count == 0)
        {
            if (mainPath)
            {
                level[start].isEnd = true;
            }
            return;
        }
        if (mainPath)
        {
            level[start].isRoute = true;
        }
        int i = Random.Range(0, list.Count);
        switch (list[i])
        {
            case 1:                
                level[start].hasRightExit = true;
                level.Add(new Coords(start.x + 1, start.y), new Room());
                level[new Coords(start.x + 1, start.y)].hasLeftExit = true;
                level[new Coords(start.x + 1, start.y)].x = start.x + 1;
                level[new Coords(start.x + 1, start.y)].y = start.y;
                level[new Coords(start.x + 1, start.y)].leftRoom = level[start];
                level[start].rightRoom = level[new Coords(start.x + 1, start.y)];
                Generate(new Coords(start.x + 1, start.y), length - 1, false, mainPath);
                break;                
            case 2:
                level[start].hasBottomExit = true;
                level.Add(new Coords(start.x, start.y - 1), new Room());
                level[new Coords(start.x, start.y - 1)].hasTopExit = true;
                level[new Coords(start.x, start.y - 1)].x = start.x;
                level[new Coords(start.x, start.y - 1)].y = start.y - 1;
                level[new Coords(start.x, start.y - 1)].topRoom = level[start];
                level[start].bottomRoom = level[new Coords(start.x, start.y - 1)];
                Generate(new Coords(start.x, start.y - 1), length - 1, false, mainPath);
                break;
            case 3:
                level[start].hasLeftExit = true;
                level.Add(new Coords(start.x - 1, start.y), new Room());
                level[new Coords(start.x - 1, start.y)].hasRightExit = true;
                level[new Coords(start.x - 1, start.y)].x = start.x - 1;
                level[new Coords(start.x - 1, start.y)].y = start.y;
                level[new Coords(start.x - 1, start.y)].rightRoom = level[start];
                level[start].leftRoom = level[new Coords(start.x - 1, start.y)];
                Generate(new Coords(start.x - 1, start.y), length - 1, false, mainPath);
                break;
            case 4:
                level[start].hasTopExit = true;
                level.Add(new Coords(start.x, start.y + 1), new Room());
                level[new Coords(start.x, start.y + 1)].hasBottomExit = true;
                level[new Coords(start.x, start.y + 1)].x = start.x;
                level[new Coords(start.x, start.y + 1)].y = start.y + 1;
                level[new Coords(start.x, start.y + 1)].bottomRoom = level[start];
                level[start].topRoom = level[new Coords(start.x, start.y + 1)];
                Generate(new Coords(start.x, start.y + 1), length - 1, false, mainPath);
                break;
            default:
                break;
        }
    }

    void GeneratePath(Coords start, int minLength, int maxLength, bool isStart, bool mainPath)
    {
        Generate(start, Random.Range(minLength, maxLength + 1), isStart, mainPath);
    }

    void GenerateRoutes(int minLength, int maxLength, float chanse, float chanseModificator, float lengthModificator, int iterations)
    {
        if (iterations <= 0) return;

        List<Coords> newRoutesCoords = new List<Coords>();
        foreach (Coords c in level.Keys)
        {
            int ch = Random.Range(0, 100);
            if (ch < 100 * chanse) newRoutesCoords.Add(c);
        }
        for (int i = 0; i < newRoutesCoords.Count; ++i)
        {
            GeneratePath(newRoutesCoords[i], minLength, maxLength, false, false);
        }
        GenerateRoutes(Mathf.RoundToInt(minLength * lengthModificator), Mathf.RoundToInt(maxLength * lengthModificator), chanse * chanseModificator, chanseModificator, lengthModificator, iterations - 1);
    }

    public void GenerateLevel(int minLength, int maxLength, int minAddRouteLength, int maxAddRouteLength, float addRouteChanse, float addRouteChanseDempf,float lengthDempf,int addRoutesIterations)
    {
        level = new Dictionary<Coords, Room>();
        GeneratePath(new Coords(0, 0), minLength, maxLength, true, true);
        GenerateRoutes(minAddRouteLength, maxAddRouteLength, addRouteChanse, addRouteChanseDempf, lengthDempf, addRoutesIterations);
    }

}
