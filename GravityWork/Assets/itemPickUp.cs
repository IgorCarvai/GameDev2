using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;

public class itemPickUp : MonoBehaviour {

	private Inventory inv;
	public GameObject itemp;
	public Item item;
	GameObject craftSlot;

	void Start()
	{
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
		craftSlot = GameObject.Find("Inventory Panel");
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			
			/*check amount in dragging stack, subract amount needed 
                make the thing, put item data on it and destroy the items*/
		
			//instantiate object
			GameObject done = Instantiate (itemp) as GameObject;
			done.GetComponent<ItemData> ().item = inv.database.FetchItemByID (1);
			done.GetComponent<ItemData> ().amount = 1;
			done.transform.SetParent (craftSlot.transform);
			done.transform.position = craftSlot.transform.position;
			done.GetComponent<Image> ().sprite = inv.database.FetchItemByID (1).Sprite;
			done.name = inv.database.FetchItemByID (1).Title;

			inv.items.Add (item);
			Destroy (this.gameObject);
		}
	}

}
