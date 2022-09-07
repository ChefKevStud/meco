using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioManager2: MonoBehaviour
{
    AudioSource audioSource;
    public float[] samples = new float[512];
    public static float[] freqBand = new float[8];

    public GameObject SampleCubePrefab;
    //public GameObject SpherePrefab;
    GameObject[] sampleCubes = new GameObject[512];

    private GameObject sphere;
    public float maxScale;
    
    private Vector3 lastPos = new Vector3(0,0,0);
    private Vector3[] lastPosArr = new Vector3[6];
    
    public float xMultiplier = 1;
    public float yMultiplier = 1;
    public float zMultiplier = 0;

    List<Transform> meshs = new();

    // Start is called before the first frame update
    void Start()
    {
        //set audio source
        audioSource = GetComponent<AudioSource>();

        //instantiate sample cubes in circle

        //instanciate sphere with mesh objects
        //GameObject instanceSphere = (GameObject)Instantiate(SpherePrefab);

        sphere = new GameObject("sphere");
        sphere.AddComponent<Planet>();
        
        //sphere.GetComponent<Planet>().xMultiplier = xMultiplier;
        //sphere.GetComponent<Planet>().yMultiplier = yMultiplier;
        //sphere.GetComponent<Planet>().zMultiplier = zMultiplier;

        Vector3 upRotation = new Vector3(0, 0, 0);
        Vector3 downRotation = new Vector3(180, 0, 0);
        Vector3 frontRotation = new Vector3(90, 0, 0);
        Vector3 backRotation = new Vector3(-90, 0, 0);
        Vector3 leftRotation = new Vector3(0, 0, 90);
        Vector3 rightRotation = new Vector3(0, 0, -90);
        
        Vector3[] rotations = { upRotation, downRotation, frontRotation, backRotation, leftRotation, rightRotation};

        foreach (Transform segment in sphere.transform)
        {
            //Debug.Log(child.name);
            meshs.Add(segment);
        }

        for (int i = 0; i < 6; i++)
        {
            var segment = sphere.transform.Find($"mesh_{i}");
            segment.Rotate(rotations[i]);
            Debug.Log(segment.name);
        }

        /*for (int i = 0; i < 512; i++)
        {
            GameObject instanceSampleCube = (GameObject)Instantiate(SampleCubePrefab);
            instanceSampleCube.transform.position = transform.position;
            instanceSampleCube.transform.parent = transform;
            instanceSampleCube.name = "SampleCube" + i;
            transform.eulerAngles = new Vector3(90, -0.703125f * i, 0);
            instanceSampleCube.transform.position = Vector3.forward * 100;
            sampleCubes[i] = instanceSampleCube;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //get the spectrum from audio source and update samples array
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);

        //set local scale of instantiated cubes to the respective sample from samples array
        /*for (int i = 0; i < 512; i++)
        {
            if (sampleCubes != null)
            {
                sampleCubes[i].transform.localScale = new Vector3(10, (samples[i] * 10) + 2, 10);
            }
        }*/

        float  moveAwayY = (samples[0] * 10);
        Vector3 newPos = new Vector3(0, 0, 0);
        
        
        Vector3[] newPosArr = new Vector3[6];

        for (int i = 0; i < 6; i++)
        {
            
            var segment = sphere.transform.Find($"mesh_{i}");
            
            var initialPos = new Vector3(0, 0, 0);
            moveAwayY = samples[i] * 10;

            var moveBack = Vector3.Lerp(lastPosArr[i], - lastPosArr[i], 1);
            segment.transform.Translate(moveBack);
            newPos = new Vector3(0, moveAwayY, 0);
            var moveAway = Vector3.Lerp(initialPos, newPos, 1);
            
            segment.transform.Translate(moveAway);

            lastPosArr[i] = newPos;

        }
        
        /*foreach (Transform child in sphere.transform)
        {
            var initialPos = new Vector3(0, 0, 0);
            //var lastPos = child.position;
            
            //Debug.Log(child.name);
            //Debug.Log(lastPos);

            var moveBack = Vector3.Lerp(lastPos, - lastPos, 1);
            child.transform.Translate(moveBack);
            //child.transform.Translate(0, - lastMove, 0);
            newPos = new Vector3(0, moveAwayY, 0);
            var moveAway = Vector3.Lerp(initialPos, newPos, 1);
            
            child.transform.Translate(moveAway);
        }
        
         lastPos = newPos;*/

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
