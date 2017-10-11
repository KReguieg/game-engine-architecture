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
	private int currentWave = 0;
	[SerializeField]
	private bool loopWaves = true;

	float waitForNextWave;
	float waveDuration;
	bool waveFinished;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (waveFinished) {
			waitForNextWave += Time.deltaTime;
			if (waitForNextWave >= waveDuration) {
				waitForNextWave = 0;
				waveFinished = false;
				currentWave++;
				if(loopWaves)
					currentWave %= Waves.Length;
			}
		}
		else
			waveFinished = Waves [currentWave].Update (this);
	}
}
