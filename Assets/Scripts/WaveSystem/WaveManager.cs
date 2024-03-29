﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	

	[Header("Link to Scene and Prefabs")]
	public GameObject target;
	public GameObject EnemyCollector;
	public GameObject MetalCollector;

	public GameObject[] Spawns;

	
	[Header("Wave System")]
	[SerializeField]
	public  Wave[] Waves;
	[SerializeField]
	private  int currentWave = 0;
	[SerializeField]
	private bool loopWaves = true;

	public bool StartGame = false;
	float waitForNextWave;
	public float waveWaitDuration ;
	private bool waveFinished;


	public bool WaveFinished{
		get{return waveFinished; }
	}

	public Wave CurrentWave{
		get{return Waves [currentWave];}
	}

	
	// Update is called once per frame
	void Update () {
		if(!StartGame)
			return;
		if (waveFinished) {
			waitForNextWave += Time.deltaTime;
			if (waitForNextWave >= waveWaitDuration) {
				waitForNextWave = 0;
				waveFinished = false;
			}
		} else {
			waveFinished = Waves [currentWave].Update (this);
			if(Waves [currentWave].OpenDoor != null && !Waves [currentWave].OpenDoor.GetComponent<DoorOpener> ().open ){				
				Waves [currentWave].OpenDoor.GetComponent<DoorOpener> ().StartOpeningDoor ();
			}
			if (waveFinished) {
				currentWave++;
			}
			if (loopWaves)
				currentWave %= Waves.Length;
		}
	}
}
