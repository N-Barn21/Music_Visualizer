using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSphere_PanelLights : MonoBehaviour {

    public AudioController_V2 aC;
    public int _band;
    public float _startScale;
    public float _scaleMultiplier;
    public bool _useBuffer;

    private float maxScale = 400;
    private float currentscale;

    // Update is called once per frame
    void Update () {
        if (_useBuffer == true)
        {
            this.transform.position = new Vector3(318, 10 + (_band * 20), 186 - ((aC._audioBandBuffer[_band] * _scaleMultiplier) + _startScale));
        }
        if (_useBuffer == false)
        {
            this.transform.position = new Vector3(318, 10 + (_band * 20), 186 - ((aC._AmplitudeBuffer * _scaleMultiplier) + _startScale));
        }
    }
}
