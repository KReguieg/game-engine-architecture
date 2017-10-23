using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickSetCameraPosition : MonoBehaviour {

	public void OnMouseDown(){

		Vector2 point = GetComponent<RayCasterFilter> ().point;
		RectTransform rectTransform = GetComponent<RectTransform> ();
		point = new Vector2 (-point.x/ rectTransform.sizeDelta.x, -point.y/ rectTransform.sizeDelta.y ); // nomalize
		point -= Vector2.one * 0.5f;
		point *= -2;
		Debug.Log (point);
		Camera.main.GetComponent<RTS_Cam.RTS_Camera>().SetTarget(point);
	}


}
