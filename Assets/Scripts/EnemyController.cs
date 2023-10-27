using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Stats
    public float speed;
    public float healthMax;
    private float health;
    public float armor;
    public int experienceToGive;
    public int scoreToGive = 5;
    public GameObject deathEffect;

    private bool isDead = false;
    private PlayerStats player;
    private Rigidbody2D playerRb;

    //Targeting
    public bool isTargeting = false;
    private bool isTargetingReset = false;
    public float targetingDistance = 10f;
    private float targetingDistanceSqr;
    public Transform target;

    //Random walking
    public bool isRandom;
    public Vector2 minPoint;
    public Vector2 maxPoint;
    public float minSquareDistanceToPoint = 0.5f;
    public float maxTimeToChangeDirection = 2f;
    private float cooldown;

    private Vector2 currentPoint;

    private EnemyShootController shooter;
    public FloatingText hurtText;

    public Transform displayPosition;


    Rigidbody2D rb;
    private Vector2 startPoint;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        if(player)playerRb = player.GetComponent<Rigidbody2D>();

        rb = GetComponent<Rigidbody2D>();
        health = healthMax;

        startPoint = rb.position;
        isTargetingReset = isTargeting;
        currentPoint = rb.position;
        cooldown = maxTimeToChangeDirection;

        shooter = GetComponent<EnemyShootController>();
        targetingDistanceSqr = targetingDistance * targetingDistance;

        InvokeRepeating("checkDistance", 1f, .5f);
    }
    private void OnEnable()
    {
        if(rb)ResetPosition();
    }
    public void ResetPosition()
    {
        rb.position = startPoint;
        health = healthMax;
        isTargeting = isTargetingReset;
        cooldown = maxTimeToChangeDirection;
    }

    private void Update()
    {
        if(cooldown>0)cooldown -= Time.deltaTime;
    }
    void FixedUpdate()
    {
        if (shooter != null && shooter.isAttacking)
        {
            rb.velocity = Vector2.zero;
        }
        else 
        {
            Vector2 direction = Vector2.zero;
            if (isTargeting && playerRb)
            {
                direction = (playerRb.position - rb.position).normalized;
            }
            else if (isRandom) 
            {
                if (Vector2.SqrMagnitude(currentPoint - rb.position) <= minSquareDistanceToPoint || cooldown < 0)
                {
                    currentPoint = GetNextPoint();
                }

                direction = (currentPoint - rb.position).normalized;
            }
            rb.velocity = direction * speed;            
        }
    }

    Vector2 GetNextPoint()
    {
        cooldown = maxTimeToChangeDirection;
        return new Vector2(Random.Range(minPoint.x, maxPoint.x), Random.Range(minPoint.y, maxPoint.y));
    }

    public void checkDistance()
    {
        if (targetingDistance > 0)
        {
            if (targetingDistanceSqr > Vector2.SqrMagnitude(rb.position - playerRb.position)) isTargeting=true;
        }
    }
    public void HurtEnemy(float damage)
    {
        damage -= armor;
        if (damage < 0) damage = 0;
        health -= damage;

        FloatingText damageDisplay = Instantiate(hurtText, displayPosition.position, Quaternion.identity);
        damageDisplay.SetText("" + damage, new Color(1f, 1f, 1f));

        isTargeting = true;
        if (health <= 0) EnemyDie();
    }

    void EnemyDie()
    {
        if (isDead) return;
        isDead = true;
        if (deathEffect != null) Instantiate(deathEffect, transform.position, Quaternion.identity);
        player.AddExperience(experienceToGive);
        player.AddScore(scoreToGive);
        Destroy(gameObject);
    }
}
