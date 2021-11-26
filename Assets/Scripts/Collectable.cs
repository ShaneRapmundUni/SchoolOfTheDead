using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(100f, 500f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.score += 1;
            PlayerStats.Instance.gameObject.GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            GameObject.Destroy(gameObject);
        }

    }
}
