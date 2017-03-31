using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.EventSystems;

public class CraftingSystem : MonoBehaviour {

    public int count = 0;
    public Item craftItem;
    public GameObject itemp;

    GameObject craftSlot;
    private ItemDatabase database;
    private List<Item> dList= new List<Item>();
    private List<string> sortList = new List<string>();
    private Inventory inv;

    public static Dictionary<string, int> toMake = new Dictionary<string, int>();
    public Dictionary<string, int> craftItemIng = new Dictionary<string, int>(); //to check inventory items against, final item ingredients


    void Start()
    {
        database = GetComponent<ItemDatabase>();
        //dList = database.database;
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        craftSlot = GameObject.Find("FinishedItem");
    }

    public void makeThings()
    {
        /*read all the crafting system ingredients, compare it against all the items in the 
        item database and their ingredients, if there's a match,  
        use dictionary, subtract inventory item amounts and create item
        then clear the list
        */
        if (toMake.Count != 0)
        {
            foreach (KeyValuePair<string, int> pair in toMake) //look through entire list of items
            {
                foreach (Item item in database.database)//look through dictionary of ingredients
                {
                    if (item.Ingredients.Count != 0)
                    {
                        if (item.Ingredients.ContainsKey(pair.Key))//if dictionary entries match, and amounts are equal to or more than the amount required
                        {
                            if (pair.Value >= item.Ingredients[pair.Key]) //can only make something if have enough ingredients
                            {
                                if (toMake.Count == 1)//put first items recipes in
                                {
                                    sortList.Add(item.Slug);
                                }

                                if (sortList.Contains(item.Slug) && toMake.Count > 1 && count < toMake.Count)
                                {
                                    count++;
                                }
                            }
                        }
                    }

                    if (sortList.Contains(item.Slug) && count == item.Ingredients.Count && count == toMake.Count) //preview object can make
                    {
                        Debug.Log("can make a " + item.Slug);
                        craftItem = item;
                        craftItemIng = item.Ingredients;

                        //instantiate object
                        GameObject done = Instantiate(itemp) as GameObject;
                        done.GetComponent<ItemData>().item = inv.database.FetchItemByID(craftItem.ID);
                        done.GetComponent<ItemData>().amount = 1;
                        done.transform.SetParent(craftSlot.transform);
                        done.GetComponent<ItemData>().isDraggable = false;
                        done.transform.position = craftSlot.transform.position;
                        done.GetComponent<Image>().sprite = inv.database.FetchItemByID(craftItem.ID).Sprite;
                        done.GetComponent<Image>().color = new Color(1f, 1f, 1f, .5f);

                        return;
                    }

                    else if (count != toMake.Count && craftSlot.transform.childCount > 0)
                    {
                        Destroy(craftSlot.transform.GetChild(0).gameObject);
                        sortList.Clear();
                    }
                    //else if can't make items anymore, destroy preview
                }
            }
        }
    }
}
