using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public float maxMana;
    public float mana;

    public float armor;
    public float regneraton;
    public float manaRegeneration;
    public float poisoned;

    public int currentLevel = 1;
    public float bulletSpeedModifier = 1f;
    public float bulletDamageModifier = 1f;
    public float bulletCooldownModifier = 1f;

    public float timeBetweenHit = 0.1f;

    public int score = 0;
    public int exp = 0;
    public int expToLevelUp = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health < maxHealth)
        {
            health += regneraton * Time.deltaTime;
            if (health > maxHealth) health = maxHealth;
        }
        if (mana < maxMana)
        {
            mana += manaRegeneration * Time.deltaTime;
            if (mana > maxMana) mana = maxMana;
        }
    }
    public void Init()
    {
        health = maxHealth;
        mana = maxMana;
    }

    public void AddExperience(int exp)
    {
        this.exp += exp;
        while (this.exp >= expToLevelUp)
        {
            LevelUp();
            this.exp = this.exp - expToLevelUp;
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public void LevelUp()
    {
        currentLevel++;
    }


}
