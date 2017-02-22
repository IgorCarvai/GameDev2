using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code adapted from Sebastian Lague
/// </summary>
public class PlayerMovements : MonoBehaviour {


	Vector3 moveAmount;
	Vector3 smoothAmount;
	bool canRotate=true;
	bool rotated=false;
	public float walkSpeed = 8;
	public float runSpeed = 16;
	float verMovement;
	float speed;
	float buttonPressed=0;
	float reset=0;

	void Update(){
		//Debug.Log(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag ("Enviroment").transform.position));
		if(Input.GetButtonUp("Vertical"))
			canRotate=true;

		if ((Input.GetAxisRaw ("Vertical") < 0) && canRotate) {
			if (rotated) {
				rotated = false;
			} else {
				rotated = true;
			}
			var player = GameObject.FindGameObjectWithTag ("Player").transform;
			transform.FindChild ("Main Camera").transform.parent = null;
			transform.Rotate(0,180,0);
			canRotate = false;
			GameObject.FindGameObjectWithTag ("MainCamera").transform.parent = player;
		}
		if ((Input.GetAxisRaw ("Vertical") > 0) && canRotate && rotated) {
			if (rotated) {
				rotated = false;
			} else {
				rotated = true;
			}
			var player = GameObject.FindGameObjectWithTag ("Player").transform;
			transform.FindChild ("Main Camera").transform.parent = null;
			transform.Rotate(0,180,0);
			canRotate = false;
			GameObject.FindGameObjectWithTag ("MainCamera").transform.parent = player;
		}
		if (Input.GetAxisRaw ("Vertical") == 0)
				speed = walkSpeed;
		if (Input.GetButtonDown ("Vertical")) {
			if (reset > 0 && buttonPressed == 1) {
				speed = runSpeed;
			} else {
				reset = .5f;
				buttonPressed += 1;
			}
		}
		if (reset > 0) {
			reset -= 1 * Time.deltaTime;
		} else {
			buttonPressed = 0;
		}
			

		verMovement = Input.GetAxisRaw ("Vertical");
		if (rotated) {
			transform.Rotate (0, -1 * Input.GetAxisRaw ("Horizontal"), 0);
			verMovement = verMovement * -1;
		} else {
			transform.Rotate(0,Input.GetAxisRaw ("Horizontal"),0);
		}
		Vector3 moveDirection = new Vector3 (0, 0, verMovement).normalized;
		Vector3 targetMoveAmount = moveDirection * speed;



		if (Input.GetKey (KeyCode.Space)) {
			targetMoveAmount.y = 3;
		}
		//Change this according to the size of the planet
		int dist = 14;
		if (Vector3.Distance (transform.position, GameObject.FindGameObjectWithTag ("Enviroment").transform.position) > dist) {
			targetMoveAmount.y = -3;
			//Debug.Log ("Hello");
			//Debug.Log ("bY");
		}
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothAmount, .15f);

		//Debug.Log ("moveAmount:" + moveAmount);
			
	}
	void FixedUpdate(){
		GetComponent<Rigidbody> ().MovePosition (GetComponent<Rigidbody> ().position + transform.TransformDirection (moveAmount) * Time.fixedDeltaTime);
	}

}
