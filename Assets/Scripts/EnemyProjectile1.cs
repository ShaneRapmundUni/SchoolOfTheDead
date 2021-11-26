using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile1 : MonoBehaviour
{
    public float damage;
    public float range;
    public float speed;
    public Vector2 startingLocation;
    private Vector2 direction;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingLocation = transform.position;
        rb.velocity = direction * speed;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public void SetDirection(Vector2 inputDirection)
    {
        direction = inputDirection;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void Update()
    {
        if (range == -1)
        {
            return;
        }
        if (!despawning && Vector2.Distance(startingLocation, transform.position) > range)
        {
            StartCoroutine("DespawnTimer");
        }
    }
    private bool despawning = false;
    private IEnumerator DespawnTimer()
    {
        despawning = true;
        GetComponent<Collider2D>().enabled = false;
        rb.Sleep();
        rb.isKinematic = true;
        
        yield return new WaitForSeconds(1f);
        GameObject.Destroy(gameObject);
        despawning = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            return;
        }
        if (collision.gameObject.layer != 10) // replace with actual background layer or tags.
        {
            transform.parent = collision.transform;
            StartCoroutine("DespawnTimer");
        }
        PlayerStats playerComponent = collision.gameObject.GetComponent<PlayerStats>();
        if (playerComponent != null)
        {
            PlayerStats.Instance.AttackPlayer((int) damage, (PlayerStats.Instance.gameObject.transform.position - transform.position).normalized);
            transform.parent = collision.transform;
            StartCoroutine("DespawnTimer");

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            return;
        }
        if (collision.gameObject.layer != 10) // replace with actual background layer or tags.
        {
            transform.parent = collision.transform;
            StartCoroutine("DespawnTimer");
        }
        PlayerStats playerComponent = collision.gameObject.GetComponent<PlayerStats>();
        if (playerComponent != null)
        {
            PlayerStats.Instance.AttackPlayer((int)damage, (PlayerStats.Instance.gameObject.transform.position - transform.position).normalized);
            transform.parent = collision.transform;
            StartCoroutine("DespawnTimer");

        }
    }
}
