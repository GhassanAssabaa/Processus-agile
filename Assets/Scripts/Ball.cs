using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle paddle;
    [SerializeField] private float velocityMagnitue = 15f;

    public event EventHandler OnHitBottomCollider;

    private Rigidbody2D rb;
    private Vector2 direction;
    private float xPush = 2f;
    private float yPush = 15f;
    private float paddleOffset = 0.3f;
    private bool started = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if (!started)
        {
            StickToPaddle();
        }
        else
        {
            direction = rb.velocity;
            rb.velocity = direction.normalized * velocityMagnitue;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BottomCollider")
        {
            OnHitBottomCollider?.Invoke(this, EventArgs.Empty);
        }
    }

    private void StickToPaddle()
    {
        transform.position = paddle.transform.position + new Vector3(0, paddleOffset, 0);
    }

    public void OnStartKeyPressed(object o, EventArgs e)
    {
        if (!started) {
            started = true;
            rb.velocity = new Vector2(xPush, yPush);
        }
    }



}
