using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float BaseMovementSpeed;

    private Rigidbody2D rb;
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Left;
    public KeyCode Right;
    private Vector2 InputDirection;
    Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        BaseMovementSpeed = PlayerStats.Instance.stats[(int)Stats.Speed].Value;
        PlayerInput();
    }


    void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerInput()
    {
        InputDirection = Vector2.zero;
        if (Input.GetKey(Up))
        {
            InputDirection += Vector2.up;
        }
        if (Input.GetKey(Down))
        {
            InputDirection += Vector2.down;
        }
        if (Input.GetKey(Left))
        {
            InputDirection += Vector2.left;
            anim.SetTrigger("MoveLeft");

        }
        if (Input.GetKey(Right))
        {
            InputDirection += Vector2.right;
            anim.SetTrigger("MoveRight");
        }
    }

    private void PlayerMovement()
    {
        //rb.velocity = InputDirection * BaseMovementSpeed;
        rb.AddForce(InputDirection * BaseMovementSpeed, ForceMode2D.Impulse);
    }

    private void UpdateAnimation()
    {

    }
    

}
