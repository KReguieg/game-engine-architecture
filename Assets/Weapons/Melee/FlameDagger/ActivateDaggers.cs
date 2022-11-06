using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ActivateDaggers : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> flameDaggerSystem = null;


    public void Activate()
    {
        foreach (var dagger in this.flameDaggerSystem)
        {
            dagger.Play();
        }
    }
}
