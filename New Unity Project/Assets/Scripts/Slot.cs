using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IDropHandler {

    public int id;
    private Inventory inv;


    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (inv.items[id].ID == -1 && this.transform.childCount < 1)  //no item in this slot
        {
            //clean up old slot before making new ones
            inv.items[droppedItem.slot] = new Item();
            inv.items[id] = droppedItem.item;
            droppedItem.slot = id;
            ItemData.draggedItem.transform.SetParent(transform);
        }    

        else if (droppedItem.slot != id) //when there's an item, swap
        {
            Transform item = this.transform.GetChild(0);
            item.GetComponent<ItemData>().slot = droppedItem.slot;
            item.transform.SetParent(inv.slots[droppedItem.slot].transform);
            item.transform.position = inv.slots[droppedItem.slot].transform.position;

            droppedItem.slot = id;
            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;

            inv.items[droppedItem.slot] = item.GetComponent<ItemData>().item;
            inv.items[id] = droppedItem.item;
        }

        if (droppedItem.transform.parent != ItemData.startParent) //if return craft item to inventory, remove from craft and return to inv list
        {
            CraftingSystem.toMake.Remove(droppedItem.item.Slug);
            inv.items.Add(droppedItem.item);
        }
    }

}
