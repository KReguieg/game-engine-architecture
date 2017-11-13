using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BezierCurve), typeof(TubeRenderer))]
public class ArmCurveGenerator : MonoBehaviour {
	BezierCurve curve;
	TubeRenderer tubeRenderer;

	public int steps = 10;

	public GameObject Shoulder;
	public GameObject Hand;

	Vector3[] armVertecies;
	// Use this for initialization
	void Start () {
		curve = GetComponent<BezierCurve>();
		tubeRenderer = GetComponent<TubeRenderer>();

		armVertecies = new Vector3[steps];
	}
	
	// Update is called once per frame
	void Update () {
		curve.SetEndWithHandle(Hand.transform.localPosition);
		CalculateArm();
		tubeRenderer.vertices = armVertecies;
	}

	void CalculateArm(){
		for (int i = 0; i < steps; i++){
			armVertecies[i] = curve.GetPoint(((float)i/steps));
		}
	}
}
