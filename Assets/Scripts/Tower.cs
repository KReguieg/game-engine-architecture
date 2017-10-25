using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TowerData{
	public int range;
	public int cost;
	public float speed;
	public float Damage;

	public TowerData(int range, int cost, float speed, float damage){
		this.range = range;
		this.cost = cost;
		this.speed = speed;
		this.Damage = damage;
	}
}


public class Tower : MonoBehaviour {
	[Header("Link to Scene")]
	public List<GameObject> EnemiesInRange;
	public GameObject integratedUiManager;

	[Header("Link to Own Objects")]
	[SerializeField]
	private GameObject towerHead;
	[SerializeField]
	public  GameObject Buildblocker;

	/// <summary>
	/// The components wich gets activatet when the Tower is build
	/// </summary>
	[SerializeField]
	public  GameObject[] activeComponents;

	[Header("Tower Attributes")]
	[SerializeField]
	private float attackspeed;
	private float attacktimer;

	[SerializeField]
	private float damage;


	bool active = false;
	// Use this for initialization
	void Start () {
		EnemiesInRange = new List<GameObject> ();
		attacktimer = Mathf.Infinity;
		foreach (GameObject child in activeComponents) {
			child.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!active) 
			return;
		
		attacktimer += Time.deltaTime;
		if (attacktimer >= 1f / attackspeed) {
			attacktimer = 0;
			GameObject activeTarget = GetTarget ();
			if(activeTarget != null)
			{
				GetComponent<LineRenderer>().SetPositions(new Vector3[2]{ activeTarget.transform.position , towerHead.transform.position}); // Shoot interface to implement shotvarients
				activeTarget.GetComponent<EnemyBehavior>().TakeDamage(damage);
			}
		}
		if (attacktimer >= 0.1f) {
			GetComponent<LineRenderer> ().SetPositions (new Vector3[2]{ Vector3.zero, Vector3.zero });
		}
		GameObject target = GetTarget ();
		if(target != null) {
			towerHead.transform.LookAt(target.transform.position);
			towerHead.transform.Rotate(Vector3.up, -90);
		}
	}

	GameObject GetTarget (){
		EnemiesInRange.ForEach(o => {
			if(o == null || o.tag == "Dead") 
				EmemyLeaveRange(o);
		});
		if (EnemiesInRange.Count > 0) {
			return EnemiesInRange [0];
		}
		return null;
	}

	public void EmemyEnterRange(GameObject enemy){
		EnemiesInRange.Add (enemy);
	}

	public void EmemyLeaveRange(GameObject enemy){
		EnemiesInRange.Remove (enemy);
	}

	public void Select(){
		integratedUiManager.GetComponent<IntergratedUiManager> ().SetTowerMenu (gameObject);
	}

	public void EnableTower(){
		active = true;
		foreach (GameObject child in activeComponents) {
			child.SetActive (true);
		}
	}

	public void Upgrade(){
		
	}

	public void Sell(){
		Destroy (gameObject);
	}
}
