using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectForGenerate
{
    public GameObject prefab = null;
    public float chanse = 1f;
}

public class ObjectSpawnPoint : MonoBehaviour
{
    public float generationChanse = 1f;
    public List<ObjectForGenerate> objectList;

    // Start is called before the first frame update
    void Start()
    {
        RoomController rc = GetComponentInParent<RoomController>();

        if ((rc != null && !rc.startRoom) && Random.Range(0f, 1f) < generationChanse)
        {
            float chance = 0f;
            for (int i = 0; i < objectList.Count; ++i) chance += objectList[i].chanse;
            chance = Random.Range(0f, chance);
            for (int i = 0; i < objectList.Count; ++i)
            {
                chance -= objectList[i].chanse;
                if (chance <= 0)
                {
                    Instantiate(objectList[i].prefab, transform.position, Quaternion.identity, gameObject.transform.parent);
                    break;
                }
            }

        }
        Destroy(gameObject);
    }
}
