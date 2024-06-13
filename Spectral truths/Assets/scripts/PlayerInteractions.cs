using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private float interactRange = 60f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask recolectableMask;
    [SerializeField] private LayerMask pickUpMask;
    [SerializeField] private Transform objectGrabPointTransform;

    private GrabbableObject grabbableObject;

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
        else if (Physics.Raycast(ray, out hit, interactRange, pickUpMask))
        {
            if(grabbableObject == null)
            {
                if (hit.transform.TryGetComponent(out grabbableObject))
                {
                    grabbableObject.Grab(objectGrabPointTransform);
                }
            } else
            {
                grabbableObject.Drop();
                grabbableObject = null;
            }

        }
    }
}
