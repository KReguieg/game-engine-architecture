using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
	public List<GameObject> EnemiesInRange;
	[SerializeField]
	private GameObject towerHead;
	[SerializeField]
	public  GameObject Buildblocker;
	[SerializeField]
	private float attackspeed;
	private float attacktimer;

	[SerializeField]
	private float damage;

	// Use this for initialization
	void Start () {
		EnemiesInRange = new List<GameObject> ();
		attacktimer = Mathf.Infinity;
	}
	
	// Update is called once per frame
	void Update () {
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



}
