using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 moveVector;
    private Rigidbody2D rb;
    private Animator anim;

    private float hitCooldown;

    public float max_face_shift;

    private Vector2 mousePos = Vector2.zero;
    public GameObject deathEffect;

    public PlayerStats stats;

    public AudioSource healSound;
    public AudioSource damageSound;

    public GameObject face;

    private static bool isCreated;
    private CameraController cam;
    private CameraShake camShaker;

    public FloatingText hurtText;
    public Transform displayPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (isCreated)
        {
            Destroy(transform.gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
            isCreated = true;
            rb = GetComponent<Rigidbody2D>();
            stats = GetComponent<PlayerStats>();
            anim = GetComponent<Animator>();
            cam = FindObjectOfType<CameraController>();
            camShaker = cam.GetComponent<CameraShake>();
            stats.Init();
            Respawn();
        }

        
    }

    public void Respawn()
    {
        //health = maxHealth;
        //anim.SetBool("Move", false);
        RespawnPoint p = FindObjectOfType<RespawnPoint>();
        if (p != null)
        {
            transform.position = p.transform.position;
        }
        else
        {
            transform.position = Vector3.zero;
        }
        if (cam == null)
        {
            cam = FindObjectOfType<CameraController>();
        }
        if(cam != null)cam.SetCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitCooldown > 0)
        {
            hitCooldown -= Time.deltaTime;
            return;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveVector = new Vector2(x, y).normalized;
        bool moving = !(x == 0 && y == 0);

        rb.velocity = moveVector * speed;

        anim.SetBool("Move", moving);
        face.transform.localPosition = new Vector3(max_face_shift * x, face.transform.localPosition.y, face.transform.localPosition.z);

        
    }

    public void HurtPlayer(float damage, Vector3 damagePoint, float force)
    {
        damage -= stats.armor;
        if (damage < 0) damage = 0;
        stats.health -= damage;
        hitCooldown = stats.timeBetweenHit;
        
        rb.velocity = Vector2.zero;
        rb.AddForce((rb.position - new Vector2(damagePoint.x, damagePoint.y)).normalized * force);

        FloatingText damageDisplay = Instantiate(hurtText, displayPosition.position, Quaternion.identity);
        damageDisplay.SetText("" + damage, new Color(1f, .1f, .1f));

        //camShaker.StartCoroutine(camShaker.Shake(timeBetweenHit / 2f, .1f));
        camShaker.StartCoroutine(camShaker.Shake2(stats.timeBetweenHit / 4f, .2f, 2));

        if (stats.health <= 0) GameOver();
    }

    public void GameOver()
    {

    }
}
