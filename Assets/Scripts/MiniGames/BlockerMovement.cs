using UnityEngine;
using UnityEngine.InputSystem;

public class BlockerMovement : MonoBehaviour
{
	private Vector3 startPos;


	// Use this for initialization
	void Start ()
	{
		this.startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Keyboard.current.spaceKey.wasPressedThisFrame)
			this.MoveDirection(Vector3.up * 0.2f);
		this.MoveBack();
	}

    private void MoveBack()
    {
        Vector3 dir = (startPos - transform.position).normalized;
		this.MoveDirection(dir * 0.01f);
    }

	private void MoveDirection(Vector3 direction)
	{
		transform.position += direction;
	}
}
