using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public List<GameObject> objectsNotAffected = new List<GameObject>();
    private List<Transform> allObjects = new List<Transform>();
    private List<GameObject> allObjectsWithoutCameras = new List<GameObject>();
    private GameObject level;



    void Start()
    {
        level = GameObject.Find("Level");
        foreach (Transform child in level.transform)
            this.allObjects.Add(child);
    }


    public void TriggerGameOverSequence()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(0);
    }
}
