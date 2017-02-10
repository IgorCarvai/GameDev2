using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code adapted from Sebastian Lague
/// </summary>
public class PlayerMovements : MonoBehaviour {


	Vector3 moveAmount;
	Vector3 smoothAmount;
	public float walkSpeed = 8;

	void Update(){
		Vector3 moveDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
		Vector3 targetMoveAmount = moveDirection * walkSpeed;
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothAmount, .15f);
	}
	void FixedUpdate(){
		GetComponent<Rigidbody> ().MovePosition (GetComponent<Rigidbody> ().position + transform.TransformDirection (moveAmount) * Time.fixedDeltaTime);
	}

}
