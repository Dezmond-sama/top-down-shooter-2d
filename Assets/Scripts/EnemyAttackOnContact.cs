using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackOnContact : MonoBehaviour
{ 
    public float damage;

    public float kickForce = 500f;

    public float attackDelay;
    private float attackCooldown;

    // Update is called once per frame
    void Update()
    {
        if (attackCooldown > 0) attackCooldown -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (attackCooldown <= 0)
            {
                PlayerController pc = collision.GetComponent<PlayerController>();
                pc.HurtPlayer(damage, transform.position, kickForce);
                attackCooldown = attackDelay;
            }
        }
    }
}
