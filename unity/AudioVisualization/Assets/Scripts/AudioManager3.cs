using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioManager3: MonoBehaviour
{
    AudioSource audioSource;
    public float[] samples = new float[320];
    public static float[] freqBand = new float[8];

    private GameObject sphere;

    private Vector3[] lastPosArr;

    public float amplification = 40;
    public float yPositionOffset = 2;

    public Material innerSphereMaterial;

    // Start is called before the first frame update
    void Start()
    {
        // set audio source
        audioSource = GetComponent<AudioSource>();

        // instanciate sphere with mesh objects

        sphere = new GameObject();
        sphere.AddComponent<Icosahedron>();

        Vector3 startPosition = sphere.transform.position;
        sphere.transform.position = new Vector3(startPosition.x, yPositionOffset + sphere.transform.lossyScale.y, startPosition.z);

        lastPosArr = new Vector3[sphere.transform.childCount];

    }

    void Update()
    {
        //get the spectrum from audio source and update samples array
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);

        float  moveAwayY = (samples[0] * 10);
        Vector3 newPos = new Vector3(0, 0, 0);
        
        
        Vector3[] newPosArr = new Vector3[6];
        
        if (lastPosArr.Length < 1)
        {
            lastPosArr = new Vector3[sphere.transform.childCount];
        }

        for (int i = 0; i < sphere.transform.childCount - 1; i++)
        {
            
            var segment = sphere.transform.Find($"triangle_{i}");

            var segmentObj = segment.gameObject;
            Mesh mesh = segmentObj.GetComponent<MeshFilter>().mesh;
            
            Vector3[] verticies = mesh.vertices;
            
            Vector3 ab = verticies[1] - verticies[0];
            Vector3 ac = verticies[2] - verticies[0];
            
            newPos = Vector3.Cross( ab, ac) * (samples[i] * amplification);
            
            var initialPos = new Vector3(0, 0, 0);

            var moveBack = Vector3.Lerp(lastPosArr[i], - lastPosArr[i], 1);
            segment.transform.Translate(moveBack);
            var moveAway = Vector3.Lerp(initialPos, newPos, 1);
            
            segment.transform.Translate(moveAway);

            lastPosArr[i] = newPos;

        }

        MakeFrequencyBands();
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float avg = 0;
            int sampleCount = (int)Mathf.Pow(2, i + 1);

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                avg += samples[count] * (count + 1);
                count++;
            }

            avg /= count;

            freqBand[i] = avg * 10;

        }
    }
}
