using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float damage;
    public float manaCost = 5f;
    private Rigidbody2D rb;
    public float lifetime = 10f;
    private PlayerController player;
    public GameObject explode;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Wall")
        {
            Explode();
        }
        if (col.tag == "Enemy")
        {
            EnemyController enemy = col.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.HurtEnemy(damage);
            }
            Explode();
        }
        //EnemyController ec = col.GetComponent<EnemyController>();
        //if (ec != null)
        //{
        //    int p = ec.HurtEnemy(damage, true);
        //    if (p > 0 && player != null)
        //        player.AddScore(p);
        //    Destroy(gameObject);
        //}
    }
    void Explode()
    {
        if(explode && transform)Instantiate(explode, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
