using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
	[Header("Link to Scene and Prefabs")]
	public GameObject EnemyPrefab;
	public GameObject target;
	public GameObject EnemyCollector;
	public GameObject MetalCollector;

	[Header("Attributes")]
	public float spawnTimer;
	private float time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= spawnTimer) {
			time -= spawnTimer;

			GameObject obj = Instantiate (EnemyPrefab,EnemyCollector.transform,true);
			obj.GetComponent<StartSearch> ().target = target;
			obj.transform.position = transform.position;
			obj.GetComponent<Enemy> ().MetalCollector = MetalCollector;
		}
	}
}
