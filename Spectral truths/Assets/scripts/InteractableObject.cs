using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private Item requiredItem;
    [SerializeField] private Inventory inventory;

    void Awake()
    {
        inventory = inventory.GetComponent<Inventory>();
    }

    public bool HasItem()
    {
        return inventory.items.Contains(requiredItem);
    }
}
