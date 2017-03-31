using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LitJson;
using System.Globalization;
using UnityEngine;

public class ClickCraft : MonoBehaviour {

    int count;

    public bool isClicked = false;
    private Inventory inv;
    private CraftingSystem craftS;

    ItemDatabase database;
    GameObject craftSlot;

    private Dictionary<string, int> craftItemIng = new Dictionary<string, int>(); //to check inventory items against, final item ingredients

    void Start()
    {
        database = GetComponent<ItemDatabase>();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        craftS = GameObject.Find("Inventory").GetComponent<CraftingSystem>();
        craftSlot = GameObject.Find("FinishedItem");

        craftItemIng = GameObject.Find("Inventory").GetComponent<CraftingSystem>().craftItemIng;
    }

    public void onClick()
    {
        Debug.Log("button pressed");
        isClicked = true;

        Item craftItem = craftS.craftItem;

        /*first, check craftItemIngredients against ingredient slot items
            compare amounts needed vs currently have
            then subtract/update amounts
            if amount = 0, delete from world
            create item to inventory
        */

        /*
        
        int updateCount = item.Ingredients[pair.Key];
        string destroyThis = pair.Key;

        Debug.Log(item.Title + " recipe requires: " + item.Ingredients[pair.Key] + " currently have: " + pair.Value);
        updateCount = pair.Value - item.Ingredients[pair.Key];


        //parsing
        if (pair.Key.Contains("_"))
        {
            destroyThis = pair.Key.Replace("_", " ");
        }
        destroyThis = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(destroyThis);



        //if there's still leftover from crafting, update item inventory data for amount
        ItemData data = GameObject.Find(destroyThis).GetComponent<ItemData>();
        data.amount = updateCount;
        data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();

        if (updateCount == 0)        //if used up all the ingredients, delete them from the world
        {
            Destroy(GameObject.Find(destroyThis));
        }

        */

        if (craftItem != null)
        {
            //instantiate object
            inv.AddItem(craftItem.ID);

        }

        //clear out lists and stuff
        craftS.count = 0;

        //if can still make, leave finished item icon, else delete
        Destroy(craftSlot.transform.GetChild(0).gameObject);
        //craftS.craftItemIng.Clear();
    }

    public void ClearAll()
    {

    }
}
