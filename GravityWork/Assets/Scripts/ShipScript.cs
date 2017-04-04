using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour {

	private GravityBody planetGravityBody;

	bool foundAlvin=false;
	bool foundRidi=false;
	bool foundTangie=false;
	// Use this for initialization
	void Start () {
		planetGravityBody = GameObject.FindGameObjectWithTag ("Player").GetComponent<GravityBody> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			nextScene ();
		}
	}
	void nextScene(){
		foundAlvin = planetGravityBody.FoundAlvin();
		foundRidi = planetGravityBody.FoundRidi ();
		foundTangie = planetGravityBody.FoundTangie ();
		if (Application.loadedLevel == 2&&foundTangie&&foundRidi&&foundAlvin) {
			Debug.Log ("DONE");
		}
	}
}
