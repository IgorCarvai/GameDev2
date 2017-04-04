using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngieScript : MonoBehaviour {
	private string cpuName="";
	private bool playerCol=false;
	private GravityBody planetGravityBody;
	private Transform playerT;
	public NPCDialogue dialogue;
	public Transform planetT;
	Vector3 targetDirection;
	int numText=1;
	int triggersIn=0;
	float timer=5;
	bool lookAt;
	bool countdown=false;
	bool firstSight=true;
	bool tpToBase=true;
	bool hastped =false;
	bool foundAlvin=false;
	bool foundRidi=false;
	bool foundTangie=false;


	// Use this for initialization
	void Start () {

		playerT = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		planetGravityBody = GameObject.FindGameObjectWithTag ("Player").GetComponent<GravityBody> ();
		dialogue = gameObject.GetComponent<NPCDialogue>();
		dialogue.checkConditions(3,numText);
		dialogue.hideText();
	}

	// Update is called once per frame 
	void Update () {
		if (lookAt)
			this.transform.LookAt (playerT.position);

		targetDirection = (transform.position - planetT.position).normalized;
		transform.rotation = Quaternion.FromToRotation (transform.up, targetDirection) * transform.rotation;

		if (playerCol == true) {
			if(Input.GetKeyUp (KeyCode.E)&&triggersIn==2){
				if (tpToBase) {
					planetGravityBody.setFoundTangie (true);
					firstSight = false;
					countdown = true;
					calcText ();
					dialogue.checkConditions (3,numText);
				}
			}
		}
		if (countdown) {
			timer -= Time.deltaTime;
			if (timer < 0 && tpToBase) {
				countdown = false;
				tpToBase = false;
				hastped = true;
				transform.position = new Vector3 (-49.57f, 471.54f, 28f);
				transform.eulerAngles = new Vector3 (-1.26f, 35.45f, 11.6f);
				gameObject.GetComponent<SphereCollider>().radius=7;
			}
		}

	}
	public void calcText(){
		foundAlvin = planetGravityBody.FoundAlvin();
		foundRidi = planetGravityBody.FoundRidi ();
		if (firstSight) {
			numText = 1;
		} else if (tpToBase) {
			numText = 2;
		} else if (hastped&&!foundRidi&&!foundAlvin) {
			numText = 3;
		} else if (!foundRidi) {
			numText = 4;
		} else if (!foundAlvin) {
			numText = 5;
		}
	}
	public bool FoundTangie(){
		return foundTangie;
	}
	void OnTriggerExit(Collider col){

		calcText ();
		dialogue.checkConditions (3,numText);
		if (col.gameObject.tag == "Player"&&col.GetType()==typeof(CapsuleCollider)) {
			triggersIn = triggersIn - 1;
			if (triggersIn == 0) {
				lookAt = false;
				dialogue.hideText ();
				playerCol = false;
				cpuName = "";
			}
		}
	}

	void OnTriggerEnter(Collider col){

		calcText ();
		dialogue.checkConditions (3,numText);
		if (col.gameObject.tag == "Player"&&col.GetType()==typeof(CapsuleCollider)) {
			triggersIn++;
			if (triggersIn == 1) {
				lookAt = true;
				dialogue.showText ();
				playerCol = true;
				cpuName = this.name;
			}

		}
	}
}

