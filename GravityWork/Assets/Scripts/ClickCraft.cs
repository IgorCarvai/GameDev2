using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LitJson;
using System.Globalization;
using UnityEngine;

public class ClickCraft : MonoBehaviour {

    int count;

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
        Item craftItem = craftS.craftItem;

        /*first, check craftItemIngredients against ingredient slot items
        compare amounts needed vs currently have
        then subtract/update amounts
        if amount = 0, delete from world
        create item to inventory
        */

        foreach (KeyValuePair<string, int> finalP in craftItem.Ingredients)
        {
            foreach (KeyValuePair<string, int> pair in craftItemIng)
            {
                if (finalP.Key == pair.Key)
                {
                    int updateCount = finalP.Value;
                    updateCount = pair.Value - finalP.Value;

                    string destroyThis = pair.Key;

                    //parsing
                    if (pair.Key.Contains("_"))
                    {
                        destroyThis = pair.Key.Replace("_", " ");
                    }
                    destroyThis = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(destroyThis);


                    if (updateCount == 0)        //if used up all the ingredients, delete them from the world
                    {
                        Destroy(GameObject.Find(destroyThis));
                    }

                    else
                    {
                        ItemData data = GameObject.Find(destroyThis).GetComponent<ItemData>();
                        data.amount = updateCount;
                        data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    }
                }
            }
        }

        if (craftItem != null)
        {
            //instantiate object
            inv.AddItem(craftItem.ID);
        }

        ClearAll();
    }

    public void ClearAll()
    {
        if (craftS.count > 0)
        {
            
        }
        craftS.craftItemIng.Clear();

        if (craftSlot.transform.childCount > 0)
        {
            Destroy(craftSlot.transform.GetChild(0).gameObject);
        }
    }

}
