using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyForGenerate
{
    public EnemyController enemyPrefab = null;
    public float enemyChanse = 1f;
}


public class EnemySpawnPoint : MonoBehaviour
{
    public float generationChanse = 1f;
    public List<EnemyForGenerate> enemyList;

    // Start is called before the first frame update
    void Start()
    {
        RoomController rc = GetComponentInParent<RoomController>();
        
        if ((rc != null && !rc.startRoom) && Random.Range(0f, 1f) < generationChanse)
        {
            float chance = 0f;
            for (int i = 0; i < enemyList.Count; ++i) chance += enemyList[i].enemyChanse;
            chance = Random.Range(0f, chance);
            for (int i = 0; i < enemyList.Count; ++i)
            {
                chance -= enemyList[i].enemyChanse;
                if (chance <= 0)
                {
                    Instantiate(enemyList[i].enemyPrefab, transform.position, Quaternion.identity, gameObject.transform.parent);
                    break;
                }
            }

        }
        Destroy(gameObject);
    }
}
