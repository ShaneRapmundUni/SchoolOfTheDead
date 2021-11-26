using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectile;
    private float attackTime;
    public KeyCode attack;
    private Vector2 direction;
    public Vector3 shootLocationOffset;

    void Start()
    {
        attackTime = 0;
    }

    void Update()
    {
        attackTime += Time.deltaTime;
        PlayerInput();
    }
    private void PlayerInput()
    {

        if (Input.GetKey(attack))
        {
            if (attackTime >= 1 / PlayerStats.Instance.stats[(int)Stats.AttackSpeed].Value)
            {
                attackTime = 0;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 currentPosition = transform.position;
                direction = (mousePosition - currentPosition).normalized;
                ShootProjectile();
            }
            
        }
    }
    private void ShootProjectile()
    {
        if (projectile != null)
        {
            GameObject projectileObj = Instantiate(projectile, transform.position + shootLocationOffset, Quaternion.identity);
            projectileObj.GetComponent<Projectile>().SetDirection(direction);
            projectileObj.GetComponent<Projectile>().SetDamage(PlayerStats.Instance.stats[(int)Stats.Damage].Value);
        }
        else
        {
            Debug.Log("No projectile Assigned");
        }
       
    }

}
