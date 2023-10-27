using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Slider healthDisplay;
    public Slider expDisplay;
    public Slider manaDisplay;
    public Text scoreDisplay;

    PlayerController player;
    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerStats = FindObjectOfType<PlayerStats>();        
        player.Respawn();

        healthDisplay.minValue = 0;
        expDisplay.minValue = 0;
        manaDisplay.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            scoreDisplay.text = "Score: "+ playerStats.score;

            healthDisplay.maxValue = playerStats.maxHealth;
            healthDisplay.value = playerStats.health;

            expDisplay.maxValue = playerStats.expToLevelUp;
            expDisplay.value = playerStats.exp;

            manaDisplay.maxValue = playerStats.maxMana;
            manaDisplay.value = playerStats.mana;
        }
    }
}
