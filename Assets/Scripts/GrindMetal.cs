using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrindMetal : MonoBehaviour {
	public List<GameObject> MetalinFirstStage;
	public List<GameObject> MetalinSecondStage;

	[Header("Link to Scene")]
	public GameObject StageOneTarget;
	public GameObject StageTwoTarget;
	public GameObject ParticelSystemObject;
	public GameObject DataCollector;



	[Header("Gravitanional Setup")]
	public float FirstStageForce;
	//public float FirstStageDistance;
	public float SecondStageForce;
	public float MaxSpeed = 2, MaxSpeedSecond;
	public float SpeedDamp = 0.2f;

	[Header("PartikelSystem")]
	public AnimationCurve BurstEmitAtDestroyCurve;
	public int minBurstAmount;
	public int maxBurstAmount;
	List<float> triggerdBurstEmmits;

	[Header("Show Metal Gain")]
	public GameObject MetalGainedPrefab;
	[SerializeField]
	public float showAfterTimer;
	int metalMined;
	float gainTimer;

	// Use this for initialization
	void Start () {
		MetalinFirstStage = new List<GameObject> ();
		MetalinSecondStage  = new List<GameObject> (); 
		triggerdBurstEmmits = new List<float> ();
	}
	
	// Update is called once per frame
	void Update () {
		FistStageUpdate ();
		SecondStageUpdate ();
		TriggerPartikelSystem ();
	}

	void TriggerPartikelSystem(){
		for (int i = 0; i < triggerdBurstEmmits.Count; i++) {
			if (triggerdBurstEmmits [i] >= BurstEmitAtDestroyCurve.length) // longer than the animation Curve
				triggerdBurstEmmits.RemoveAt (i--);
			else {
				triggerdBurstEmmits [i] += Time.deltaTime;
				float emitstrength = BurstEmitAtDestroyCurve.Evaluate (triggerdBurstEmmits [i]);
				ParticelSystemObject.GetComponent<ParticleSystem> ().Emit ((int)(emitstrength * Random.Range(minBurstAmount, maxBurstAmount)));
			}
		}
	}

	public void FixedUpdate(){
		if (metalMined != 0) {
			gainTimer += Time.fixedDeltaTime;
			if (gainTimer >= showAfterTimer) {
				gainTimer = 0;
				GameObject gain = Instantiate (MetalGainedPrefab);
				gain.transform.position = transform.position;
				gain.GetComponentInChildren<TextMesh> ().text = "+" + metalMined;
				metalMined = 0;
			}
		}
	}

	public void MetalEnterRange(GameObject metal){
		metal.transform.GetChild(0).tag = "Untagged";
		metal.GetComponent<Rigidbody> ().useGravity = false;
		MetalinFirstStage.Add (metal);
	} 

	void FistStageUpdate(){
		for (int i = 0; i < MetalinFirstStage.Count; i++) {
			GameObject metal = MetalinFirstStage [i];
			Vector3 forceDirection = StageOneTarget.transform.position - metal.transform.position;

			forceDirection = forceDirection.normalized * FirstStageForce;

			metal.GetComponent<Rigidbody> ().AddForce (forceDirection);
			if (metal.GetComponent<Rigidbody> ().velocity.magnitude >= MaxSpeed)
				metal.GetComponent<Rigidbody> ().velocity = metal.GetComponent<Rigidbody> ().velocity.normalized * MaxSpeed;
		}

	}

	public void ReachedFirstStage(GameObject metal){
		MetalinFirstStage.Remove (metal);
		MetalinSecondStage.Add (metal);

		//metal.GetComponent<Rigidbody> ().velocity = Vector3.zero;

	}

	void SecondStageUpdate(){
		for (int i = 0; i < MetalinSecondStage.Count; i++) {
			GameObject metal = MetalinSecondStage [i];
			if (metal == null) {
				MetalinSecondStage.RemoveAt (i--);
				continue;
			}

			if (metal.transform.position.y <= StageTwoTarget.transform.position.y) {
				MetalinSecondStage.RemoveAt (i);
				MetalinFirstStage.Add (metal);
				i--;
			} else {
				Vector3 forceDirection = StageTwoTarget.transform.position - metal.transform.position;

				forceDirection = forceDirection.normalized * SecondStageForce;

				if (metal.GetComponent<Rigidbody> ().velocity.magnitude >= MaxSpeedSecond)
					metal.GetComponent<Rigidbody> ().velocity *= SpeedDamp;

				metal.GetComponent<Rigidbody> ().AddForce (forceDirection);

			}
		}
	}

	public void ReachedSecondStage(GameObject metal){
		MetalinSecondStage.Remove (metal);
		MetalinFirstStage.Remove (metal);
		Destroy (metal);

		triggerdBurstEmmits.Add (0);
		metalMined += 1;
		DataCollector.GetComponent<DataCollector> ().ModifieMetal (1);
	}
}
