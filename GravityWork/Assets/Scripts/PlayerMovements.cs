using System.Collections;
using System.Collections.Generic;

using UnityEngine.EventSystems;
using UnityEngine;

/// <summary>
/// Code adapted from Sebastian Lague
/// </summary>
public class PlayerMovements : MonoBehaviour {
	
	private GameObject tooltip;
	public GameObject E;
	public GameObject craftSystem;
	public Animator anim;

	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float turnSmoothTime = .2f;
	public float speedSmoothTime =.1f;

	float turnSmoothVelocity;
	float speedSmoothVelocity;
	float currentSpeed;
	float reset =0;
	float buttonPressed=0;

	bool start = true;
	bool cpuCollided = false;


	bool running = false;
	Transform cameraT;

	void Start(){
		cameraT = Camera.main.transform;
		anim = GetComponent<Animator>();
	}

	void toggleInventory(){
		if (E.activeSelf && !Input.GetMouseButton(0)) {
			if (GameObject.Find ("Description")!=null) {
				tooltip = GameObject.Find ("Description");
				tooltip.SetActive (false);
			}
			E.SetActive(false);
		}
		else{
			E.SetActive(true);
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

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDir = input.normalized;

		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			//transform.eulerAngles = Vector3.left * Mathf.SmoothDampAngle (transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

		if (Input.GetButtonDown ("Vertical")) {
			if (reset > 0 && buttonPressed == 1) {
				running = true;
			} else {
				reset = .5f;
				buttonPressed += 1;
			}
		} else {
			if (Input.GetAxisRaw ("Vertical") == 0) {
				running = false;
			}
		}
		if (reset > 0) {
			reset -= 1 * Time.deltaTime;
		} else {
			buttonPressed = 0;
		}
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

		transform.Translate (transform.forward * currentSpeed * Time.deltaTime, Space.World);
		if (currentSpeed == 0) {
			anim.Play ("idle");
		} else {
			anim.Play ("Run");
		}
	}
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "CPU") {
			cpuCollided = false;
		}
	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "CPU") {
			cpuCollided = true;
		}
	}


}
