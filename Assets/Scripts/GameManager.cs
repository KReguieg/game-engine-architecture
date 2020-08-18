using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<GameObject> objectsNotAffected = new List<GameObject>();
    private List<Transform> allObjects = new List<Transform>();
    private List<GameObject> allObjectsWithoutCameras = new List<GameObject>();

    GameObject level;

    void Start()
    {
        level = GameObject.Find("Level");
        foreach (Transform child in level.transform)
        {
            allObjects.Add(child);
        }
    }

    void Update()
    {   
        /*
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Q Pressed!");
            foreach (var obj in allObjects)
            {
                Debug.Log("NAME= " + obj.name);
                obj.gameObject.isStatic = false;
                obj.position = Vector3.one * 20;
            }
        }*/
    }

    public void TriggerGameOverSequence()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(0);
    }
}
