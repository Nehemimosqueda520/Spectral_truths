using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InspectorUI : MonoBehaviour
{
    [SerializeField] private GameObject inspectorPanel;
    [SerializeField] private Transform item3DContainer;
    [SerializeField] private Camera itemCamera;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Inventory playerInventory;

    private GameObject _current3DItem;
    private bool _isInspectorOpen;

    public event Action OnItemSaved;
    public event Action OnInspectorOpened;
    public event Action OnInspectorClosed;

    void Start()
    {
        playerInventory = playerInventory.GetComponent<Inventory>();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) CloseInspector();
        if (Input.GetKeyDown(KeyCode.Mouse0) && isInspectorOpen)
        {
            Debug.Log("Se guardo el itemsardo");
            SaveItem();
            CloseInspector();
        }
    }

    private void ShowCurrentItem()
    {
        ClearCurrentItem();

        Item item = current3DItem.GetComponent<ItemComponent>().item;
        if(current3DItem != null)
        {
            GameObject itemToShow = Instantiate(item.itemPrefab, item3DContainer);
            itemToShow.transform.localPosition = Vector3.zero;
            itemToShow.transform.localRotation = Quaternion.identity;
            Item3DInteraction interactionScript = itemToShow.AddComponent<Item3DInteraction>();
            interactionScript.SetCamera(itemCamera);

            itemCamera.gameObject.SetActive(true);
            itemCamera.transform.localPosition = new Vector3(0, 0, -440);
            itemCamera.transform.LookAt(itemToShow.transform);
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

    private void SaveItem()
    {
        OnItemSaved?.Invoke();
        Item item = current3DItem.GetComponent<ItemComponent>().item;
        UIManager.Instance.ShowInventoryMessage("1 item added to inventory - Press tab to open it");
        playerInventory.AddItem(item);
        CloseInspector();
    }

    public void OpenInspector()
    {
        isInspectorOpen = true;
        GameManager.isGamePaused = true;
        gameObject.SetActive(true);
        ShowCurrentItem();
        OnInspectorOpened?.Invoke();
    }

    public void CloseInspector()
    {
        isInspectorOpen = false;
        GameManager.isGamePaused = false;
        gameObject.SetActive(false);
        ClearCurrentItem();
        OnInspectorClosed?.Invoke();
    }


    //Properties
    public GameObject current3DItem
    {
        get { return _current3DItem; }
        set { _current3DItem = value; }
    }
    public bool isInspectorOpen
    {
        get { return _isInspectorOpen; }
        private set { _isInspectorOpen = value; }
    }
}
