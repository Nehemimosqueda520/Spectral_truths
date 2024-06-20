using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask recolectableMask;
    [SerializeField] private LayerMask pickUpMask;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private Inventory playerInventory;

    private float interactRange = 60f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CollectItem();
        } 
    }
    private void CollectItem()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, recolectableMask))
        {
            Item item = hit.collider.GetComponent<ItemComponent>()?.item;
            if (item != null)
            {
                playerInventory.AddItem(item);
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
