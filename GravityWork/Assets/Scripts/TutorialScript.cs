using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {


	public GameObject MyText;
	public GameObject Inv;
	public Camera Cam;
	private bool playerCol=false;

	private Vector3 o = new Vector3(-180,0,0);
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {


		MyText.transform.LookAt (Cam.transform.position);
		MyText.transform.Rotate (0, 180, 0);

		if (this.name == "Mechanic") {
			if (Input.GetKeyUp (KeyCode.E) && playerCol == true) {
				MyText.SetActive (false);
			}
		}
		if (this.name == "Captain") {

			if (Input.GetKeyUp (KeyCode.E)) {
				//if Inv has the item we want then load the next scene
				Application.LoadLevel(2);
			}
		}

	}
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			playerCol = false;
		}
	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			playerCol = true;
		}
	}
}
