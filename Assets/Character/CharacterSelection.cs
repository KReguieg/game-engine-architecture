using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private List<Character> availableCharacterList = new List<Character>();
    [SerializeField] private AudioClip hoverPortrait = null;
    [SerializeField] private AudioClip clickedOnPortrait = null;
    [SerializeField] private CinemachineVirtualCamera charSelectedCamera = null;

    [SerializeField] private GameObject marigona = null;

    private Button[] characterPortraitsButtons = null;
    private Character selectedCharacter = null;


    public void SelectCharacter(Character selectedCharacter)
    {
        Debug.Log($"Selected Character: { selectedCharacter.name }");
        this.selectedCharacter = selectedCharacter;
        this.charSelectedCamera.gameObject.SetActive(true);
        this.PresentCharacter();
    }

    private void PresentCharacter()
    {
        // ToDo:
        // Play Sound
        // Change character in scene
        // Play Animation
        this.selectedCharacter.gameObject.GetComponent<Animator>().StartPlayback();
        this.selectedCharacter.gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.LogFormat("Point hovers over {0}", eventData.pointerCurrentRaycast.gameObject.name);
        eventData.pointerCurrentRaycast.gameObject.GetComponent<AudioSource>().PlayOneShot(this.hoverPortrait);
        // this.PresentCharacter();
    }
}
