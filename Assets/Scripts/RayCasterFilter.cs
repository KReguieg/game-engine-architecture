using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (RectTransform), typeof (Collider2D))]
public class RayCasterFilter : MonoBehaviour, ICanvasRaycastFilter {

	public GameObject Canvas;

	Collider2D myCollider;
	RectTransform rectTransform;

	void Awake ()
	{
		myCollider = GetComponent<Collider2D>();
		rectTransform = GetComponent<RectTransform>();
	}

	#region ICanvasRaycastFilter implementation
	public bool IsRaycastLocationValid (Vector2 screenPos, Camera eventCamera)
	{
		Vector3 worldPoint = Vector3.zero;
		bool isInside = RectTransformUtility.ScreenPointToWorldPointInRectangle(
			rectTransform,
			screenPos,
			eventCamera,
			out worldPoint
		);

		Debug.Log (isInside);
		if (isInside)
			Canvas.GetComponent<RaycastBlocker> ().RaycastBlockByUI = true;
		return isInside;
	}
	#endregion
}

