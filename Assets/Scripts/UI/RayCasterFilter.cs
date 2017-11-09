using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (RectTransform))]
public class RayCasterFilter : MonoBehaviour, ICanvasRaycastFilter {
	RectTransform rectTransform;

	public Vector2 point;
	void Awake ()
	{
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
		if (isInside){
			RaycastBlocker.GetInstance().RaycastBlockByUI = true;
		}
		return isInside;
	}
	#endregion
}

