using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour {
    public GameObject _sampleCubePrefab;
    private GameObject[] _sampleCubes = new GameObject[512];
    public float _maxscale;
	// Use this for initialization of circular cubes formation
	void Start () {
		for(int i = 0; i < 512; i++)
        {
            GameObject _temp = (GameObject)Instantiate(_sampleCubePrefab); //Create a temp cube to put in array
            _temp.transform.position = this.transform.position; 
            _temp.transform.parent = this.transform;
            _temp.name = "Cube" + i;
            /* We want the cubes to be in a circle, a circle is 360 degrees
             * If we want to space out the cubes evenly we should divide 360 degrees by the number of cubes
             * The are 512 cubes, so 360/512 = -0.703125
             * We also want to multiply this by i so that the degree values increases and so the cubes don't stack
             */
            this.transform.eulerAngles = new Vector3(0, -.703125f * i, 0); //This rotates the object at its origin
            _temp.transform.position = Vector3.forward * 100; // this moves the obect at its origin
            _sampleCubes[i] = _temp; //stores temp cube in the array of cubes
        }
	}
	
	// Update is called once per to animate the cubes bases on sound read in
	void Update () {
        for (int i = 0; i < 512; i++)
        {
            //ensures a cable is not Null for some reason, and if so does not apply audio values to it
            if(_sampleCubes[i] != null)
            {
                //This is what will make the cubes respond to the audio values
                _sampleCubes[i].transform.localScale = new Vector3(10, (AudioController._samples[i] * _maxscale) + 2,10);
            }
        }
    }
}
