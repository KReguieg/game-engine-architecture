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
			ready = Vector3.Distance(targetPosition, transform.position) <= 0.1f;
			if(!ready)
			{
				Vector3 dir = (targetPosition - transform.position).normalized;			
				transform.position += dir * speed * Time.deltaTime	;
			}
		}
	}
}
