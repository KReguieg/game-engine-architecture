using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour {

	delegate void Callback(bool ready);
	public TextMesh PC, VR;
	bool pcReady, vrReady;
	WaitForSeconds wait;
	public WaveManager waveManager;
	Animator animator;

	void Start(){
		wait = new WaitForSeconds(1);
		animator = GetComponent<Animator>();
	}

	void StartingGame(){
		pcReady = false;
		vrReady = false;
		waveManager.StartGame = true;
		animator.Play("HideStartGameMenu");
	}

	public void SetPCReady(bool ready){
		PC.color = ready? Color.green:Color.red;
		pcReady = ready;
		if(ready)
			StartCoroutine(DeactivateInOneSecond(SetPCReady));
	}

	IEnumerator DeactivateInOneSecond(Callback callBack){
		if(pcReady && vrReady || Keyboard.current.pKey.wasPressedThisFrame)
			StartingGame();
		else
		{
			yield return wait;
			callBack(false);
		}
	}

	public void SetVRReady(bool ready){
		VR.color = ready? Color.green:Color.red;
		vrReady = ready;
		if(ready)
			StartCoroutine(DeactivateInOneSecond(SetVRReady));
	}
}
