using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Gun : VRTK_InteractableObject
{
    [SerializeField]
    private GameObject muzzlePoint;
    //The Layer to hit with a shot, should be rangecheck
    public LayerMask mask;
    private LineRenderer lineRenderer;
	private VRTK_ControllerReference controllerReference;
	public float noHitHapticStrength = 0.1f;
	public float hapticStrength = 0.25f;
	public float enemyHitHapticStrength = 1f;

    [SerializeField]
    private float damagePerSecond = 8.0f; // <-- Haha Random Number by Moni^^

	[SerializeField]
	private ParticleSystem laserhitAnimation;
    
	[SerializeField]
	private ParticleSystem laserBeamAnimation;

    protected void Update()
	{
		if (IsUsing ()) {	
			FireLaser ();
		} else {
			lineRenderer.enabled = false;
			laserBeamAnimation.GetComponent<Renderer>().enabled = false;
		}
	}

    private void FireLaser()
    {
		laserBeamAnimation.GetComponent<Renderer>().enabled = true;
        Ray r = new Ray(muzzlePoint.transform.position, muzzlePoint.transform.forward);
        RaycastHit hit;

		if (Physics.Raycast (r, out hit,50.0f, mask )) {
			SpawnLaserHit(hit.transform);
			if (hit.collider.CompareTag ("Enemy")) { // Hit rangechecker of Enemy
				hit.transform.parent.GetComponent<EnemyBehavior> ().TakeDamage (damagePerSecond * Time.deltaTime);
				VRTK_ControllerHaptics.TriggerHapticPulse (controllerReference, enemyHitHapticStrength, 0.1f, 0.01f);
			} else {
				VRTK_ControllerHaptics.TriggerHapticPulse (controllerReference, hapticStrength, 0.1f, 0.01f);
			}
            
			lineRenderer.enabled = true;
			lineRenderer.SetPosition (0, muzzlePoint.transform.position);
			lineRenderer.SetPosition (1, hit.point);
		} else {
			lineRenderer.enabled = true;
			lineRenderer.SetPosition (0, muzzlePoint.transform.position);
			lineRenderer.SetPosition (1, r.GetPoint(50));
			VRTK_ControllerHaptics.TriggerHapticPulse (controllerReference, noHitHapticStrength, 0.1f, 0.01f);
		}
    }

    private void SpawnLaserHit(Transform hit)
    {
		Debug.Log(hit.position);
		Destroy(Instantiate(laserhitAnimation, hit.position, hit.rotation).transform.gameObject, 0.5f);
    }

    public override void Grabbed(VRTK_InteractGrab grabbingObject)
	{
		base.Grabbed(grabbingObject);
		controllerReference = VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);
	}

    public override  void Ungrabbed(VRTK_InteractGrab previousGrabbingObject = null){
        transform.localPosition = Vector3.zero;
        transform.localRotation = new Quaternion(0,0,0,0);
		controllerReference = null;

        base.Ungrabbed(previousGrabbingObject);
    }

    protected void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
}
