using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataCollector : MonoBehaviour {


	[Header("LinkToGui")]
	public GameObject MetalText;
	public GameObject MetalTextBackground;

	[SerializeField]
	private int Metal;

	static DataCollector instance;
	public static DataCollector GetInstance{
		get{ return instance; }
	}
	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool ModifieMetal(int amount){
		if (-amount > Metal) {
			StartCoroutine (BlinkNotEnoughMetalMetal());
			return false;
		}
		Metal += amount;
		MetalText.GetComponent<Text> ().text = "M : " + Metal;
		return true;

	}

	IEnumerator BlinkNotEnoughMetalMetal(){
		Color startColor = MetalTextBackground.GetComponent<RawImage> ().color;
		for (int i = 0; i < 4; i++) {
			MetalTextBackground.GetComponent<RawImage> ().color = Color.red;
			yield return new WaitForSeconds (0.4f);
			MetalTextBackground.GetComponent<RawImage> ().color = startColor;
			yield return new WaitForSeconds (0.4f);
		}
	}
}
