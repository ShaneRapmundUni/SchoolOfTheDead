using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float health;


    public int damage = 1;
    public float speed = 1;
    public float attackSpeed = 1;
    public float attackTime;
    public Slider healthBar;
    public float startingHealth;
    public int attackRange = 10;
    public Rigidbody2D rb;
    public GameObject collectable;

    public AudioClip ACAttack;
    public AudioClip ACDamage;
    public AudioClip ACDeath;

    public Animator anim;

    private void Start()
    {
        int difficulty = GameManager.Instance.GameLevel;
        health = health * difficulty;
        startingHealth = health;
        damage = damage * difficulty;
        speed = speed * difficulty;
        attackSpeed = attackSpeed * difficulty;

        healthBar.maxValue = startingHealth;
        healthBar.minValue = 0;
        healthBar.value = startingHealth;
        anim = GetComponent<Animator>();
    }
    public virtual void Hit(float damage)
    {
       


    }

    public virtual void Death()
    {
       
    }

    public virtual void PlayerHit()
    {

    }


    /// <summary>
    /// REMOVE AND REPLACE WITH PROPER PATHFINDING ON CHILDREN CLASSES
    /// </summary>
    //public void Update()
    //{
      
        
    //}

}
