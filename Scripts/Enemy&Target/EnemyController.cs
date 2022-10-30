using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public static EnemyController instance;
    public LayerMask layerWall;

    public float health = 3;
    public GameObject explosion;
    private float currentHealth;

    public float playerRange = 10f;

    public Rigidbody theRB;
    public float moveSpeed;

    public bool shouldShoot;
    public float fireRate = .5f;
    private float shotCounter;
    public GameObject bullet;
    public Transform firePoint;

    public bool isMeele;
    public float attackRate = 2.5f;
    private float attackCounter;
    public int damageAmount;
    public GameObject hand;
    public Transform handPlace;

    public SpriteRenderer sp;
    public Sprite closedMouth;
    public Sprite openMouth;


    private void Awake()
    {

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;

        GameObject temp = GameObject.Find("Main Camera");

        Transform camTransform = temp.transform;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 playerDirection = PlayerMotor.controller.transform.position - transform.position;
        Ray ray = new Ray(transform.position, PlayerMotor.controller.transform.position - transform.position);
        Debug.DrawRay(transform.position, PlayerMotor.controller.transform.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                if (Vector3.Distance(transform.position, PlayerMotor.controller.transform.position) <= 3)
                {
                    theRB.velocity = Vector3.zero;
                    if (isMeele)
                    {
                        attackCounter -= Time.deltaTime;
                        if (attackCounter <= 0)
                        {
                            Instantiate(hand, handPlace.position, handPlace.rotation);
                            attackCounter = attackRate;
                        }
                    }
                }
                if (Vector3.Distance(transform.position, PlayerMotor.controller.transform.position) < playerRange)
                {

                    theRB.velocity = playerDirection.normalized * moveSpeed;

                    if (shouldShoot)
                    {
                        sp.sprite = openMouth;
                        shotCounter -= Time.deltaTime;
                        if (shotCounter <= 0)
                        {
                            Instantiate(bullet, firePoint.position, firePoint.rotation);
                            shotCounter = fireRate;
                        }
                    }
                }
                else
                {
                    theRB.velocity = Vector3.zero;
                    sp.sprite = closedMouth;
                }
            }
            else if (hit.transform.tag != "Player")
            {
                theRB.velocity = Vector3.zero;
                if (shouldShoot)
                {
                    sp.sprite = closedMouth;
                }
            }
            else
            {
                theRB.velocity = Vector3.zero;
                if (shouldShoot)
                {
                    sp.sprite = closedMouth;
                }
            }
        }
    }


    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
