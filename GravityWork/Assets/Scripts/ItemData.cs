using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {

    public Item item;
    public static GameObject draggedItem;
    public int amount;
    public int slot;
    public Vector3 originalPos;
    public static Transform startParent;
    public bool isDraggable = false;

    public static Inventory inv;
    public static CraftingSystem craft;
    private Tooltip tooltip;

    void Start()
    {

        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        craft = GameObject.Find("CraftSystem").GetComponent<CraftingSystem>();

        tooltip = inv.GetComponent<Tooltip>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDraggable == true)
        {
            if (item != null)
            {
                originalPos = this.transform.position;
                startParent = this.transform.parent.parent;
                draggedItem = gameObject;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
                draggedItem.GetComponent<LayoutElement>().ignoreLayout = true;
                draggedItem.transform.SetParent(draggedItem.transform.parent.parent);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable == true)
        {
            if (item != null)
            {
                //x, y of mouse
                this.transform.position = eventData.position;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggable == true)
        {
            //if dropped in inventory / original parent, item start parent
            if (this.transform.parent == startParent)
            {
                this.transform.SetParent(inv.slots[slot].transform);
                this.transform.position = inv.slots[slot].transform.position;
            }
            else //clean the slot
            {
                inv.items[slot] = new Item();
            }

            GetComponent<CanvasGroup>().blocksRaycasts = true;
            draggedItem.GetComponent<LayoutElement>().ignoreLayout = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }

}
