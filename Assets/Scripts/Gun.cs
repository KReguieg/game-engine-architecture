using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class Gun : VRTK_InteractableObject
{
    [SerializeField]
    private GameObject muzzlePoint;
    private LineRenderer lineRenderer;

    [SerializeField]
    private float damagePerShot = 8.0f; // <-- Haha Random Number by Moni^^

    float timer = 0;
    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        FireLaser();
    }

    private void FireLaser()
    {
        Ray r = new Ray(muzzlePoint.transform.position, muzzlePoint.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(r, out hit))
        {
            if(hit.collider.CompareTag("Enemy"))
            {
                hit.transform.gameObject.GetComponent<EnemyBehavior>().TakeDamage(damagePerShot);
            }
            
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, muzzlePoint.transform.position);
            lineRenderer.SetPosition(1, hit.point);
            StartCoroutine(DisableShot());
        }
    }


    protected void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    IEnumerator DisableShot()
    {
        
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
    }
}
