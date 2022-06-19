using UnityEngine;

/// <summary>
/// Describe your class quickly here.
/// </summary>
/// <remarks>
/// Author: Khaled Reguieg E-Mail: Khaled.Reguieg@gmail.com
/// </remarks>
public class MirrorPosition : MonoBehaviour
{
	[SerializeField]
	private GameObject objectToMirror;

	[SerializeField]
	private GameObject simulatorBackupObjectToMirror;

	[SerializeField]
	private GameObject simulatorCameraPosition;
	
	// Update is called once per frame
	private void Update ()
	{
		if(objectToMirror.activeSelf) 
		{
			Debug.Log("SteamVR POS= " + objectToMirror.transform.position);
			transform.position = objectToMirror.transform.position - new Vector3(0, 0, -0.25f);
			transform.rotation = objectToMirror.transform.rotation;
		}
		else
		{
			Debug.Log("SteamVR POS= " + simulatorBackupObjectToMirror.transform.position);			
			transform.position = simulatorCameraPosition.transform.position - new Vector3(0, 0, -0.25f);
			transform.rotation = simulatorBackupObjectToMirror.transform.rotation;			
		}
	}
}
