using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnAudio : MonoBehaviour {
    public int _band;
    public float minIntensity;
    public float maxIntensity;
    Light light;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        light.intensity = (AudioController.audioBandBuffer[_band] * (maxIntensity - minIntensity))
            + minIntensity;
	}
}
