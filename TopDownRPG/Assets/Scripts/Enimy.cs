using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enimy : MonoBehaviour
{
    public int health = 5;
    private PlayerController player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public int Health
    {
        set
        {
            health = value;
            if(health <= 0)
                Destroy(gameObject);
        }
        get
        {
            return health;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("sword"))
        {
            Health -= player.damage;
        }
    }
}
