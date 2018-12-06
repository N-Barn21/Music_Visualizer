using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController_V2 : MonoBehaviour {

    AudioSource _audiosource;
    private float[] _samplesLeft = new float[512];
    private float[] _samplesRight = new float[512];
    
    // 8 Bands
    private float[] _freqband = new float[8];
    private float[] _bandbuffer = new float[8];
    private float[] _bufferdecrease = new float[8];
    private float[] _frequencyBandHighest = new float[8];

    // 64 Bands
    private float[] _freqband64 = new float[64];
    private float[] _bandbuffer64 = new float[64];
    private float[] _bufferdecrease64 = new float[64];
    private float[] _frequencyBandHighest64 = new float[64];

    [HideInInspector]
    public float[] _audioBand, _audioBandBuffer;

    [HideInInspector]
    public float[] _audioBand64, _audioBandBuffer64;

    [HideInInspector]
    public float _Amplitude, _AmplitudeBuffer;
    private float _AmplitudeHighest;
    public float _audioProfile;

    public enum _channel { Stereo, Left, Right };
    public _channel channel = new _channel();

    void Start()
    {
        _audioBand = new float[8];
        _audioBandBuffer = new float[8];
        _audioBand64 = new float[64];
        _audioBandBuffer64 = new float[864];
        _audiosource = GetComponent<AudioSource>();
        AudioProfile(_audioProfile);
    }

    void Update()
    {
        GetSpectrumAudioSource();
        makeFrequencyBands();
        makeFrequencyBands64();
        bandBuffer();
        bandBuffer64();
        CreateAudioBands();
        CreateAudioBands64();
        GetAmplitude();
    }

    void AudioProfile(float audioProfile)
    {
        for (int i = 0; i < 8; i++)
        {
            _frequencyBandHighest[i] = audioProfile;
        }
    }

    void GetAmplitude()
    {
        float _CurrentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;
        for(int i =0; i < 8; i++)
        {
            _CurrentAmplitude += _audioBand[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer[i];
        }
        if(_CurrentAmplitude > _AmplitudeHighest)
        {
            _AmplitudeHighest = _CurrentAmplitude;
        }
        _Amplitude = _CurrentAmplitude / _AmplitudeHighest;
        _AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitudeHighest;
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqband[i] > _frequencyBandHighest[i])
            {
                _frequencyBandHighest[i] = _freqband[i];
            }
            _audioBand[i] = (_freqband[i] / _frequencyBandHighest[i]);
            _audioBandBuffer[i] = (_bandbuffer[i] / _frequencyBandHighest[i]);
        }
    }

    void CreateAudioBands64()
    {
        for (int i = 0; i < 64; i++)
        {
            if (_freqband64[i] > _frequencyBandHighest64[i])
            {
                _frequencyBandHighest64[i] = _freqband64[i];
            }
            _audioBand64[i] = (_freqband64[i] / _frequencyBandHighest64[i]);
            _audioBandBuffer64[i] = (_bandbuffer64[i] / _frequencyBandHighest64[i]);
        }
    }

    void bandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqband[i] > _bandbuffer[i])
            {
                _bandbuffer[i] = _freqband[i];
                _bufferdecrease[i] = 0.005f;
            }
            if (_freqband[i] < _bandbuffer[i])
            {
                _bandbuffer[i] -= _bufferdecrease[i];
                _bufferdecrease[i] *= 1.2f;
            }
        }
    }

    void bandBuffer64()
    {
        for (int i = 0; i < 64; i++)
        {
            if (_freqband64[i] > _bandbuffer64[i])
            {
                _bandbuffer64[i] = _freqband64[i];
                _bufferdecrease64[i] = 0.005f;
            }
            if (_freqband64[i] < _bandbuffer64[i])
            {
                _bandbuffer64[i] -= _bufferdecrease64[i];
                _bufferdecrease64[i] *= 1.2f;
            }
        }
    }

    void makeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int numSamples = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                numSamples += 2;
            }

            for (int s = 0; s < numSamples; s++)
            {
                if (channel == _channel.Stereo)
                {
                    average += (_samplesLeft[count] + _samplesRight[count]) * (count + 1);
                    count++;
                }
                if (channel == _channel.Left)
                {
                    average += _samplesLeft[count] * (count + 1);
                    count++;
                }
                if (channel == _channel.Right)
                {
                    average += _samplesRight[count] * (count + 1);
                    count++;
                }
            }

            average = average / count;  
            _freqband[i] = average * 10;
        }
    }

    void makeFrequencyBands64()
    {
        int count = 0;
        int sampleCount = 1;
        int power = 0;

        for (int i = 0; i < 64; i++)
        {
            float average = 0;

            if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56)
            {
                power++;
                sampleCount = (int)Mathf.Pow(2, power);
                if(power == 3)
                {
                    sampleCount -= 2;
                }
            }

            for (int j = 0; j < sampleCount; j++)
            {
                if (channel == _channel.Stereo)
                {
                    average += (_samplesLeft[count] + _samplesRight[count]) * (count + 1);
                    count++;
                }
                if (channel == _channel.Left)
                {
                    average += _samplesLeft[count] * (count + 1);
                    count++;
                }
                if (channel == _channel.Right)
                {
                    average += _samplesRight[count] * (count + 1);
                    count++;
                }
            }

            average /= count;
            _freqband64[i] = average * 80;
        }
    }

    void GetSpectrumAudioSource()
    {
        _audiosource.GetSpectrumData(_samplesLeft, 0, FFTWindow.Blackman);
        _audiosource.GetSpectrumData(_samplesRight, 0, FFTWindow.Blackman);
    }
}
