using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour {
    public int _band;
    public float _startScale;
    public float _scaleMultiplier;
    public bool _useBuffer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_useBuffer == true)
        {
            transform.localScale = new Vector3(transform.localScale.x,
                (AudioController._bandbuffer[_band] * _scaleMultiplier) + _startScale,
                transform.localScale.z);
        }
        if (_useBuffer == false)
        {
            transform.localScale = new Vector3(transform.localScale.x,
                (AudioController._freqband[_band] * _scaleMultiplier) + _startScale,
                transform.localScale.z);
        }
    }
}
