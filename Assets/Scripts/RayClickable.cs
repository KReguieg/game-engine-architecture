using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RayClickable : MonoBehaviour {
	public UnityEvent OnClick;
	public void Click(){
		OnClick.Invoke();
	}
}
