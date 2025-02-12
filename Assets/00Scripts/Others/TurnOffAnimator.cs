using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAnimator : MonoBehaviour {

	[SerializeField] Animator playerAnimator;
	void turnOffAttackAnimator(){
			playerAnimator.SetBool("attack", false);
	}
}
