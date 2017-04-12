using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotTutorial : MonoBehaviour {

	bool playerCol = false;
	public Text Goal1;
	float timer=5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (playerCol) {
			Goal1.text = "Good work soldier";
			timer -= Time.deltaTime;
			if(timer<=0)
				Application.LoadLevel (2);
		}
	}
	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag == "Player") 
			playerCol = true;

	}
}
