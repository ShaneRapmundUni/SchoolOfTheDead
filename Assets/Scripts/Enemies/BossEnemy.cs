using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public GameObject levelChanger;
    public GameObject projectile;
    public Vector3 shootLocationOffset;
    public override void Death()
    {

        Instantiate(levelChanger, transform.position, transform.rotation);
        Instantiate(collectable, transform.position + new Vector3(0,1,0), transform.rotation);
        Instantiate(collectable, transform.position + new Vector3(0, 1, 0), transform.rotation);
        Instantiate(collectable, transform.position + new Vector3(0, 1, 0), transform.rotation);
        PlayerStats.Instance.gameObject.GetComponent<AudioSource>().PlayOneShot(ACDeath);
        GameObject.Destroy(gameObject);
    }

    public override void Hit(float damage)
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.MoveTowards(transform.position, PlayerStats.Instance.gameObject.transform.position, speed * Time.deltaTime * 0.5f) - transform.position);
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

    public void Attack()
    {
        if (attackTime >= 1 / attackSpeed)
        {
            attackTime = 0;
            anim.SetTrigger("GhostAttack");
        }
    }

    public void Shoot()
    {
        if (attackTime * 2 >= 1 / attackSpeed)
        {
            attackTime = 0;
            if (projectile != null)
            {
                Vector2 playerPosition = PlayerStats.Instance.gameObject.transform.position;
                Vector2 currentPosition = transform.position;
                Vector2 direction = (playerPosition - currentPosition).normalized;
                if (health >= startingHealth / 2)
                {
                    EnemyProjectile1 projectileObj = Instantiate(projectile, transform.position + shootLocationOffset, Quaternion.identity).GetComponent<EnemyProjectile1>();
                    projectileObj.SetDirection(direction);
                    projectileObj.SetDamage(projectileObj.damage * GameManager.Instance.GameLevel);
                }
                else
                {
                    for (int i = 0; i < 360; i++)
                    {
                        EnemyProjectile1 projectileObj = Instantiate(projectile, transform.position + shootLocationOffset, Quaternion.identity).GetComponent<EnemyProjectile1>();
                        projectileObj.SetDirection(direction + new Vector2(Mathf.Sin(i), Mathf.Cos(i)));
                        projectileObj.SetDamage(projectileObj.damage * GameManager.Instance.GameLevel);
                        i += 60;
                    }
                }
            }
            else
            {
                Debug.Log("No projectile Assigned");
            }
        }
    }

    public override void PlayerHit()
    {
        GetComponent<AudioSource>().PlayOneShot(ACAttack);
        if (Vector2.Distance(transform.position, PlayerStats.Instance.gameObject.transform.position) <= 4f || 
            (health < startingHealth / 2 && Vector2.Distance(transform.position, PlayerStats.Instance.gameObject.transform.position) <= 6f))
        {
            PlayerStats.Instance.AttackPlayer(damage * GameManager.Instance.GameLevel, (PlayerStats.Instance.gameObject.transform.position - transform.position).normalized);
        }
    }


    public void Update()
    {
        attackTime += Time.deltaTime;
        if (Vector2.Distance(transform.position, PlayerStats.Instance.gameObject.transform.position) <= attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerStats.Instance.gameObject.transform.position, speed * Time.deltaTime);
        }

        if (health >= startingHealth / 2 && Vector2.Distance(transform.position, PlayerStats.Instance.gameObject.transform.position) <= 4f)
        {
            Attack();
        }else if (health < startingHealth / 2 && Vector2.Distance(transform.position, PlayerStats.Instance.gameObject.transform.position) <= 6f)
        {
            Attack();
        }
        else if(Vector2.Distance(transform.position, PlayerStats.Instance.gameObject.transform.position) <= 20f)
        {
            Shoot();
        }
    }
}
