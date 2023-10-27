using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float speed = 6;
    public float damage = 5f;
    public float force = 500f;
    private Rigidbody2D rb;
    public float lifetime = 10f;
    public GameObject explode;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.forward * speed;
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
        if (col.tag == "Player")
        {
            PlayerController player = col.GetComponent<PlayerController>();
            if (player != null)
            {
                player.HurtPlayer(damage,transform.position,force);
            }
            Explode();
        }
    }
    void Explode()
    {
        if(explode && transform)Instantiate(explode, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
