using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = RTS_Camera.Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, collisionMask))
            {
                RayClickable objctClickable = hit.collider.GetComponent<RayClickable>();
                if (objctClickable != null)
                    objctClickable.Click();
            }
        }
    }

}
