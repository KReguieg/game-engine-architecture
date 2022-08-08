using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeAvailableCharacter : MonoBehaviour
{
    [SerializeField] private List<GameObject> characterList = new List<GameObject>();
    [SerializeField] private GameObject characterBox = null;

    private List<GameObject> availableCharacter = new List<GameObject>();
    private Vector3 boxSize = new Vector3(2f, 2.5f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            var box = GameObject.Instantiate(characterBox, new Vector3((boxSize.x + 1f) * i, 0f, 0f), Quaternion.identity, this.transform);
            var character = GameObject.Instantiate(characterList[i], box.transform.position,
                Quaternion.LookRotation(Vector3.back, Vector3.up), box.transform);
            this.availableCharacter.Add(character);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
