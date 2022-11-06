using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private List<Character> availableCharacterList = new List<Character>();
    [SerializeField] private AudioClip hoverPortrait = null;
    [SerializeField] private AudioClip clickedOnPortrait = null;

    [SerializeField] private GameObject marigona = null;

    private Button[] characterPortraitsButtons = null;
    private Character selectedCharacter = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void SelectCharacter(Character selectedCharacter)
    {
        this.selectedCharacter = selectedCharacter;
    }

    private void PresentCharacter()
    {
        // ToDo:
        // Play Sound
        // Change character in scene
        // Play Animation
        selectedCharacter.gameObject.GetComponent<Animator>().StartPlayback();
        this.marigona.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.LogFormat("Point hovers over {0}", eventData.pointerCurrentRaycast.gameObject.name);
        eventData.pointerCurrentRaycast.gameObject.GetComponent<AudioSource>().PlayOneShot(this.hoverPortrait);
        // this.PresentCharacter();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Portrait clicked!");
        eventData.pointerCurrentRaycast.gameObject.GetComponent<AudioSource>().PlayOneShot(this.clickedOnPortrait);
        this.SelectCharacter(selectedCharacter);
        this.PresentCharacter();
    }
}
