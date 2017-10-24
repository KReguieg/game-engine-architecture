using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntergratedUiManager : MonoBehaviour {
	public GameObject TowerInteractionMenuPrefab;

	public GameObject Canvas; // to set the Raycast blocker
	GameObject currentTowerInteraction;

	public void SetTowerMenu(Vector3 position){
		if (currentTowerInteraction == null){
			currentTowerInteraction = Instantiate (TowerInteractionMenuPrefab, transform);
			foreach (RayCasterFilter filter in currentTowerInteraction.GetComponentsInChildren<RayCasterFilter>())
				filter.Canvas = Canvas;
		}
		currentTowerInteraction.transform.position = position;
		currentTowerInteraction.GetComponent<TowerMenu> ().DontDestroy ();
	}

}
