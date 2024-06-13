using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickupLayerMask;

    private GrabbableObject grabbableObject;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(grabbableObject == null)
            {
                float pickupDistance = 60f;
                if(Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickupLayerMask))
                {
                    if(raycastHit.transform.TryGetComponent(out grabbableObject))
                    {
                        grabbableObject.Grab(objectGrabPointTransform);
                    }
                }
                else
                {
                    grabbableObject.Drop();
                }
            }
        }        
    }
}
