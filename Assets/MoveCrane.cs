using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Describe your class quickly here.
/// </summary>
/// <remarks>
/// Author: Khaled Reguieg E-Mail: Khaled.Reguieg@artcom.de
/// </remarks>
public class MoveCrane : MonoBehaviour
{
	[SerializeField]
	private GameObject claw;
	
	// Update is called once per frame
	private void Update ()
	{
		if(Input.GetKey(KeyCode.UpArrow))
			gameObject.transform.position += new Vector3(0 , 0 , 0.1f);
		if(Input.GetKey(KeyCode.DownArrow))
			gameObject.transform.position += new Vector3(0 , 0 , -0.1f);
		if(Input.GetKey(KeyCode.RightArrow))
			gameObject.transform.position += new Vector3(0.1f , 0 , 0);
		if(Input.GetKey(KeyCode.LeftArrow))
			gameObject.transform.position += new Vector3(-0.1f , 0 , 0);
		// if(Input.GetKeyDown(KeyCode.Space))
			//StartCoroutine(DropClaw());

	}
}
