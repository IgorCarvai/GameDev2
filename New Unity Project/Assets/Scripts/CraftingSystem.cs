﻿ using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftingSystem : MonoBehaviour {

   //public static List<Item> ingredients = new List<Item>();
    public static Dictionary<string, int> toMake = new Dictionary<string, int>();
    private Inventory inv;

    void Start()
    {
    }

}
