using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour {
	[Header("Link to Scene and Prefabs")]
	[SerializeField]
	private GameObject MetalPrefab;
	public GameObject MetalCollector;
	[SerializeField]
	private GameObject Healthbar;
	[SerializeField]
	private GameObject AnimatorObject;
	[Header("Attributes")]
	[SerializeField]
	private int carryMetalAmount = 1;
	[SerializeField]
	private float speed, health, maxHealth = 5;

	[SerializeField]
	private EnemyData enemyData;

	public void SetEnemyData(EnemyData data){
		enemyData = data;

		health = data.health;
		maxHealth = data.health;
		carryMetalAmount = data.carryMetal;
		speed = data.speed;

	}
	// Use this for initialization
	void Start () {
		GetComponent<NavMeshAgent> ().speed = speed;
	}

	public void TakeDamage(float damage){
		health -= damage;
		Healthbar.GetComponent<HealthbarColor> ().SetLives (health / maxHealth);

		if (health <= 0)
			Die ();

	}

	public void Die(){
		AnimatorObject.GetComponent<Animator> ().SetTrigger ("Death");
		gameObject.tag = "Dead";
		for (int i = 0; i < carryMetalAmount; i++) {
			GameObject metalpiece = Instantiate (MetalPrefab);
			metalpiece.transform.position = transform.position  + Random.insideUnitSphere;
			metalpiece.GetComponent<Rigidbody> ().AddForceAtPosition ((Vector3.up + Random.insideUnitSphere) * 100 , transform.position);
			metalpiece.transform.SetParent (MetalCollector.transform);
		}
	}

	public void AttackTarget(){
		GetComponent<StartSearch> ().target.GetComponent<Base> ().TakeDamage (enemyData.Damage);
	}
}
