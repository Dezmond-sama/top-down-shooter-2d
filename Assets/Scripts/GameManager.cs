using UnityEngine;

public class GameManager : MonoBehaviour
{

    void Start()
    {
        LevelController lc = FindObjectOfType<LevelController>();
        if (lc != null) lc.CreateLevel();
    }

}
