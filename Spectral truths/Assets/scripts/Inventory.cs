using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public GameObject inventoryUI;
    public GameObject itemButtonPrefab;

    public Transform content;

    private bool isInventoryOpen = false;
    private CursorLockMode previousLockMode;

    void Start()
    {
        inventoryUI.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
            if (inventoryUI.activeSelf)
            {
                UpdateInventoryUI();
            }
        }
    }

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in items)
        {
            GameObject button = Instantiate(itemButtonPrefab, content);
            button.GetComponentInChildren<Text>().text = item.name;

        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if(isInventoryOpen)
        {
            inventoryUI.SetActive(true);
            Cursor.visible = true;
            previousLockMode = Cursor.lockState;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            inventoryUI.SetActive(false);
            Cursor.lockState = previousLockMode;
        }

    }

}
