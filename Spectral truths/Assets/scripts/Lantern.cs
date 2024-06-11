using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lantern : MonoBehaviour
{
    private float targetXRotation; 

    void Start()
    {
        targetXRotation = transform.rotation.x;
    }

    void Update()
    {
        targetXRotation = Mathf.Clamp(targetXRotation, -85f, 85f);
      
    }
}
