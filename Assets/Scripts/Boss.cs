using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    public int health = 300;
    public GameObject deathEffect;
    public float waitTime = 0.1f;
    private bool waitTimeIsRunning = false;

	void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
		shaderSpritesDefault = Shader.Find("Sprites/Default");
	}
	
	// Update is called once per frame
	void Update() 
    {
        if (waitTimeIsRunning == true)
        {
            if (waitTime > 0)
            {
                WhiteSprite();
                waitTime -= Time.deltaTime;
            }
            else
            {
                NormalSprite();
                waitTime = 0.1f;
                waitTimeIsRunning = false;
            }
        }
	}

    void WhiteSprite()
    {
        _spriteRenderer.material.shader = shaderGUItext;
        _spriteRenderer.color = Color.white;
    }

    void NormalSprite()
    {
        _spriteRenderer.material.shader = shaderSpritesDefault;
	    _spriteRenderer.color = Color.white;
    }

    public void TakeDamage(int damage)
    {
        waitTimeIsRunning = true;
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}