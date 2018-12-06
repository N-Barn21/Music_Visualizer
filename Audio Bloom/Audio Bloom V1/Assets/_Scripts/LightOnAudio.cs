using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnAudio : MonoBehaviour {
    public AudioController_V2 aC;
    public int _band;
    public float minIntensity;
    public float maxIntensity;
    Light light;

    //Color control Variables
    public float speed = 1.0f;
    public Color startColor;
    public Color endColor;
    public bool repeatable = false;
    float startTime;
	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        startTime = Time.time;
	}
 
    void LightColorChange()
    {
        if (!repeatable)
        {
            float t = (Time.time - startTime) * speed;
            light.color = Color.Lerp(startColor, endColor, t);
        }
        else
        {
            float t = (Mathf.Sin(Time.time - startTime) * speed);
            light.color = Color.Lerp(startColor, endColor, t);
        }
    }

    // Update is called once per frame
    void Update () {
        light.intensity = (aC._audioBandBuffer[_band] * (maxIntensity - minIntensity))
            + minIntensity;
        //startColor = new Color(Random.value, Random.value, Random.value);
        //endColor = new Color(Random.value, Random.value, Random.value);
        LightColorChange();

    }
}
