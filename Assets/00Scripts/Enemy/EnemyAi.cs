using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
	[SerializeField] Transform player;
	Animator animator;
	Rigidbody2D rb;
	float moveSpeed = 3f;
	float attackRange = 6f;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		float distanceToPlayer = Vector2.Distance(transform.position, player.position);

		// chase player
		if (distanceToPlayer <= attackRange)
		{
			if (player.position.x < transform.position.x)
			{
				transform.localScale = new Vector3(6, 6, 6);
			}
			else if (player.position.x > transform.position.x)
			{
				transform.localScale = new Vector3(-6, 6, 6);
			}
			animator.SetBool("attackPlayer", true);
			transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
		}
		else
		{
			animator.SetBool("attackPlayer", false);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("PlayerAttack"))
		{
			Debug.Log("Trigger");
			Destroy(gameObject);
		}
	}

}
