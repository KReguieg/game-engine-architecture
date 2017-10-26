using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	[Header("Link to Scene and Prefabs")]
	public GameObject target;
	public GameObject EnemyCollector;
	public GameObject MetalCollector;

	public GameObject[] Spawns;
	public GameObject[] Doors;

	public int OpenSouthDoorAtLvl, OpenEastDoorAtLvl, OpenWestDoorAtLvl;
	[Header("Wave System")]
	[SerializeField]
	public  Wave[] Waves;
	[SerializeField]
	private  int currentWave = 0;
	[SerializeField]
	private bool loopWaves = true;


	float waitForNextWave;
	public float waveWaitDuration ;
	private bool waveFinished;


	public bool WaveFinished{
		get{return waveFinished; }
	}

	public Wave CurrentWave{
		get{return Waves [currentWave];}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (waveFinished) {
			waitForNextWave += Time.deltaTime;
			if (waitForNextWave >= waveWaitDuration) {
				waitForNextWave = 0;
				waveFinished = false;

			}
		} else {
			waveFinished = Waves [currentWave].Update (this);

			if (waveFinished) {
				currentWave++;
				OpenDoors ();
			}
			if (loopWaves)
				currentWave %= Waves.Length;
		}
	}

	void OpenDoors(){
		if (currentWave == OpenEastDoorAtLvl) {
			Doors [0].GetComponent<DoorOpener> ().StartOpeningDoor ();
			return;
		}
		if (currentWave == OpenSouthDoorAtLvl) {
			Doors [1].GetComponent<DoorOpener> ().StartOpeningDoor ();
			return;
		}
		if (currentWave == OpenWestDoorAtLvl) {
			Doors [2].GetComponent<DoorOpener> ().StartOpeningDoor ();
		}
	}
}
