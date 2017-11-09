using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBlocker : MonoBehaviour {
	public bool RaycastBlockByUI;
	private static RaycastBlocker instance;
	

	void Start()
	{
		instance = this;
	}

	float resetTimer;
	void LateUpdate(){
		if(RaycastBlockByUI){
			resetTimer += Time.deltaTime;
			if(resetTimer >= 0.5f){
				RaycastBlockByUI = false;
				resetTimer = 0;
			}
		}
	}

	public static RaycastBlocker GetInstance(){
		return instance;
	}
}
