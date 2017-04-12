﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvinScrpit : MonoBehaviour {

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
		dialogue.checkConditions(1,numText);
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
					planetGravityBody.setFoundAlvin (true);
					firstSight = false;
					countdown = true;
					calcText ();
					dialogue.checkConditions (1,numText);
				}
			}
		}
		if (countdown) {
			timer -= Time.deltaTime;
			if (timer < 0 && tpToBase) {
				countdown = false;
				tpToBase = false;
				hastped = true;
				transform.position = new Vector3 (-83f, 467.22f, -34.4f);
				transform.eulerAngles = new Vector3 (0.533f, -24.11f, 18.259f);
				gameObject.GetComponent<SphereCollider>().radius=7;
			}
		}

	}
	public void calcText(){
		foundTangie = planetGravityBody.FoundTangie();
		foundRidi = planetGravityBody.FoundRidi ();


		if (firstSight) {
			numText = 1;
		} else if (tpToBase) {
			numText = 2;
		} else if (hastped&&!foundRidi&&!foundTangie) {
			numText = 3;
		} else if (!foundRidi) {
			numText = 4;
		} else if (!foundTangie) {
			numText = 5;
		}
	}
	public bool FoundAlvin(){
		return foundAlvin;
	}
	void OnTriggerExit(Collider col){
		calcText ();
		dialogue.checkConditions (1,numText);
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
		dialogue.checkConditions (1,numText);
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
