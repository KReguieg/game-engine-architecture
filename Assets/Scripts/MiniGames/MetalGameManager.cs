using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalGameManager : MonoBehaviour {

	public GameObject MetalPrefab;
	public GameObject MetalCollector;
	public Vector3 SpawnForce;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.M))
			Win();
	}

	public void Win(){
	
		StartCoroutine(SpawnMetal());
		
	}

	WaitForSeconds wait = new WaitForSeconds(0.2f);
	IEnumerator SpawnMetal(){
		for(int i = 0; i <= 10; i++)
		{
			GameObject metal = Instantiate( MetalPrefab, MetalCollector.transform );
			metal.transform.position = transform.position;
			metal.GetComponent<Rigidbody>().velocity = SpawnForce;
			yield return wait;
		}
	}
}
