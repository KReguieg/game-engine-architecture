using UnityEditor;
using UnityEngine;
// from http://catlikecoding.com/unity/tutorials/curves-and-splines/
[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor {

	private BezierCurve curve;
	private Transform handleTransform;
	private Quaternion handleRotation;

	private const int lineSteps = 10;
	
	private const float handleSize = 0.04f;
	private const float pickSize = 0.06f;
	
	private int selectedIndex = -1;
	

	private void OnSceneGUI () {
		curve = target as BezierCurve;
		handleTransform = curve.transform;
		handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;

		Vector3 p0 = ShowPoint(0);
		Vector3 p1 = ShowPoint(1);
		Vector3 p2 = ShowPoint(2);
		Vector3 p3 = ShowPoint(3);

		Handles.color = Color.white;
		Handles.DrawLine(p0, p1);
		Handles.DrawLine(p2, p3);

		Handles.color = Color.white;
		Vector3 lineStart = curve.GetPoint(0f);
		
		Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
	}

	private Vector3 ShowPoint (int index) {
		Vector3 point = handleTransform.TransformPoint(curve.points[index]);
		Handles.color = Color.white;
		Handles.DotHandleCap(index,point,handleRotation,pickSize, EventType.Repaint);
		EditorGUI.BeginChangeCheck();
		point = Handles.DoPositionHandle(point, handleRotation);
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(curve, "Move Point");
			EditorUtility.SetDirty(curve);
			curve.points[index] = handleTransform.InverseTransformPoint(point);
		}
		
		return point;
	}
}