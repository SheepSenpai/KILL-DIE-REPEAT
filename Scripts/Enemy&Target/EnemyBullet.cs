using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damageAmount;

    public float bulletSpeed = 5f;

    public Rigidbody theRB;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerMotor.controller.transform.position - transform.position;
        direction.Normalize();
        direction = direction * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            IDamageable damageable = other.transform.GetComponent<IDamageable>();
            damageable?.Damage(damageAmount);
            Destroy(gameObject);
        }
        else if(other.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (other.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }
}
