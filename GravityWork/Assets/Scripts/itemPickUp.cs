using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;

public class itemPickUp : MonoBehaviour {

	private Inventory inv;
	public GameObject itemp;
	public Item item;
	int ID;
	GameObject craftSlot;
	bool playerCol=false;

	void Start()
	{
		if (name.Contains("Poop"))
			ID = 0;
		if (name.Contains("Grass"))
			ID = 1;		
		if (name.Contains("Acid"))
			ID = 2;
		if (name.Contains("Split"))
			ID = 3;
		if (name.Contains("Bone"))
			ID = 4;
		if (name.Contains("Butterfly Scales"))
			ID = 5;		
		if (name.Contains("Cloth"))
			ID = 6;
		if (name.Contains("Coal"))
			ID = 7;
		if (name.Contains("Crystal"))
			ID = 8;
		if (name.Contains("Dirt"))
			ID = 9;		
		if (name.Contains("Flowers"))
			ID = 10;
		if (name.Contains("Hairball"))
			ID = 11;
		if (name.Contains("Landfill Trash"))
			ID = 12;
		if (name.Contains("Match"))
			ID = 13;		
		if (name.Contains("Mirror Shards"))
			ID = 14;
		if (name.Contains("Oil"))
			ID = 15;
		if (name.Contains("Rocks"))
			ID = 16;
		if (name.Contains("Roots"))
			ID = 17;		
		if (name.Contains("Snakeskin"))
			ID = 18;
		if (name.Contains("Squirrel Fluff"))
			ID = 19;
		if (name.Contains("steel"))
			ID = 20;
		if (name.Contains("Tree Branch"))
			ID = 21;
		if (name.Contains("Water"))
			ID = 22;
		if (name.Contains("Wood"))
			ID = 23;		
		if (name.Contains("Fuel Tank"))
			ID = 24;
		if (name.Contains("LED Lights"))
			ID = 25;
		if (name.Contains("Engine"))
			ID = 26;
		if (name.Contains("Radiator"))
			ID = 27;		
		if (name.Contains("Sensors"))
			ID = 28;
		if (name.Contains("Solar Panels"))
			ID = 29;
		if (name.Contains("Solid Rocket Booster"))
			ID = 30;
		if (name.Contains("Thrusters"))
			ID = 31;
		if (name.Contains("Wire"))
			ID = 32;
		if (name.Contains("Cog"))
			ID = 33;		
		if (name.Contains("Screw"))
			ID = 34;
		if (name.Contains("Piston"))
			ID = 35;
		if (name.Contains("Pulse Pusher"))
			ID = 36;
		if (name.Contains("Ballistic Bomber"))
			ID = 37;		
		if (name.Contains("The Piercer"))
			ID = 38;
		if (name.Contains("Rapid Ripper"))
			ID = 39;

		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
		craftSlot = GameObject.Find("Inventory Panel");
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			playerCol = true;
			/*check amount in dragging stack, subract amount needed 
                make the thing, put item data on it and destroy the items*/

			//instantiate object
        
		}
	}
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			playerCol = false;
			/*check amount in dragging stack, subract amount needed 
                make the thing, put item data on it and destroy the items*/

			//instantiate object

		}
	}
	void Update () {
		if (Input.GetKeyUp (KeyCode.E) && playerCol == true) {
			Debug.Log ("ID is: " + ID);
			if (ID == 4) {
				for (int i = 0; i < 8; i++) {
					inv.AddItem (ID);
				}
			} else {
				inv.AddItem (ID);
			}
			Destroy (this.gameObject);
		}
	}
}
