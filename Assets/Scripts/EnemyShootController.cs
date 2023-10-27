using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootController : MonoBehaviour
{
    public Transform firePoint;
    public float timeBetweenShootMin;
    public float timeBetweenShootMax;
    private float timeBetweenShootCooldown;
    public float chargeTime;
    private float chargeTimeCooldown;
    public GameObject bullet;

    public GameObject chargeEffect;
    private GameObject charge;

    private PlayerStats player;

    public bool canAttack = true;
    public bool isAttacking = false;

    public float maxDistance = 3f;
    private float maxDistanceSqr;


    // Start is called before the first frame update
    void Start()
    {
        maxDistanceSqr = maxDistance * maxDistance;
        player = FindObjectOfType<PlayerStats>();
        timeBetweenShootCooldown = Random.Range(timeBetweenShootMin,timeBetweenShootMax);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        if (timeBetweenShootCooldown > 0) timeBetweenShootCooldown -= Time.deltaTime;

        if (isAttacking)
        {
            chargeTimeCooldown -= Time.deltaTime;
            if (chargeTimeCooldown <= 0)
            {
                timeBetweenShootCooldown = Random.Range(timeBetweenShootMin, timeBetweenShootMax);
                isAttacking = false;
                Shoot();
            }
        }
        else if(timeBetweenShootCooldown <= 0 && Vector3.SqrMagnitude(transform.position - player.transform.position) < maxDistanceSqr)
        {
            isAttacking = true;
            if(chargeEffect) charge = Instantiate(chargeEffect, firePoint.position, Quaternion.identity);
            chargeTimeCooldown = chargeTime;
        }
    }

    void Shoot()
    {
        if (bullet.GetComponent<EnemyBulletController>() != null)
        {
            GameObject b = Instantiate(bullet, firePoint.position, Quaternion.identity);
            if (b)
            {
                b.transform.LookAt(player.transform);
            }
        }
        if (charge) Destroy(charge);
    }
}
