using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WaveType{
	/// <summary>
	/// Amount gets divieded by 10
	/// </summary>
	Normal = 15,
	Fast = 10,
	Group = 2,
	BigGroup = 1
}

public enum SpawnType{
	One,
	Random,
	AllSymetric,
	Row
}

[System.Serializable]
public class Wave{

	public WaveType waveType;

	[Header("Number of Different Enemies in Wave")]
	public List<Enemy> EnemiesToSpawn = new List<Enemy>();

	public SpawnType spawnType = SpawnType.One;
	[SerializeField]
	public bool[] SpawnActive = new bool[4];  

	float time;
	float SpawnSpeed;
	int spawnCounter;
	int amountCounter;
	bool waveSpawnComplete = false;

	public int AmountCounter { 
		get{return amountCounter;}
	}

	// Update is called once per frame
	public bool Update (WaveManager manager) {
		time += Time.deltaTime;
		if (time >= (int)waveType / 10f) {
			time = 0;
			return Spawn (manager);
		}
		return false;
	}


	public bool Spawn(WaveManager manager){
		
		amountCounter++;

		if (amountCounter <= EnemiesToSpawn [spawnCounter].amount) {
			GameObject enemy = GameObject.Instantiate (EnemiesToSpawn [spawnCounter].EnemyPrefab);
			enemy.GetComponent<EnemyBehavior> ().SetEnemyData (EnemiesToSpawn [spawnCounter].Data);
			 
			enemy.transform.localScale =  Vector3.one * ((int)EnemiesToSpawn [spawnCounter].size / 10f);
			enemy.GetComponent<StartSearch> ().target = manager.target;
			enemy.transform.position = SelectSpawn(manager);// manager.Spawns [spawnCounter].transform.position; //TODO: Spawn on diffrent locations
			enemy.GetComponent<EnemyBehavior> ().MetalCollector = manager.MetalCollector;
			enemy.transform.SetParent (manager.EnemyCollector.transform);
		} else {
			amountCounter = 0;
			spawnCounter++;
			if (spawnCounter >= EnemiesToSpawn.Count) {
				spawnCounter = 0;
				return true;
			}
		}
		return false;
	}

	Vector3 SelectSpawn(WaveManager manager){
		GameObject Spawn;
		switch (spawnType) {
			case SpawnType.Random:
				Spawn = manager.Spawns [Random.Range(0,manager.Spawns.Length - 1)];
			break;

		case SpawnType.One:
			Spawn = FindFirstActive (manager);
			break;

			default: 
				Spawn = manager.Spawns [0];
				break;
		}
		return Spawn.transform.position;
	}


	GameObject FindFirstActive(WaveManager manager){
		for (int i = 0; i < SpawnActive.Length; i++) {
			if (SpawnActive [i])
				return manager.Spawns [i];
		}
		Debug.LogError ("No Spawn Active");
		return null;
	}

}

