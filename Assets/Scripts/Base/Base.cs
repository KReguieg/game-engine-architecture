using UnityEngine;

public class Base : MonoBehaviour {

	public GameObject Healthbar;

    public DamageManager BaseDamageManager;

	public float MaxHealth;

	[SerializeField]
	private float health;

    [SerializeField]
    private GameManager gameManager;

	void Start()
    {
		health = MaxHealth;
	}

    public void TakeDamage(float damage)
    {
		health -= damage;
		float amount = health / MaxHealth;
		Healthbar.GetComponent<HealthbarColor> ().SetLives (amount);
		BaseDamageManager.DestroyBase (amount);
	}

    void Update()
    {
        if (health <= 0)
            gameManager.TriggerGameOverSequence();
    }
}
