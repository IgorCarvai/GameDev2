using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour {

    private List<Item> database = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();
        
        //Debug.Log(FetchItemByID(35).Ingredients["steel"]);
       
    }

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
            if (database[i].ID == id)
                return database[i];
        return null;
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            Dictionary<string, int> ing = new Dictionary<string, int>();
            if (itemData[i]["ingredients"].Count > 0)
            {
                //Debug.Log("not empty");
                List<string> keyList = new List<string>(itemData[i]["ingredients"].Keys);

                foreach (string j in keyList)
                {
                    ing.Add(j, (int)(itemData[i]["ingredients"][j]));
                }
            }

            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), 
                itemData[i]["description"].ToString(), ing, bool.Parse(itemData[i]["stackable"].ToString()), 
                itemData[i]["slug"].ToString()));

        }
    }
}

public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Dictionary<string,int> Ingredients { get; set; }
    public bool Stackable { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Item(int id, string title, string description, Dictionary<string,int> ingredients, bool stackable, string slug)
    {
        this.ID = id;
        this.Title = title;
        this.Description = description;
        this.Ingredients = ingredients;
        this.Stackable = stackable;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }

    public Item()
    {
        //if item was deleted/error
        this.ID = -1;
    }
}