using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ActivateDaggers : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> flameDaggerSystem = null;
    [SerializeField] AudioSource[] daggerAudioSource = null;
    [SerializeField] AudioClip daggerStart = null;
    [SerializeField] AudioClip daggerIdle = null;

    public void Activate()
    {
        for (int i = 0; i < this.flameDaggerSystem.Count; i++)
        {
            this.flameDaggerSystem[i].Play();
            this.StartCoroutine(PlayDaggerSound(this.daggerAudioSource[i]));
        }
    }

    private IEnumerator PlayDaggerSound(AudioSource audio)
    {
        audio.clip = daggerStart;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.loop = true;
        audio.clip = daggerIdle;
        audio.Play();
    }
}
