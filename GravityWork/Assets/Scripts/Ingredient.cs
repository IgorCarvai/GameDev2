using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Ingredient : MonoBehaviour, IDropHandler
{
    public Inventory inv;
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                //get item
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    void Start ()
    {
        inv = GetComponent<Inventory>();

    }
    #region IdropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

        if (!item)
        {
            ItemData.draggedItem.transform.SetParent(transform);
        }
        else //swap from inventory to crafting, just switch places
        {
            //swap info and places
            Transform item = this.transform.GetChild(0); //put craft item in inv
            item.GetComponent<ItemData>().slot = droppedItem.slot;
            item.transform.SetParent(ItemData.inv.slots[droppedItem.slot].transform);
            item.transform.position = ItemData.inv.slots[droppedItem.slot].transform.position;

            //droppedItem.slot = id; //put inv item in craft
            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;

            ItemData.inv.items[droppedItem.slot] = item.GetComponent<ItemData>().item;
            //ItemData.inv.items[id] = droppedItem.item;
        }

        //move item into ingredients - make a copy to ingredients
        if (!CraftingSystem.toMake.ContainsKey(droppedItem.item.Slug)) //if first time putting item in craft, create new entry
        {
            CraftingSystem.toMake.Add(droppedItem.item.Slug, droppedItem.amount);
            Debug.Log(droppedItem.item.Slug + " " + droppedItem.amount);
        }
    }
    #endregion
}
