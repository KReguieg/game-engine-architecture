using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataCollector : MonoBehaviour {


	[Header("LinkToGui")]
	public GameObject MetalText;

	[SerializeField]
	private int Metal;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool ModifieMetal(int amount){
		if (-amount > Metal)
			return false;
		Metal += amount;
		MetalText.GetComponent<Text> ().text = "M : " + Metal;
		return true;

	}




}
