﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntergratedUiManager : MonoBehaviour {
	public GameObject TowerInteractionMenuPrefab;

	public GameObject Canvas; // to set the Raycast blocker
	GameObject currentTowerInteractionMenu;
	GameObject currentSelectedTower;
	public float MenuHeight = 1;

	public GameObject[] towerUpgrades;

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
		
		int towerLevel = currentSelectedTower.GetComponent<Tower> ().upgradeLevel;
		if (towerLevel >= 4)
			return;
		GameObject towerUpgraded = Instantiate( towerUpgrades[towerLevel]);
		towerUpgraded.transform.SetPositionAndRotation(currentSelectedTower.transform.position, currentSelectedTower.transform.rotation);
		towerUpgraded.transform.SetParent (currentSelectedTower.transform.parent);
		towerUpgraded.GetComponent<Tower> ().integratedUiManager = gameObject;
		Destroy (currentSelectedTower);

	}

	public void SellSelecetdTower(){
		currentSelectedTower.GetComponent<Tower> ().Sell ();
	}
}
