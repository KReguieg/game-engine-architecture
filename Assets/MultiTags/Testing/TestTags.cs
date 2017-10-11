using UnityEngine;
using System.Collections;


public class TestTags : MonoBehaviour {

	// Use this for initialization
	void Start () {
	


	}




	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.A)) {
				
			gameObject.AddTag("Hello World");
	
		}


		if (Input.GetKeyDown (KeyCode.R)) {
			

			gameObject.RemoveTag("Hello World");

		}

		if (Input.GetKeyDown (KeyCode.C)) {
			

			Debug.Log(	gameObject.HasTag("Hello World"));
	
		}
	
	}
}
