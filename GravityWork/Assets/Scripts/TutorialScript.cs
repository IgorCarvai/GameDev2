using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialScript: MonoBehaviour {


	public Transform player;
	public Inventory inv;
	public Camera Cam;
	private string cpuName="";
	private bool playerCol=false;
	private bool foundCrystal = false;
	private GameObject[] texts;
	private int numText = 1;
	public NPCDialogue dialogue;
	bool intro=true;
	bool findItem=true;
	bool goodbye=true;
	bool tryInventory=true;
	bool tipPressI=true;
	public Text Goal1;
	public GameObject door1;
	public GameObject door2;

	private Vector3 o = new Vector3(-180,0,0);
	// Use this for initialization
	void Start () {

		inv = GameObject.Find("Inventory").GetComponent<Inventory>();

		dialogue = gameObject.GetComponent<NPCDialogue>();
	}
	
	// Update is called once per frame 
	void Update () {
		if (foundCrystal == false) {
			foreach (Item item in inv.items) {
				if (item.Title == "Crystal") {
					Goal1.text = "Goal:\nTalk to the mechanic.";
					numText++;
					dialogue.checkConditions (4,numText);
					foundCrystal = true;
				}
			}
		}
		Vector3 targetPostition = new Vector3 (player.position.x, 
			                          this.transform.position.y, 
			                          player.position.z);
		this.transform.LookAt (targetPostition);

		if (playerCol == true) {
			if (intro) {
				dialogue.checkConditions (4, numText);
			} else if (foundCrystal &&tipPressI) {
				Goal1.text = "Goal:\nPress I to access inventory";
				if (Input.GetKeyUp (KeyCode.I)) {
					tipPressI = false;
					Goal1.text = "Goal:\nGive him the crystal";
					numText++;
					dialogue.checkConditions (4, numText);
				}
			}
		}
		if (Input.GetKeyUp (KeyCode.E) && playerCol == true) {
			if (intro) {
				intro = false;
				numText++;
				door1.SetActive (false);
				Goal1.text = "Goal:\nFind and interact with a crystal";
				dialogue.checkConditions (4, numText);
			}else if (goodbye&& tipPressI==false) {
				inv.RemoveItem (8);
				Goal1.text = "Goal:\nTalk to the Pilot";
				door2.SetActive (false);
				numText++;
				dialogue.checkConditions (4, numText);
				goodbye = false;
				//Application.LoadLevel (2);
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
