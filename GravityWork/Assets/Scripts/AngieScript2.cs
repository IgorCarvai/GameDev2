﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngieScript2 : MonoBehaviour {
	private string cpuName="";
	private bool playerCol=false;
	private GravityBody planetGravityBody;
	private Transform playerT;
	public NPCDialogue dialogue;
	public Transform planetT;
	Vector3 targetDirection;
	int triggersIn=0;
	bool lookAt;


	// Use this for initialization
	void Start () {

		playerT = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		planetGravityBody = GameObject.FindGameObjectWithTag ("Player").GetComponent<GravityBody> ();
		dialogue = gameObject.GetComponent<NPCDialogue>();
		dialogue.checkConditions(3,1);
		dialogue.hideText();
	}

	// Update is called once per frame 
	void Update () {
		if (lookAt)
			this.transform.LookAt (playerT.position);

		targetDirection = (transform.position - planetT.position).normalized;
		transform.rotation = Quaternion.FromToRotation (transform.up, targetDirection) * transform.rotation;

		if (playerCol == true && Input.GetKeyUp (KeyCode.I)) {
			
		}
	}

	void OnTriggerExit(Collider col){

		dialogue.checkConditions (3,1);
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

		dialogue.checkConditions (3,1);
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

