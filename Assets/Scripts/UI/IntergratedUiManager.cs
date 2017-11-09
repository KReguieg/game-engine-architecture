using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntergratedUiManager : MonoBehaviour {
	public GameObject TowerInteractionMenuPrefab;
	GameObject currentTowerInteractionMenu;
	GameObject currentSelectedTower;
	public float MenuHeight = 1;

	public void SetTowerMenu(GameObject tower){
		currentSelectedTower = tower;
		if (currentTowerInteractionMenu == null){
			currentTowerInteractionMenu = Instantiate (TowerInteractionMenuPrefab, transform);
			
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
			towerUpgraded.GetComponent<Tower> ().ManagerObjects = transform.parent.gameObject;
			Destroy (currentSelectedTower);
		}
	}

	public void SellSelecetdTower(){
		DataCollector.GetInstance.ModifieMetal (currentSelectedTower.GetComponent<Tower> ().metalCost);
		currentSelectedTower.GetComponent<Tower> ().Sell ();
	}
}
