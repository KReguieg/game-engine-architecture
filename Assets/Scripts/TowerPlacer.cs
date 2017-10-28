using UnityEngine;

/// <summary>
/// Describe your class quickly here.
/// </summary>
/// <remarks>
/// Author: Khaled Reguieg E-Mail: Khaled.Reguieg@artcom.de
/// </remarks>
public class TowerPlacer : MonoBehaviour
{
	[SerializeField]
	private GameObject[] TowerPrefabs;

	[SerializeField]
    private GameObject placeableObjectPrefab;


    [SerializeField]
    private KeyCode newObjectHotkey = KeyCode.B;

    private GameObject currentPlaceableObject;
	public GameObject towerCollector;

    private float mouseWheelRotation;


	[SerializeField]
	private LayerMask buildBlockLayer;

	[SerializeField]
	private LayerMask buildLayer;

    [SerializeField]
    private GameObject buildButtonMenu;

    [SerializeField]
    private GameObject canvas;

    private void Update()
    {
        if (Input.GetKeyDown(newObjectHotkey))
        {
            HandleNewObjectHotkey();
            buildButtonMenu.GetComponent<BuildButtonMenu>().ExpandMenu();
        }

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            //RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    public void HandleNewObjectHotkey()
    {
        if (currentPlaceableObject != null)
        {
            Destroy(currentPlaceableObject);
            UnMaskCamera ();
        }
        else
        {
            Camera.main.cullingMask = Camera.main.cullingMask | buildLayer;
            currentPlaceableObject = Instantiate(placeableObjectPrefab, towerCollector.transform);
        }
    }

	private void UnMaskCamera(){
		Camera.main.cullingMask = Camera.main.cullingMask ^ buildLayer; //xOR
	}

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//Vector3 oldPosition = Vector3.zero;
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100, buildLayer))
        {
			BoxCollider col = currentPlaceableObject.GetComponent<Tower> ().Buildblocker.GetComponent<BoxCollider> ();
			Vector3 point = SnapToGrid(hitInfo.point);
			if (!Physics.CheckBox (point, col.size / 2, Quaternion.identity, buildBlockLayer)) {
				currentPlaceableObject.transform.position = point;
				currentPlaceableObject.transform.rotation = Quaternion.FromToRotation (Vector3.up, hitInfo.normal);
			}
        }
    }

	Vector3 SnapToGrid(Vector3 point){
		int x =(int)( point.x * 100 );
		int z =(int)( point.z * 100 );

		return new Vector3 (x/ 100, point.y, z/ 100);
	}

    private void RotateFromMouseWheel()
    {
        //Debug.Log(Input.mouseScrollDelta);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0) && !canvas.GetComponent<RaycastBlocker>().RaycastBlockByUI)
        {
			if (DataCollector.GetInstance.ModifieMetal (-currentPlaceableObject.GetComponent<Tower> ().metalCost)) {
				currentPlaceableObject.GetComponent<Tower> ().EnableTower ();

				currentPlaceableObject.GetComponent<Tower> ().integratedUiManager = transform.parent.GetComponentInChildren<IntergratedUiManager>().gameObject;
				currentPlaceableObject = null;
				UnMaskCamera ();
                buildButtonMenu.GetComponent<BuildButtonMenu>().ExpandMenu();
			} 
        }
    }

	public void SwitchTower(int i){
		placeableObjectPrefab = TowerPrefabs [i];
		if (currentPlaceableObject != null) {
			Destroy(currentPlaceableObject);
			currentPlaceableObject = Instantiate (placeableObjectPrefab, towerCollector.transform);
		}
	}
}