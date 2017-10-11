//using UnityEngine;
//using System.Collections;
//
//public class MaterialSettings : MonoBehaviour {
//	
//	public Transform FogOfWarPlane;
//	
//	float hSliderValue1 = 9;
//	float hSliderValue2 = 0.7f;
//	float hSliderValue3 = 0.1f;
//	int hSliderValue4 = 90;
//	
//	bool needRebuildPlane = false;
//	float rebuildTimer = 0;	
//	
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void OnGUI () {
//		GUI.Label(new Rect(25, 10, 100, 30), "Fog Radius");
//		hSliderValue1 = GUI.HorizontalSlider(new Rect(25, 30, 100, 30), hSliderValue1, 0.0F, 10.0f);
//		GUI.Label(new Rect(25, 50, 100, 30), "Fog Max Radius");
//		hSliderValue2 = GUI.HorizontalSlider(new Rect(25, 70, 100, 30), hSliderValue2, 0.0F, 1.0f);
//		GUI.Label(new Rect(25, 90, 100, 30), "Fog Alpha");
//		hSliderValue3 = GUI.HorizontalSlider(new Rect(25, 110, 100, 30), hSliderValue3, 0.0F, 1.0f);
//		
//		GUI.Label(new Rect(25, 130, 200, 30), "Plane Vertex Count");
//		int newVertexCount = (int) GUI.HorizontalSlider(new Rect(25, 160, 100, 30), hSliderValue4, 20, 200);
//		if(newVertexCount != hSliderValue4) {
//			needRebuildPlane = true;
//			rebuildTimer = 0;
//		}
//		hSliderValue4 = newVertexCount;
//		
//		GUIStyle style = new GUIStyle(GUI.skin.label);
//		style.fontSize = 12;
//		//style.font = GUI.skin.button.font;
//		//style.font.material.color = Color.white;
//		GUI.Label(new Rect(Screen.width - 180, 10, 180, 80), "Copyright (C) Sergey Taraban", style);
//		GUI.Label(new Rect(Screen.width - 180, 30, 180, 80), "Website http://staraban.com", style);
//	}
//	
//	void Update () {
//		FogOfWarPlane.GetComponent<Renderer>().sharedMaterial.SetFloat("_FogRadius", hSliderValue1);
//		FogOfWarPlane.GetComponent<Renderer>().sharedMaterial.SetFloat("_FogMaxRadius", hSliderValue2);	
//		
//		Color cl = FogOfWarPlane.GetComponent<Renderer>().sharedMaterial.color;
//		cl = new Color(cl.r, cl.g, cl.b, hSliderValue3);
//		FogOfWarPlane.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", cl);	
//		
//		if(needRebuildPlane) {
//			rebuildTimer += Time.deltaTime;
//			if(rebuildTimer > 0.2f) {
//				FogOfWarPlane.GetComponent<OverridePlaneMesh>().widthSegments = hSliderValue4;
//				FogOfWarPlane.GetComponent<OverridePlaneMesh>().lengthSegments = hSliderValue4;				
//				FogOfWarPlane.GetComponent<OverridePlaneMesh>().ModifyPlane();
//				needRebuildPlane = false;
//				rebuildTimer = 0;
//			}
//		}
//	}
//}
