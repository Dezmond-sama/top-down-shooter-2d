using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlacementWithChanse
{
    public PlacementScheme placementScheme;
    public float chanse = 1f;
}

public class RoomController : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public Transform topPoint;
    public Transform bottomPoint;

    public WarpController leftWarp;
    public WarpController rightWarp;
    public WarpController topWarp;
    public WarpController bottomWarp;

    public bool startRoom;

    public List<PlacementWithChanse> placementVariant;
    public float generationChanse = 1f;

    private void Start()
    {
        if (!startRoom && Random.Range(0f, 1f) < generationChanse)
        {
            float chance = 0f;
            for (int i = 0; i < placementVariant.Count; ++i) chance += placementVariant[i].chanse;
            chance = Random.Range(0f, chance);
            for (int i = 0; i < placementVariant.Count; ++i)
            {
                chance -= placementVariant[i].chanse;
                if (chance <= 0)
                {
                    Instantiate(placementVariant[i].placementScheme, transform.position, Quaternion.identity, gameObject.transform);
                    break;
                }
            }

        }
    }
}
