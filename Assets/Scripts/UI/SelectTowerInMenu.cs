using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Describe your class quickly here.
/// </summary>
/// <remarks>
/// Author: Khaled Reguieg E-Mail: Khaled.Reguieg@artcom.de
/// </remarks>
public class SelectTowerInMenu : MonoBehaviour
{
	[SerializeField]
	private Button mainTowerMenuButton;

	[SerializeField]
	private GameObject buildManager;

	[SerializeField]
    private int towerIndex;

    // Use this for initialization
    private void Start ()
	{
		GetComponent<Button>().onClick.AddListener( () => {SelectTower();} );
	}

    private void SelectTower()
    {
        mainTowerMenuButton.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
	 	buildManager.GetComponent<TowerPlacer>().SwitchTower(towerIndex);
    }
}
