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
		if (name == "Poop")
			ID = 0;
		if (name == "Grass")
			ID = 1;		
		if (name == "Acid")
			ID = 2;
		if (name == "Split")
			ID = 3;
		if (name == "Bone")
			ID = 4;
		if (name == "Butterfly Scales")
			ID = 5;		
		if (name == "Cloth")
			ID = 6;
		if (name == "Coal")
			ID = 7;
		if (name == "Crystal")
			ID = 8;
		if (name == "Dirt")
			ID = 9;		
		if (name == "Flowers")
			ID = 10;
		if (name == "Hairball")
			ID = 11;
		if (name == "Landfill Trash")
			ID = 12;
		if (name == "Match")
			ID = 13;		
		if (name == "Mirror Shards")
			ID = 14;
		if (name == "Oil")
			ID = 15;
		if (name == "Rocks")
			ID = 16;
		if (name == "Roots")
			ID = 17;		
		if (name == "Snakeskin")
			ID = 18;
		if (name == "Squirrel Fluff")
			ID = 19;
		if (name == "Steel")
			ID = 20;
		if (name == "Tree Branch")
			ID = 21;
		if (name == "Water")
			ID = 22;
		if (name == "Wood")
			ID = 23;		
		if (name == "Fuel Tank")
			ID = 24;
		if (name == "LED Lights")
			ID = 25;
		if (name == "Engine")
			ID = 26;
		if (name == "Radiator")
			ID = 27;		
		if (name == "Sensors")
			ID = 28;
		if (name == "Solar Panels")
			ID = 29;
		if (name == "Solid Rocket Booster")
			ID = 30;
		if (name == "Thrusters")
			ID = 31;
		if (name == "Wire")
			ID = 32;
		if (name == "Cog")
			ID = 33;		
		if (name == "Screw")
			ID = 34;
		if (name == "Piston")
			ID = 35;
		if (name == "Pulse Pusher")
			ID = 36;
		if (name == "Ballistic Bomber")
			ID = 37;		
		if (name == "The Piercer")
			ID = 38;
		if (name == "Rapid Ripper")
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
			inv.AddItem (ID);
			Destroy (this.gameObject);
		}
	}
}
