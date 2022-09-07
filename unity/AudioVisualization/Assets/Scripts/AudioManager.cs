using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioManager: MonoBehaviour
{
    AudioSource audioSource;
    public float[] samples = new float[512];

    public GameObject SampleCubePrefab;
    GameObject[] sampleCubes = new GameObject[512];
    public float maxScale;

    // Start is called before the first frame update
    void Start()
    {
        //set audio source

        //instantiate sample cubes in circle
    }

    // Update is called once per frame
    void Update()
    {
        //get the spectrum from audio source and update samples array

        //set local scale of instantiated cubes to the respective sample from samples array
    }    
}
