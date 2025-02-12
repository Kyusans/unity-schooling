using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 400.0f;
    float direction = 1;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(direction * speed * Time.deltaTime, rb.velocity.y);
    }

    public void SetDirection(float playerDirection)
    {
        direction = playerDirection;
    }
}