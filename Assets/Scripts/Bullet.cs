using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float spinSpeed;
    public Rigidbody2D rb;
    public int damage = 10;
    public GameObject impactEffect;
    public float TimeToLive = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, TimeToLive);
    }

    void Update()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        enemy?.TakeDamage(damage);

        Boss boss = hitInfo.GetComponent<Boss>();
        boss?.TakeDamage(damage);
        
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}