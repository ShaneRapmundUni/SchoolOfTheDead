using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy1 : Enemy
{
    public override void Death()
    {
        Instantiate(collectable, transform.position, transform.rotation);
        Instantiate(collectable, transform.position, transform.rotation);
        Instantiate(collectable, transform.position, transform.rotation);
        PlayerStats.Instance.gameObject.GetComponent<AudioSource>().PlayOneShot(ACDeath);
        GameObject.Destroy(gameObject);
    }

    public override void Hit(float damage)
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.MoveTowards(transform.position, PlayerStats.Instance.gameObject.transform.position, speed * Time.deltaTime) - transform.position);
        }

        GetComponent<AudioSource>().PlayOneShot(ACDamage);
        health -= damage;
        healthBar.value = health;
        anim.SetTrigger("GhostHit");
        if (health <= 0)
        {
            Death();
        }
    }

    public override void PlayerHit()
    {
        GetComponent<AudioSource>().PlayOneShot(ACAttack);
        if (Vector2.Distance(transform.position, PlayerStats.Instance.gameObject.transform.position) <= 2f)
        {
            PlayerStats.Instance.AttackPlayer(damage, (PlayerStats.Instance.gameObject.transform.position - transform.position).normalized);
        }
    }

    public void Attack()
    {

    }
    public void Update()
    {
        attackTime += Time.deltaTime;
        if (Vector2.Distance(transform.position, PlayerStats.Instance.gameObject.transform.position) <= attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerStats.Instance.gameObject.transform.position, speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, PlayerStats.Instance.gameObject.transform.position) <= 2f)
        {
            if (attackTime >= 1 / attackSpeed)
            {
                attackTime = 0;
                anim.SetTrigger("GhostAttack");
            }
        }
    }
}
