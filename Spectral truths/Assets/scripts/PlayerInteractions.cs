using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask recolectableMask;
    [SerializeField] private LayerMask doorMask;
    [SerializeField] private LayerMask pickUpMask;
    [SerializeField] private LayerMask LampMask;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private InspectorUI inspector;

    private Transform door;
    private float openDoorAngle = 90f;
    private float openDoorSpeed = 5f;
    private bool isOpen = false;
    private Quaternion closedDoorRotation;
    private Quaternion openDoorRotation;
    private Quaternion lampAngleRotation;
    private float interactRange = 60f;
    private RaycastHit currentHit;
    private GameObject currentItemObject;

    void Start()
    {
        inspector = inspector.GetComponent<InspectorUI>();
        inspector.OnItemSaved += HandleItemSaved;
        inspector.OnInspectorOpened += HandleInspectorOpened;
        inspector.OnInspectorClosed += HandleInspectorClosed;

        closedDoorRotation = Quaternion.Euler(0, 0, 0);
        openDoorRotation = Quaternion.Euler(0, openDoorAngle, 0);
        lampAngleRotation = Quaternion.Euler(-12f, -90f, 0);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Interact();
        }

        PointToLamp();

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
            currentHit = hit;
            GrabItem(hit);
        }
        else if (Physics.Raycast(ray, out hit, interactRange, doorMask))
        {
            isOpen = !isOpen;
            HandleDoor(hit);
        }
    }

    private void PointToLamp()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 120f, LampMask))
        {
            Transform lamp = hit.collider.gameObject.transform;
            lamp.rotation = Quaternion.Lerp(lamp.rotation, lampAngleRotation, Time.deltaTime * 2f);

        }
    }

    private void GrabItem(RaycastHit hit)
    {
        Item item = hit.collider.GetComponent<ItemComponent>()?.item;
        if(item != null)
        {
            currentItemObject = hit.collider.gameObject;
            inspector.current3DItem = currentItemObject;
            inspector.OpenInspector();
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

    private void HandleItemSaved()
    {
        Destroy(currentHit.collider.gameObject);
        currentItemObject = null;
    }

    private void HandleInspectorOpened()
    {
        if (currentItemObject != null)
        {
            currentItemObject.SetActive(false);
        }
    }

    private void HandleInspectorClosed()
    {
        if (currentItemObject != null)
        {
            currentItemObject.SetActive(true);
        }   
    }

    private void HandleDoor(RaycastHit hit)
    {
        InteractableObject obj = hit.collider.GetComponent<InteractableObject>();

        if (obj != null && obj.HasItem())
        {
            door = hit.collider.transform;
        } 
        else
        {
            UIManager.Instance.ShowInventoryMessage("you need a key to open this door");
        }
    }
}
