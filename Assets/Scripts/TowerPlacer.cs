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

	private Material standartMaterial;
	public Material notPlaceble;
	bool blocked;

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
			if(!blocked)
				ReleaseIfClicked();
        }
    }

    public void HandleNewObjectHotkey()
    {
        if (currentPlaceableObject != null)
        {
            Destroy(currentPlaceableObject);
			transform.parent.GetComponentInChildren<RangeRotator> ().Disable ();
            UnMaskCamera ();
        }
        else
        {
            Camera.main.cullingMask = Camera.main.cullingMask | buildLayer;
            currentPlaceableObject = Instantiate(placeableObjectPrefab, towerCollector.transform);
			standartMaterial = currentPlaceableObject.GetComponentInChildren<Renderer> ().sharedMaterial;
			transform.parent.GetComponentInChildren<RangeRotator> ().SetToTower (currentPlaceableObject);
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
		BoxCollider col = currentPlaceableObject.GetComponent<Tower> ().Buildblocker.GetComponent<BoxCollider> ();

		bool hitbuildPLane = Physics.Raycast (ray, out hitInfo, 100, buildLayer);
		Vector3 point = SnapToGrid (hitInfo.point);
		bool hitOtherTower = Physics.CheckBox (point, col.size / 2, Quaternion.identity, buildBlockLayer);
		if (hitbuildPLane && !hitOtherTower) {
			currentPlaceableObject.transform.position = point;
			currentPlaceableObject.transform.rotation = Quaternion.FromToRotation (Vector3.up, hitInfo.normal);

			SetMaterialStandart(currentPlaceableObject.GetComponentsInChildren<Renderer> ());
			blocked = false;
		} else {
			blocked = true;
			Plane plane = new Plane (Vector3.up, Vector3.zero);
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			float dist;
			plane.Raycast (r, out dist);
			currentPlaceableObject.transform.position = r.GetPoint(dist);
			SetMaterialNotPlaceble (currentPlaceableObject.GetComponentsInChildren<Renderer> ());
		}
    }

	void SetMaterialNotPlaceble(Renderer[] renderer)
	{
		foreach (Renderer r in renderer)
			if(r.gameObject.tag != "Fixed")
				r.sharedMaterial = notPlaceble;
	}
	void SetMaterialStandart(Renderer[] renderer)
	{
		foreach (Renderer r in renderer)
			if(r.gameObject.tag != "Fixed")
				r.sharedMaterial = standartMaterial;
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

				currentPlaceableObject.GetComponent<Tower> ().ManagerObjects = transform.parent.gameObject;
				currentPlaceableObject = null;
				UnMaskCamera ();
                buildButtonMenu.GetComponent<BuildButtonMenu>().ExpandMenu();

				transform.parent.GetComponentInChildren<RangeRotator> ().Disable ();
			} 
        }
    }

	public void SwitchTower(int i){
		placeableObjectPrefab = TowerPrefabs [i];
		if (currentPlaceableObject != null) {
			Destroy(currentPlaceableObject);
			currentPlaceableObject = Instantiate (placeableObjectPrefab, towerCollector.transform);
			transform.parent.GetComponentInChildren<RangeRotator> ().SetToTower (currentPlaceableObject);
			standartMaterial = currentPlaceableObject.GetComponentInChildren<Renderer> ().sharedMaterial;

		}
	}
}