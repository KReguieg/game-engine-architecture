using UnityEngine;

public class PositionSeeker : MonoBehaviour {
	private Vector3 targetPosition;
	public float speed = 2;

	public bool ready;
    public Vector3 TargetPosition { 
		get {return targetPosition;} 
		set {targetPosition = value;} 
	}
	
	// Update is called once per frame
	void Update () {
		if(targetPosition != Vector3.zero){
			float dist = Vector3.Distance(targetPosition, transform.position);
			ready = (dist <= 0.1f);
			Vector3 dir = (targetPosition - transform.position).normalized;
			float distFaktor = Mathf.Clamp(dist,0,1f);
			transform.position += dir * speed * Time.deltaTime * distFaktor	;
			
		}
	}
}
