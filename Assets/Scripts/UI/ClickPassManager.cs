using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickPassManager : MonoBehaviour
{
    public LayerMask collisionMask;
    public float timeIntervall = 0.1f;
    float timer = 0;
    // Use this for initialization
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = RTS_Camera.Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, collisionMask))
                hit.collider.GetComponent<RayClickable>()?.Click();
        }
    }
}
