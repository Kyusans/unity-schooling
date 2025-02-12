using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEnemy : MonoBehaviour
{

	Transform player;

	void Start()
	{
		player = GameObject.Find("Player").transform;
	}

	void Update()
	{
		var horizontal = Input.GetAxisRaw("Horizontal");
		if (horizontal != 0)
		{
			float direction = horizontal * 10;
			transform.position = new Vector3(-direction, 0, 0);
		}

	}

}
