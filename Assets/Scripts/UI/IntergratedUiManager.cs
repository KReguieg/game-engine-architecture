using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntergratedUiManager : MonoBehaviour {
	public GameObject TowerInteractionMenuPrefab;

	public GameObject Canvas; // to set the Raycast blocker
	GameObject currentTowerInteractionMenu;
	GameObject currentSelectedTower;
	public float MenuHeight = 1;

	public void SetTowerMenu(GameObject tower){
		currentSelectedTower = tower;
		if (currentTowerInteractionMenu == null){
			currentTowerInteractionMenu = Instantiate (TowerInteractionMenuPrefab, transform);
			foreach (RayCasterFilter filter in currentTowerInteractionMenu.GetComponentsInChildren<RayCasterFilter>())
				filter.Canvas = Canvas;
		}
		currentTowerInteractionMenu.transform.position = tower.transform.position + Vector3.up * MenuHeight;
		currentTowerInteractionMenu.GetComponent<TowerMenu> ().DontDestroy ();
	}


	public void UpgradeSelecetdTower(){
		currentSelectedTower.GetComponent<Tower>().Upgrade ();
	}

	public void SellSelecetdTower(){
		currentSelectedTower.GetComponent<Tower> ().Sell ();
	}
}
