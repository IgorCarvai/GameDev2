using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript: MonoBehaviour {


	public Transform player;
	public Inventory inv;
	public Camera Cam;
	private string cpuName="";
	private bool playerCol=false;
	private GameObject[] texts;
	private int numText = 1;
	public NPCDialogue dialogue;
	bool intro=true;
	bool findItem=true;
	bool goodbye=true;


	private Vector3 o = new Vector3(-180,0,0);
	// Use this for initialization
	void Start () {
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();

		dialogue = gameObject.GetComponent<NPCDialogue>();
	}
	
	// Update is called once per frame 
	void Update () {

		Vector3 targetPostition = new Vector3 (player.position.x, 
			                          this.transform.position.y, 
			                          player.position.z);
		this.transform.LookAt (targetPostition);


		if (Input.GetKeyUp (KeyCode.E) && playerCol == true) {
			if (intro) {
				numText++;
				dialogue.checkConditions (999,numText);
				intro = false;
			} else if (findItem) {
				foreach (Item item in inv.items) {
					if (item.Title == "Crystal") {
						numText++;
						dialogue.checkConditions (999,numText);
						findItem = false;
					}
				}
			} else if (goodbye) {
				Application.LoadLevel (2);
			}

		}
		
	}
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			if(!intro)
				dialogue.hideText ();
	
			playerCol = false;
			cpuName = "";
		}
	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			dialogue.showText ();
			playerCol = true;
			cpuName = this.name;
		}
	}
}
