using System.Collections;
using System.Collections.Generic;

using UnityEngine.EventSystems;
using UnityEngine;

/// <summary>
/// Code adapted from Sebastian Lague
/// </summary>
public class PlayerMovements : MonoBehaviour {


	Vector3 moveAmount;
	Vector3 smoothAmount;
	bool canRotate=true;
	bool rotated=false;
	bool start = true;
	bool cpuCollided = false;
	bool enventoryOn=false;
	float verMovement;
	float speed;
	float buttonPressed=0;
	float reset=0;
	string cpuName;
	public float walkSpeed = 8;
	public float runSpeed = 16;
	public GameObject E;
	private GameObject tooltip;
	public GameObject craftSystem;
	public Animator playerAnim;


	void Start (){
		playerAnim = GetComponent<Animator>();
	}

	void toggleInventory(){
		if (E.activeSelf && !Input.GetMouseButton(0)) {
			if (GameObject.Find ("Description")!=null) {
				tooltip = GameObject.Find ("Description");
				tooltip.SetActive (false);
			}
			enventoryOn = false;
			E.SetActive(false);
		}
		else{
			E.SetActive(true);
			enventoryOn = true;
		}
	}
	void toggleCraft(){
		if (cpuCollided) {

			if (craftSystem.activeSelf) {
				craftSystem.SetActive (false);
			} else {
				craftSystem.SetActive (true);
			}

		}

	}
	void Update(){
		if (start) {
			E.SetActive (false);
			craftSystem.SetActive (false);
			start = false;
		}
		if(Input.GetKeyUp(KeyCode.E)){
			toggleInventory ();
			toggleCraft ();
		}


		//Debug.Log(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag ("Enviroment").transform.position));
		if(Input.GetButtonUp("Vertical"))
			canRotate=true;

		if ((Input.GetAxisRaw ("Vertical") < 0) && canRotate && !rotated) {
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
			if (!enventoryOn) {
				transform.Rotate (0, (-1 * Input.GetAxisRaw ("Horizontal")) * 1.2f, 0);
			}
			verMovement = verMovement * -1;
		} else {
			if (!enventoryOn) {
				transform.Rotate(0,(Input.GetAxisRaw ("Horizontal"))*1.2f,0);
			}
		}

		if (enventoryOn) {
			verMovement = 0f;
			speed = 0f;
			moveAmount = Vector3.zero;
		}
		Vector3 moveDirection = new Vector3 (0, 0, verMovement).normalized;
		Vector3 targetMoveAmount = moveDirection * speed;
		if (targetMoveAmount == Vector3.zero) {
			playerAnim.Play ("idle");
		} else {
			playerAnim.Play ("Run");
		}
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothAmount, .15f);



	}
	void FixedUpdate(){
		GetComponent<Rigidbody> ().MovePosition (GetComponent<Rigidbody> ().position + transform.TransformDirection (moveAmount) * Time.fixedDeltaTime);
	}
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "CPU") {
			cpuCollided = false;
		}
	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "CPU") {
			cpuCollided = true;
			cpuName = col.gameObject.name;
		}
	}

}
