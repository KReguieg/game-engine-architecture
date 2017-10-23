using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (RectTransform), typeof (Collider2D))]
public class RayCasterFilter : MonoBehaviour, ICanvasRaycastFilter {

	public GameObject Canvas;

	Collider2D myCollider;
	RectTransform rectTransform;

	public Vector2 point;
	void Awake ()
	{
		myCollider = GetComponent<Collider2D>();
		rectTransform = GetComponent<RectTransform>();
	}

	#region ICanvasRaycastFilter implementation
	public bool IsRaycastLocationValid (Vector2 screenPos, Camera eventCamera)
	{
		Vector2 worldPoint = Vector2.zero;
		bool isInside = RectTransformUtility.ScreenPointToLocalPointInRectangle(
			rectTransform,
			screenPos,
			eventCamera,
			out worldPoint
		);
		point = worldPoint;
		if (isInside)
			Canvas.GetComponent<RaycastBlocker> ().RaycastBlockByUI = true;
		return isInside;
	}
	#endregion
}

