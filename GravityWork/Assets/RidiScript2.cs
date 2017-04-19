using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidiScript2 : MonoBehaviour {
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
				numText++;
				if (numText == 17)
					numText = 1;
				dialogue.checkConditions (2,numText);
				
			}
		}
	}
	void OnTriggerExit(Collider col){

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

