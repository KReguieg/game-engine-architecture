using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiWaveManager : MonoBehaviour {
	public GameObject WavePrefab;
	public GameObject WaveManagerObject;

	List<GameObject> uiWavesList;

	public Vector2 startPos;
	public float xOffset = 120;
	public float DestroyAfterTimer = 1;
	public AnimationCurve AnimationCurve;

	float destroyTimer = 0;
	float animationTimer;
	Vector3[] startPositions;

	// Use this for initialization
	void Start () {
		uiWavesList = new List<GameObject> ();
		Wave[] waves = WaveManagerObject.GetComponent<WaveManager> ().Waves;

		for (int wave = 0; wave < waves.Length; wave++) {
			GameObject uiElement = Instantiate(WavePrefab, transform);
			uiElement.transform.localPosition = startPos + Vector2.right * xOffset * wave;
			uiElement.GetComponentInChildren<Text> ().text = WaveStringBuilder (waves[wave]);
			uiWavesList.Add(uiElement);
		}
	}
	bool alreadyDestroyedUIWave = false;
	// Update is called once per frame
	void Update () {
		if (uiWavesList.Count == 0)
			return;
		Wave currentWave = WaveManagerObject.GetComponent<WaveManager> ().CurrentWave;
		uiWavesList[0].GetComponentInChildren<Text> ().text = WaveStringBuilder (currentWave);
		if (WaveManagerObject.GetComponent<WaveManager> ().WaveFinished ) {
			destroyTimer += Time.deltaTime;
			if (!alreadyDestroyedUIWave) {
			
				destroyTimer = 0;
				alreadyDestroyedUIWave = true;
				Destroy (uiWavesList [0]);
				uiWavesList.RemoveAt (0);

				StartCoroutine (Animate());

			}

		} else {
			alreadyDestroyedUIWave = false;
		}

	}

	void SaveAnimationStartPosition(){
		int i = 0;
		startPositions = new Vector3[uiWavesList.Count];
		foreach (GameObject wave in uiWavesList)
			startPositions [i++] = wave.transform.localPosition;
	}

	IEnumerator Animate(){
		SaveAnimationStartPosition ();
		while(animationTimer <= AnimationCurve.length){
			animationTimer += Time.deltaTime;

			int i = 0;
			foreach (GameObject uiWave in uiWavesList) {
				uiWave.transform.localPosition = Vector3.LerpUnclamped (startPositions[i], startPositions[i] + Vector3.left * xOffset, AnimationCurve.Evaluate(animationTimer));
				i++;
			}
			yield return null;
		}
		animationTimer = 0;
	}

	string WaveStringBuilder(Wave w){ // TODO : Alle verschiednen Typen zusammen zählen
		string s = w.AmountCounter + " / "+ w.EnemiesToSpawn[0].amount ;
		return s;
	}

}
