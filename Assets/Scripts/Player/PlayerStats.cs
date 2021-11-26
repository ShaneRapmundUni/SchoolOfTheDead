
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// REPLACE WITH SCRIPTABLE OBJECTS
/// </summary>
public class PlayerStats : UnityEngine.MonoBehaviour
{
    private static PlayerStats _instance;

    public static PlayerStats Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public float BaseHealth;
    public float BaseDamage;
    public float BaseSpeed;
    public float BaseAttackSpeed;

    public float currentDamage = 0;

    public List<PlayerStat> stats;
    public TextMeshProUGUI text;
    public Rigidbody2D rb;

    private void Start()
    {
        PlayerStat stat = new PlayerStat(BaseHealth, "Health");
        stats.Add(stat);
        stat = new PlayerStat(BaseDamage, "Damage");
        stats.Add(stat);
        stat = new PlayerStat(BaseSpeed, "Speed");
        stats.Add(stat);
        stat = new PlayerStat(BaseAttackSpeed, "AttackSpeed");
        stats.Add(stat);
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        text.text = "Current Health: " + (stats[0].Value - currentDamage);
        for (int i = 0; i < stats.Count; i++)
        {
            text.text += "\n" + stats[i].StatName + ": " + stats[i].Value;
        }
    }

    public void AttackPlayer(int damage, Vector2 direction)
    {
        if (rb != null)
        {
            if (direction == Vector2.zero)
            {
                direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }
            rb.AddForce(direction * 40f, ForceMode2D.Impulse);
        }
        currentDamage += damage;
        GetComponent<AudioSource>().Play();
        if ((stats[0].Value - currentDamage) <= 0)
        {
            GameManager.Instance.EndGame();
        }
    }
}

public enum Stats
{
    Health,
    Damage,
    Speed,
    AttackSpeed
}
