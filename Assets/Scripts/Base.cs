using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
	public GameObject Healthbar;
	public DamageManager BaseDamageManager;

	public float MaxHealth;
	[SerializeField]
	float health;

	void Start(){
		health = MaxHealth;
	}

	public void TakeDamage(float damage){
		health -= damage;
		float amount = health / MaxHealth;
		Healthbar.GetComponent<HealthbarColor> ().SetLives (amount);
		BaseDamageManager.DestroyBase (amount);
	}


}
