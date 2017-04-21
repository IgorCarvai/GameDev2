using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    private Item item;
    private string data;
    private GameObject tooltip;


    void Start()
    {
        tooltip = GameObject.Find("Description");
        tooltip.SetActive(false);

    }

    void Update()
    {
        if (tooltip.activeSelf)
        {
            Vector3 pos = Input.mousePosition;

            float distX = 150;
            float distY = 100;

            if (Input.mousePosition.x + distX > Screen.width)
            {
                pos = new Vector3(Input.mousePosition.x - distX, Input.mousePosition.y, Input.mousePosition.z);
            }

            if (Input.mousePosition.y - distY < 0)
            {
                pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 75, Input.mousePosition.z);
            }

            tooltip.transform.position = pos;

        }
    }

    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        data = "<color=#000000><b>" + item.Title + "</b></color>\n\n" + item.Description;
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
