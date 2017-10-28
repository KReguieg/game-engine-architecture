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
		currentTowerInteractionMenu.GetComponent<TowerMenu> ().SetNewContent(currentSelectedTower);
		currentTowerInteractionMenu.GetComponent<TowerMenu> ().DontDestroy ();
	}


	public void UpgradeSelecetdTower(){
		GameObject nextLevelTower = currentSelectedTower.GetComponent<Tower> ().towerUpgrade;
		if (DataCollector.GetInstance.ModifieMetal (-nextLevelTower.GetComponent<Tower> ().metalCost)) {
			GameObject towerUpgraded = Instantiate( nextLevelTower);
			towerUpgraded.transform.SetPositionAndRotation(currentSelectedTower.transform.position, currentSelectedTower.transform.rotation);
			towerUpgraded.transform.SetParent (currentSelectedTower.transform.parent);
			towerUpgraded.GetComponent<Tower> ().integratedUiManager = gameObject;
			Destroy (currentSelectedTower);
		}
	}

	public void SellSelecetdTower(){
		DataCollector.GetInstance.ModifieMetal (currentSelectedTower.GetComponent<Tower> ().metalCost);
		currentSelectedTower.GetComponent<Tower> ().Sell ();
	}
}
