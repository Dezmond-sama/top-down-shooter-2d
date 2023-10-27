using System.Collections.Generic;
using UnityEngine;

public class WarpController : MonoBehaviour
{
    public List<RoomController> roomsPrefabs;
    public int warpSide;
    private FadeController fader;
    private PlayerController player;
    private LevelController level;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        level = FindObjectOfType<LevelController>();
        fader = FindObjectOfType<FadeController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Time.timeScale = 0;
            fader.FadeOut();

            level.Warp(warpSide);


            fader.FadeIn();
            Time.timeScale = 1;
        }
    }
}
