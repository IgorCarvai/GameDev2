using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidiScript : MonoBehaviour {
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
		dialogue.checkConditions(2,numText);
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
					planetGravityBody.setFoundRidi (true);
					firstSight = false;
					countdown = true;
					calcText ();
					dialogue.checkConditions (2,numText);
				}
			}
		}
		if (countdown) {
			timer -= Time.deltaTime;
			if (timer < 0 && tpToBase) {
				countdown = false;
				tpToBase = false;
				hastped = true;
				transform.position = new Vector3 (-21.8f, 400.6f, -6.4f);
				transform.eulerAngles = new Vector3 (-5.58f, -266.5f, -1.74f);
				gameObject.GetComponent<SphereCollider>().radius=6;			
			}
		}

	}
	public void calcText(){
		foundAlvin = planetGravityBody.FoundAlvin();
		foundTangie = planetGravityBody.FoundTangie ();
		if (firstSight) {
			numText = 1;
		} else if (tpToBase) {
			numText = 2;
		} else if (hastped&&!foundTangie&&!foundAlvin) {
			numText = 3;
		} else if (!foundTangie) {
			numText = 4;
		} else if (!foundAlvin) {
			numText = 5;
		}
	}
	public bool FoundRidi(){
		return foundRidi;
	}
	void OnTriggerExit(Collider col){

		calcText ();
		dialogue.checkConditions (2,numText);
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
		dialogue.checkConditions (2,numText);
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

