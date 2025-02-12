using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	Rigidbody2D rb;
	float moveSpeed = 200f;
	[SerializeField] float direction;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{

		if (direction == 1)
		{
			transform.localScale = new Vector3(-8, transform.localScale.y, transform.localScale.z);
		}
		else if (direction == -1)
		{
			transform.localScale = new Vector3(8, transform.localScale.y, transform.localScale.z);
		}

		rb.velocity = new Vector2(direction * moveSpeed * Time.deltaTime, rb.velocity.y);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		direction *= -1f;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		direction *= -1f;
	}
}
