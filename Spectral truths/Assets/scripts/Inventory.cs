using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    private int currentItemIndex = 0;

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public Item GetCurrentItem()
    {
        if (items.Count > 0)
        {
            return items[currentItemIndex];
        }
        return null;
    }

    public void NextItem()
    {
        if (items.Count > 0)
        {
            currentItemIndex = (currentItemIndex + 1 + items.Count) % items.Count;
        }
    }

    public void PreviousItem()
    {
        if(items.Count > 0)
        {
            currentItemIndex = (currentItemIndex - 1 + items.Count) % items.Count;
        }
    }
}
