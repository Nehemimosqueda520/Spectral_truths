using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private float interactRange = 60f;
    [SerializeField] Camera playerCamera;
    [SerializeField] LayerMask recolectableMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Interact();
        } 
    }

    void Interact()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, recolectableMask))
        {
            ItemPickup itemPickup = hit.collider.GetComponent<ItemPickup>();
            if (itemPickup != null)
            {
                itemPickup.PickUp();
            }
        }
    }
}
