using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    public Transform firePoint;
    public float timeBetweenShoot;
    private float timeBetweenShootCooldown;
    public float timeBetweenAltShoot;
    private float timeBetweenAltShootCooldown;
    public GameObject bullet;
    public GameObject bulletAlt;
    public GameObject fireEffect;

    private PlayerStats player;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenShootCooldown > 0)
        {
            timeBetweenShootCooldown -= Time.deltaTime;
        }
        if (timeBetweenAltShootCooldown > 0)
        {
            timeBetweenAltShootCooldown -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0) && timeBetweenShootCooldown <= 0)
        {
            Shoot(firePoint,bullet,fireEffect);
            timeBetweenShootCooldown = timeBetweenShoot * player.bulletCooldownModifier;
        }

        if (Input.GetMouseButton(1) && timeBetweenAltShootCooldown <= 0)
        {
            Shoot(firePoint, bulletAlt, fireEffect);
            timeBetweenAltShootCooldown = timeBetweenAltShoot * player.bulletCooldownModifier;
        }

    }

    void Shoot(Transform firePoint, GameObject projectile, GameObject effect)
    {
        if (projectile.GetComponent<BulletController>() != null && projectile.GetComponent<BulletController>().manaCost <= player.mana)
        {
            BulletController bc = Instantiate(projectile, firePoint.position, firePoint.rotation).GetComponent<BulletController>();
            if (bc)
            {
                player.mana -= bc.manaCost;
                if (effect)
                {
                    Instantiate(effect, firePoint.position, firePoint.rotation, transform);
                }

                bc.speed = bc.speed * player.bulletSpeedModifier;
                bc.damage = bc.damage * player.bulletDamageModifier;
            }
        }
    }
}
