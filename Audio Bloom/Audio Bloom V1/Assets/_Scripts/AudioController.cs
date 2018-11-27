using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioController : MonoBehaviour {
    AudioSource _audiosource; //object that stores source of audio in the project
    public static float[] _samples = new float[512]; // Holds the spectrum data
    public static float[] _freqband = new float[8]; // reduce number of samples to a generic 8
    public static float[] _bandbuffer = new float[8];
    private float[] _bufferdecrease = new float[8];
	
    // Initialization of _audiosource to the sound of the audio file provided
	void Start () {
        _audiosource = GetComponent<AudioSource>();
	}
	
	// Updates the samples variable every frame
	void Update () {
        GetSpectrumAudioSource();
        createFrequencyBands();
        bandbuffer();
    }

    void bandbuffer()
    {
        for(int i =0; i < 8; i++)
        {
            if(_freqband[i] > _bandbuffer[i])
            {
                _bandbuffer[i] = _freqband[i];
                _bufferdecrease[i] = 0.005f;
            }
            if(_freqband[i] < _bandbuffer[i])
            {
                _bandbuffer[i] -= _bufferdecrease[i];
                _bufferdecrease[i] = _bufferdecrease[i] * 1.2f;
            }
        }
    }

    //important so we get more movement in all the cubes as some spectrums are not hit with certain audio frequencies
    void createFrequencyBands()
    {
        /* IMPORTANT READ TO UNDERSTAND THE FREQUENCY PER BAND, MATH INCLUDED
         * Math for how we get the generic 8 frequency bands is done here
         * Song has a total hz value of 22,050
         * to seperate it to each cube, we need to divide 22,050/512 = around 43 hz per sample\
         *  
         * Sample Sizes
         * Keeps track of the number of samples
         * 2 to the zero power is 1 then  * 2 = 2
         * 2 to the first power is 2 then * 2 = 4
         * 2 to the second power is 4 then * 2 = 8
         * 2 to the third power is 8 then * 2 = 16
         * 2 to the fourth power is 16 then * 2 = 32
         * 2 to the fifth power is 32 then * 2 = 64
         * 2 to the sixth power is 64 then * 2 = 128
         * 2 to the seventh power is 128 then * 2 = 256
         * 
         * Take the number of hz per sample size and add to previous iteration to get coverage range
         *  NO OVERLAPPING
         * 0 - 2 samples = 86hz which means 0 - 86 are covered
         * 1 - 4 samples = 172hz which means 87 - 258 are covered
         * 2 - 8 samples = 344hz which means 259 - 602 are covered
         * 3 - 16 samples = 688hz which means 603 - 1290 are covered
         * 4 - 32 samples = 1376hz which means 1291 -2666 are covered
         * 5 - 64 samples = 2752hz which means 2667 - 5418 are covered
         * 6 - 128 samples = 5504hz which means 5419 - 10922 are covered
         * 7 - 256 samples = 11008hz which means 10923 - 21930 are covered
         * 
         */

        // Set the number of samples per each frequency band
        int count = 0;
        for(int i = 0; i < 8; i++)
        {
            float average = 0;
            int numSamples = (int)Mathf.Pow(2, i) * 2; //number of sample per iteration of i should match math above
            
            if(i == 7)
            {
                numSamples += 2;
            }

            //Gets the average of a select number of samples.
            for(int s = 0; s < numSamples; s++)
            {
                average += _samples[count] * (count +1);
                count++;
            }

            average = average / count;  //Divide the sumb of sample values by count of samples
            _freqband[i] = average *10; //Most average will be very small so multiply by 10 to increase them some
        }
    }

    //Gets the numbers of spectrum samples and stores them in the samples variable
    void GetSpectrumAudioSource()
    {
        // (AudioSource Variable).GetSpectrumData(number of samples to be stored, audio channel, window type);
        _audiosource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }
}
