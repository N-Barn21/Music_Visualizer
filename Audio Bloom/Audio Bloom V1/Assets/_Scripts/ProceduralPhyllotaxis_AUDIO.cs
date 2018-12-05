using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralPhyllotaxis_AUDIO : MonoBehaviour {

    private Material _trailMat;
    public Color _trailColor;

    public float _degree;
    public float _scale;
    public int _numberStart;
    public int _stepSize;
    public int _maxIteration;

    // Lerping Values
    public bool _useLerping;
    private bool _isLerping;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private float _lerpPosTimer;
    private float _lerpPosSpeed;
    public Vector2 _lerpPosSpeedMinMax;
    public AnimationCurve _lerpPosAnimCurvve;
    public int _lerpPosBand;

    private int _number;
    private int _currentIteration;
    private TrailRenderer _trailRenderer;
    private Vector2 phyllotaxisPosition;

    //What to do when it reaches maxIteration Value
    private bool _forward;
    public bool _repeat;
    public bool _invert;

    // Scaling
    public bool _useScaleAnimation;
    public bool _useScaleCurve;
    public Vector2 _scaleAnimMinMax;
    public AnimationCurve _scaleAnimCurve;
    public float _scalAnimSpeed;
    public int _scaleBand;
    private float _scaleTimer;
    private float _currentScale;

    private Vector2 calcPhyllotaxis(float degree, float scale, int number)
    {
        double angle = number * (degree * Mathf.Deg2Rad);
        float radius = scale * Mathf.Sqrt(number);
        float x = radius * (float)System.Math.Cos(angle);
        float y = radius * (float)System.Math.Sin(angle);

        Vector2 vec2 = new Vector2(x, y);
        return vec2;
    }

    void setLerpPositions()
    {
        phyllotaxisPosition = calcPhyllotaxis(_degree, _currentScale, _number);
        _startPos = this.transform.localPosition;
        _endPos = new Vector3(phyllotaxisPosition.x, phyllotaxisPosition.y, 0);
    }

    void Awake()
    {
        _currentScale = _scale;
        _forward = true;
        _trailRenderer = GetComponent<TrailRenderer>();
        _trailMat = new Material(_trailRenderer.material);
        _trailMat.SetColor("_TintColor", _trailColor);
        _trailRenderer.material = _trailMat;
        _number = _numberStart;
        this.transform.localPosition = calcPhyllotaxis(_degree, _currentScale, _number);
        if (_useLerping)
        {
            _isLerping = true;
            setLerpPositions();
        }
    }

    private void Update()
    {
        if (_useScaleAnimation)
        {
            if (_useScaleCurve)
            {
                _scaleTimer += (_scalAnimSpeed * AudioController._bandbuffer[_scaleBand]) * Time.deltaTime;
                if (_scaleTimer >= 1)
                {
                    _scaleTimer -= 1;
                }
                _currentScale = Mathf.Lerp(_scaleAnimMinMax.x, _scaleAnimMinMax.y,
                    _scaleAnimCurve.Evaluate(_scaleTimer));
            }
            else
            {
                _currentScale = Mathf.Lerp(_scaleAnimMinMax.x, _scaleAnimMinMax.y,
                    AudioController._bandbuffer[_scaleBand]);
            }
        }
        if (_useLerping)
        {
            if (_isLerping)
            {
                _lerpPosSpeed = Mathf.Lerp(_lerpPosSpeedMinMax.x, _lerpPosSpeedMinMax.y,
                    _lerpPosAnimCurvve.Evaluate(AudioController._bandbuffer[_lerpPosBand]));
                _lerpPosTimer += Time.deltaTime * _lerpPosSpeed;
                transform.localPosition = Vector3.Lerp(_startPos, _endPos, Mathf.Clamp01(_lerpPosTimer));
                if(_lerpPosTimer >= 1)
                {
                    _lerpPosTimer -= 1;
                    if (_forward)
                    {
                        _number += _stepSize;
                        _currentIteration++;
                    }
                    else
                    {
                    _number -= _stepSize;
                    _currentIteration--;
                    }
                    if(_currentIteration > 0 && _currentIteration < _maxIteration)
                    {
                        setLerpPositions();
                    }
                    else // Current Iteration has hit 0 or maxiteration
                    {
                        if (_repeat)
                        {
                            if (_invert)
                            {
                                _forward = !_forward;
                                setLerpPositions();
                            }
                            else
                            {
                                _number = _numberStart;
                                _currentIteration = 0;
                                setLerpPositions();
                            }
                        }
                        else
                        {
                            _isLerping = false;
                        }
                    }
                }
            }
        }
        if(!_useLerping)
        {
            phyllotaxisPosition = calcPhyllotaxis(_degree, _currentScale, _number);
            transform.localPosition = new Vector3(phyllotaxisPosition.x, phyllotaxisPosition.y, 0);
            _number += _stepSize;
            _currentIteration++;
        }
    }

}
