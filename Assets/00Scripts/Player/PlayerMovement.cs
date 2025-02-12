using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody2D rb;
	private float jumpForce = 7f;
	// [SerializeField] GameObject event1;
	// [SerializeField] GameObject event2;
	// [SerializeField] GameObject event3;
	[SerializeField] private float moveSpeed = 4f;
	[SerializeField] Animator animator;
	[SerializeField] GameObject fireBall;
	[SerializeField] Transform firePoint;
	private bool isGrounded = false;
	private bool powerup = false;
	private int jumpCount = 0;
	private int maxJumps = 2;
	AudioSource[] audioSources;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		audioSources = GetComponents<AudioSource>();
	}

	void Update()
	{
		var horizontal = Input.GetAxisRaw("Horizontal");

		// walk
		animator.SetFloat("speed", Mathf.Abs(horizontal));
		rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

		// face left or right
		if (horizontal != 0)
		{
			transform.localScale = new Vector3(Mathf.Sign(horizontal) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}

		// ambak
		if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
		{
			animator.SetBool("isJump", true);
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			jumpCount++;
			audioSources[0].Play();
		}

		// hulog
		if (transform.position.y < -8)
		{
			SceneManager.LoadScene("GameOver");
		}

		// attack
		if (Input.GetKeyDown(KeyCode.L))
		{
			animator.SetBool("attack", true);
			StartCoroutine(ResetAttackAnimation());
			
			GameObject fireballInstance = Instantiate(fireBall, firePoint.position, firePoint.rotation);
			FireballController fireballController = fireballInstance.GetComponent<FireballController>();
			fireballController.SetDirection(transform.localScale.x);
		}

		// run
		if (Input.GetKey(KeyCode.LeftShift) && horizontal != 0)
		{
			animator.SetBool("isRunning", true);
			moveSpeed = 8f;
			Debug.Log("Run");
		}
		else
		{
			animator.SetBool("isRunning", false);
			moveSpeed = 4f;
		}
	}

	private IEnumerator ResetAttackAnimation()
	{
		yield return new WaitForSeconds(0.35f);
		animator.SetBool("attack", false);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			animator.SetBool("isJump", false);
			isGrounded = true;
			jumpCount = 0;
		}
		else if (other.gameObject.CompareTag("Enemy"))
		{
			if (powerup)
			{
				PlayAudio(1);
				powerup = false;
				moveSpeed = 4f;
				transform.localScale = new Vector3(transform.localScale.x, 1.5f, transform.localScale.z);
			}
			else
			{
				SceneManager.LoadScene("GameOver");
			}
		}
		else if (other.gameObject.CompareTag("Finish"))
		{
			SceneManager.LoadScene("Win");
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			isGrounded = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		// if (other.gameObject.CompareTag("Event1"))
		// {
		// 	event1.SetActive(true);
		// }
		// else if (other.gameObject.CompareTag("Enemy"))
		// {
		// 	SceneManager.LoadScene("GameOver");
		// }
		// else if (other.gameObject.CompareTag("Event2"))
		// {
		// 	event2.SetActive(true);
		// }
		// else if (other.gameObject.CompareTag("Event3"))
		// {
		// 	event3.SetActive(true);
		// }
		// else 
		if (other.gameObject.CompareTag("Powerup"))
		{
			PlayAudio(2);
			powerup = true;
			moveSpeed = 8f;
			transform.localScale = new Vector3(transform.localScale.x, 2f, transform.localScale.z);
		}
	}

	void StopAllAudio()
	{
		for (int i = 0; i < audioSources.Length; i++)
		{
			if (audioSources[i].isPlaying)
			{
				audioSources[i].Stop();
			}
		}
	}

	void PlayAudio(int index)
	{
		StopAllAudio();
		audioSources[index].Play();
	}
}
