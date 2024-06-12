using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    public void PickUp()
    {
        Debug.Log("Picking up item: " + item.name);
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            inventory.AddItem(item);
            Destroy(gameObject);
        }
    }
}
