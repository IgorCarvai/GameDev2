﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class Inventory : MonoBehaviour{

    GameObject inventoryPanel;
    GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    public ItemDatabase database;
	public AudioSource source;
	public AudioClip pickup;

    int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

    void Start()
    {
        database = GetComponent<ItemDatabase>();

        slotAmount = 20;
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;

        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }

    }

    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);
        //already existing, and stackable
        if (itemToAdd.Stackable && CheckIfItemExists(itemToAdd))
        {
			source.PlayOneShot (pickup,1f);
			for (int i = 0; i < items.Count; i++)
               if (items[i].ID == id)
               {
                    //child 0 is text obj
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
        }


        else //not stackable & hasn't exsisted
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1) //empty item
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
					source.PlayOneShot (pickup,1f);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().amount = 1;
                    //slot it's attached to
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.transform.position = Vector2.zero;

                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.name = itemToAdd.Title;

                    break;
                }
            }
        }
    }

    public void RemoveItem(int id)
    {
		Item itemToDestroy = database.FetchItemByID (id);
		GameObject item2 = GameObject.Find (itemToDestroy.Title);
		Destroy (item2);
		items.Remove (itemToDestroy);

    }

    bool CheckIfItemExists(Item item)
    {
        for (int i = 0; i < items.Count; i++)
            //already have one in inventory
            if (items[i].ID == item.ID)
                return true;

        return false;
    }
}
