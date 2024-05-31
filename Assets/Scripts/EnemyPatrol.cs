using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public float waitTime = 2f;
    private bool waitTimeIsRunning = false;
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointA.transform;
        anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else{
            rb.velocity = new Vector2(-speed, 0);
        }
    
        if(Vector2.Distance(transform.position, currentPoint.position) < 1.5f && currentPoint == pointB.transform)
        {
            waitTimeIsRunning = true;
            if (waitTimeIsRunning)
            {
                if (waitTime > 0)
                {
                    rb.velocity = new Vector2(0, 0);
                    waitTime -= Time.deltaTime;
                    anim.SetBool("isRunning", false);
                }
                else
                {
                    Flip();
                    anim.SetBool("isRunning", true);
                    currentPoint = pointA.transform;
                    waitTime = 2;
                    waitTimeIsRunning = false;
                }
            }
        }
        else if(Vector2.Distance(transform.position, currentPoint.position) < 1.5f && currentPoint == pointA.transform)
        {
            waitTimeIsRunning = true;
            if (waitTimeIsRunning)
            {
                if (waitTime > 0)
                {
                    rb.velocity = new Vector2(0, 0);
                    waitTime -= Time.deltaTime;
                    anim.SetBool("isRunning", false);
                }
                else
                {
                    Flip();
                    anim.SetBool("isRunning", true);
                    currentPoint = pointB.transform;
                    waitTime = 2;
                    waitTimeIsRunning = false;
                }
            }  
        }
    }
    
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
