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

	float handDistance = 0;

	float bezierAncorDistance = 1.25f;
	Material armMaterial;
	// Use this for initialization
	void Start () {
		curve = GetComponent<BezierCurve>();
		tubeRenderer = GetComponent<TubeRenderer>();
		
		armVertecies = new Vector3[steps + 1];
		tubeRenderer.SetSize(armVertecies.Length);

		armMaterial = GetComponent<Renderer>().material;

	}
	
	// Update is called once per frame
	void Update () {
		SetHandToCurve();
		CalculateArm();
		tubeRenderer.vertices = armVertecies;
	}
	void CalculateArm(){
		Vector3 previousVertex = Vector3.zero;
		handDistance = 0;
		for (int i = steps; i >= 0 ; i--){
			armVertecies[i] = curve.GetPoint(((float)i/steps));
			handDistance += Vector3.Distance(armVertecies[i], previousVertex);
			previousVertex = armVertecies[i];
		}

		armMaterial.mainTextureScale = new Vector3(1, handDistance * 2);
		
	}

	void SetHandToCurve(){
		curve.points[2] = Hand.transform.localPosition + transform.InverseTransformDirection(Hand.transform.forward * -bezierAncorDistance );
		curve.End = Hand.transform.localPosition;
	}
}
