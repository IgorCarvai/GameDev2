using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LitJson;
using System.Globalization;
using UnityEngine;

public class ClickCraft : MonoBehaviour {

    int count, notin;
    private Inventory inv;
    public Item craftItem;
    public GameObject itemp;
    ItemDatabase database;
    GameObject craftSlot;

    private Dictionary<string, int> craftItemIng = new Dictionary<string, int>(); //to check inventory items against, final item ingredients

    void Start()
    {
        database = GetComponent<ItemDatabase>();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        craftSlot = GameObject.Find("FinishedItem");
    }

    public void onClick()
    {
        count = 0;
        notin = 0;
        //copy of final list
        Dictionary<string, int> finalList = new Dictionary<string, int>();
        finalList = CraftingSystem.toMake;

        /*read all the crafting system ingredients, compare it against all the items in the 
        item database and their ingredients, if there's a match,  
        use dictionary, subtract inventory item amounts and create item
        then clear the list
        */
        //Debug.Log("button pressed");

        foreach (Item item in inv.items) //look through entire list of items
        {
            foreach (KeyValuePair<string, int> pair in finalList)//look through dictionary of ingredients
            {
                //if dictionary entries match, and amounts are equal to or more than the amount required

                if (item.Ingredients.ContainsKey(pair.Key))
                {
                    if (pair.Value >= item.Ingredients[pair.Key]) //can only make something if have enough ingredients
                    {
                        //Debug.Log("recipe requires: " + item.Ingredients[pair.Key] + " currently have: " + pair.Value);
                        item.Ingredients[pair.Key] = pair.Value - item.Ingredients[pair.Key];

                        int updateCount = item.Ingredients[pair.Key];
                        string destroyThis = pair.Key;
                        if (pair.Key.Contains("_"))
                        {
                            destroyThis = pair.Key.Replace("_", " ");
                        }

                        //remove item from inventory and destroy object from world
                        destroyThis = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(destroyThis);

                        if (item.Ingredients[pair.Key] == 0) //if used up all the ingredients, delete them from the world
                        {
                            Destroy(GameObject.Find(destroyThis));
                            inv.items.Remove(item);
                        }

                        else //if there's still leftover from crafting, update item inventory data for amount
                        {
                            ItemData data = GameObject.Find(destroyThis).GetComponent<ItemData>();
                            data.amount = updateCount;
                            data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                        }

                        count++;
                    }
                    else
                    {
                        continue;
                    }
                }
               /* else
                {
                    notin++;
                    if (notin == finalList.Count)
                    {
                        Debug.Log("no recipe");
                        finalList.Clear();
                    }
                }*/
            }

            if (count == finalList.Count)
            {
                /*check amount in dragging stack, subract amount needed 
                make the thing, put item data on it and destroy the items*/

                craftItem = item;
                Debug.Log("can make a " + craftItem.Title);
                craftItemIng = item.Ingredients;
                //instantiate object
                GameObject done = Instantiate(itemp) as GameObject;
                done.GetComponent<ItemData>().item = inv.database.FetchItemByID(craftItem.ID);
                done.GetComponent<ItemData>().amount = 1;
                done.transform.SetParent(craftSlot.transform);
                done.transform.position = craftSlot.transform.position;
                done.GetComponent<Image>().sprite = inv.database.FetchItemByID(craftItem.ID).Sprite;
                done.name = inv.database.FetchItemByID(craftItem.ID).Title;
                break;
           }
        }
    }
}
