using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {


	public GameObject MyText;
	public Inventory inv;
	public Camera Cam;
	private string cpuName="";
	private bool playerCol=false;

	private Vector3 o = new Vector3(-180,0,0);
	// Use this for initialization
	void Start () {
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {


		MyText.transform.LookAt (Cam.transform.position);
		MyText.transform.Rotate (0, 180, 0);

		if (cpuName == "Mechanic") {
			if (Input.GetKeyUp (KeyCode.E) && playerCol == true) {
				MyText.SetActive (false);
			}
		}
		if (cpuName == "Captain") {

			if (Input.GetKeyUp (KeyCode.E)) {
				foreach (Item item in inv.items) { //look through entire list of items

					if (item.Title == "Crystal") {

						Application.LoadLevel(2);
					}
				}
			}
		}

	}
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			playerCol = false;
			cpuName = "";
		}
	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			playerCol = true;
			cpuName = this.name;
		}
	}
}
