using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] float spawnTime = 2f;
	[SerializeField] float spawnDelay = 1f;
	[SerializeField] Transform player;

	void Spawn()
	{
		float enemyPositionX = player.localScale.x > 0 ? -8f : 8f;
		Instantiate(enemyPrefab, new Vector3(enemyPositionX, 0f, 0f), player.rotation);
	}
	void Start()
	{

	}

	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Debug.Log("Player entered the trigger");
			Destroy(gameObject);
			Spawn();
		}
	}
}
