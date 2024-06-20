using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform item3DContainer;
    [SerializeField] private Camera itemCamera;
    [SerializeField] private GameManager gameManager;

    private GameObject current3DItem;
    private bool isInventoryOpen = false;
    private CursorLockMode previousLockMode;

    void Start()
    {
        inventoryPanel.SetActive(false);
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab)) ToggleInventory();

        SwitchItem();
    }

    private void ShowCurrentItem()
    {
        ClearCurrentItem();

        Item currentItem = inventory.GetCurrentItem();
        if (currentItem != null)
        {
            current3DItem = Instantiate(currentItem.itemPrefab, item3DContainer);
            current3DItem.transform.localPosition = Vector3.zero;
            current3DItem.transform.localRotation = Quaternion.identity;
            Item3DInteraction interactionScript = current3DItem.AddComponent<Item3DInteraction>();
            interactionScript.SetCamera(itemCamera);

            itemCamera.gameObject.SetActive(true);
            itemCamera.transform.localPosition = new Vector3(0, 0, -440);
            itemCamera.transform.LookAt(current3DItem.transform);
        }
    }

    private void ClearCurrentItem()
    {
        foreach (Transform child in item3DContainer)
        {
            Destroy(child.gameObject);
        }
        itemCamera.gameObject.SetActive(false);
    }

    private void SwitchItem()
    {
        if (isInventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                inventory.NextItem();
                ShowCurrentItem();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                inventory.PreviousItem();
                ShowCurrentItem();
            }
        }
    }

    private void ToggleInventory()
    {
        gameManager.TogglePausedGame();
        isInventoryOpen = !isInventoryOpen;

        if(isInventoryOpen)
        {
            inventoryPanel.SetActive(true);
            ShowCurrentItem();  
            Cursor.visible = true;
            previousLockMode = Cursor.lockState;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            inventoryPanel.SetActive(false);
            ClearCurrentItem();
            Cursor.lockState = previousLockMode;
        }
    }
}
