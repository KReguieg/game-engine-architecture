using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
	public GameObject Healthbar;

	public float MaxHealth;
	[SerializeField]
	float health;

	void Start(){
		health = MaxHealth;
	}

	public void TakeDamage(float damage){
		health -= damage;
		Healthbar.GetComponent<HealthbarColor> ().SetLives (health / MaxHealth);
	}
}
