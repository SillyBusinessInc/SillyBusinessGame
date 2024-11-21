using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SkibidiDirkScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> apearObjectsThings;

    private void Awake()
    {
        apearObjectsThings.ForEach(obj => obj.SetActive(false));
        GlobalReference.SubscribeTo(Events.ALL_ENEMIES_DEAD, OnWaveDone);
    }

    private void Update()
    {
     
    }

    private void OnWaveDone()
    {
        apearObjectsThings.ForEach(obj => obj.SetActive(true));
    }
}
