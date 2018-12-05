using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralPhyllotaxis : MonoBehaviour {

    public float _degree;
    public float _scale;
    public int _numberStart;
    public int _stepSize;
    public int _maxIteration;

    // Lerping Values
    public bool _useLerping;
    public float _intervalLerp;
    private bool _isLerping;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private float _timeStartedLerping;

    private int _number;
    private int _currentIteration;
    private TrailRenderer _trailRenderer;
    private Vector2 phyllotaxisPosition;

    private Vector2 calcPhyllotaxis(float degree, float scale, int number)
    {
        double angle = number * (degree * Mathf.Deg2Rad);
        float radius = scale * Mathf.Sqrt(number);
        float x = radius * (float)System.Math.Cos(angle);
        float y = radius * (float)System.Math.Sin(angle);

        Vector2 vec2 = new Vector2(x, y);
        return vec2;
    }

    void StartLerping()
    {
        _isLerping = true;
        _timeStartedLerping = Time.time;
        phyllotaxisPosition = calcPhyllotaxis(_degree, _scale, _number);
        _startPos = this.transform.localPosition;
        _endPos = new Vector3(phyllotaxisPosition.x, phyllotaxisPosition.y, 0);
    }

    void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _number = _numberStart;
        this.transform.localPosition = calcPhyllotaxis(_degree, _scale, _number);
        if (_useLerping)
        {
            StartLerping();
        }
    }

    private void FixedUpdate()
    {
        if (_useLerping){
            if (_isLerping)
            {
                float timeSinceStart = Time.time - _timeStartedLerping;
                float percentageComplete = timeSinceStart / _intervalLerp;
                transform.localPosition = Vector3.Lerp(_startPos, _endPos, percentageComplete);
                if(percentageComplete >= 0.97f)
                {
                    transform.localPosition = _endPos;
                    _number += _stepSize;
                    _currentIteration++;
                    if(_currentIteration <= _maxIteration)
                    {
                        StartLerping();
                    }
                    else
                    {
                        _isLerping = false;
                    }
                }
            }
        }
        else
        {
            phyllotaxisPosition = calcPhyllotaxis(_degree, _scale, _number);
            transform.localPosition = new Vector3(phyllotaxisPosition.x, phyllotaxisPosition.y, 0);
            _number+= _stepSize;
            _currentIteration++;
        }
    }
	
}
