using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask recolectableMask;
    [SerializeField] private LayerMask doorMask;
    [SerializeField] private LayerMask pickUpMask;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private Inventory playerInventory;

    private Transform door;
    private float openDoorAngle = 90f;
    private float openDoorSpeed = 5f;
    private bool isOpen = false;
    private Quaternion closedDoorRotation;
    private Quaternion openDoorRotation;
    private float interactRange = 60f;

    void Start()
    {
        closedDoorRotation = Quaternion.Euler(0, 0, 0);
        openDoorRotation = Quaternion.Euler(0, openDoorAngle, 0);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Interact();
        }

        if (door)
        {
            if (isOpen)
            {
                door.rotation = Quaternion.Slerp(door.rotation, openDoorRotation, Time.deltaTime * openDoorSpeed);
            }
            else
            {
                door.rotation = Quaternion.Slerp(door.rotation, closedDoorRotation, Time.deltaTime * openDoorSpeed);
            }
        }
    }

    private void Interact()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, recolectableMask))
        {
            CollectItem(hit);
        }
        else if (Physics.Raycast(ray, out hit, interactRange, doorMask))
        {
            isOpen = !isOpen;
            HandleDoor(hit);
        }
    }

    private void CollectItem(RaycastHit hit)
    {
        Item item = hit.collider.GetComponent<ItemComponent>()?.item;
        if (item != null)
        {
            playerInventory.AddItem(item);
            Destroy(hit.collider.gameObject);
        }
    }

    private void HandleDoor(RaycastHit hit)
    {
        InteractableObject obj = hit.collider.GetComponent<InteractableObject>();

        if (obj != null && obj.HasItem())
        {
            door = hit.collider.transform;
        }
    }
}
