using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMenu : MonoBehaviour {

	bool locked = true;
	float destroyTimer = 0;
	bool destroy;
	IntergratedUiManager iUIManager;
	RangeRotator rangeRotator;
	[SerializeField]
	private GameObject UpgradeText, SellText;

	public float DestroyAfterTime = 1;
	GameObject currentTower;
	// Use this for initialization
	void Start () {
		iUIManager = transform.parent.GetComponent<IntergratedUiManager> ();
		rangeRotator = transform.parent.parent.GetComponentInChildren<RangeRotator>();
		GetComponentInChildren<Canvas>().worldCamera = RTS_Camera.Camera;
	
	}

	// Update is called once per frame
	void Update () {
		if (!locked && Input.GetMouseButton (0)) {
			destroy = true;
		}
		if (locked && Input.GetMouseButtonUp (0))
			locked = false;

		if (destroy) {
			destroyTimer += Time.deltaTime;
			if (destroyTimer >= DestroyAfterTime) {
				Destroy (gameObject);
				rangeRotator.Disable ();
			}
		}
	}

	public void SetNewContent(GameObject currentTower ){
		this.currentTower = currentTower;
		SellText.GetComponent<UnityEngine.UI.Text>().text = "Sell(" +  currentTower.GetComponent<Tower> ().metalCost + ")";
		if (currentTower.GetComponent<Tower> ().towerUpgrade == null) {
			UpgradeText.GetComponent<UnityEngine.UI.Text> ().text = "Maxed";
			UpgradeText.transform.parent.GetComponent<UnityEngine.UI.Button> ().enabled = false;
		}
		else
			UpgradeText.GetComponent<UnityEngine.UI.Text>().text = "Upgrade(" +  currentTower.GetComponent<Tower> ().towerUpgrade.GetComponent<Tower>().metalCost + ")";
	}

	public void DontDestroy(){
		destroy = false;
		destroyTimer = 0;
		locked = true;
	}

	public void OnUpgrade(){
		if (!locked)
			iUIManager.UpgradeSelecetdTower();
	}

	public void OnSell(){
		Destroy (gameObject);
		iUIManager.SellSelecetdTower ();
		rangeRotator.Disable ();
	}

	public void MouseEnterUpgrade(){
		rangeRotator.SetUpgrade (currentTower.GetComponent<Tower> ().towerUpgrade.GetComponent<Tower>());
	}

	public void MouseLeaveUpgrade(){
		rangeRotator.UnsetUpgrade ();
	}



}
