using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_Visualizer : MonoBehaviour {
    public GameObject _sampleCubePrefab;
    private GameObject[] _sampleCubes = new GameObject[80];
    private int[] bandAssignments = new int[80];
    public float _startScale;
    public float _scaleMultiplier;
    public bool _useBuffer;
    Material material;

    // Use this for initialization
    void Start () {
        int count = 0;
        for (int i = 0; i < 80; i++)
        {
            bandAssignments[i] = count;
            GameObject _temp = (GameObject)Instantiate(_sampleCubePrefab); //Create a temp cube to put in array
            _temp.transform.position = this.transform.position;
            _temp.transform.parent = this.transform;
            _temp.name = "Line Cube" + i;
            this.transform.eulerAngles = new Vector3(0, 0, 0); //This rotates the object at its origin
            _temp.transform.position = Vector3.forward * ((i * 7.15f) + 1f); // this moves the obect at its origin
            _sampleCubes[i] = _temp; //stores temp cube in the array of cubes

            if(count == 7)
                count = 0;
            else
                count++;
        }
        this.transform.eulerAngles = new Vector3(0, -90, 0); //This rotates the object at its origin
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 80; i++)
        {
            if (_useBuffer == true)
            {
                material = _sampleCubes[i].GetComponent<MeshRenderer>().materials[0];
                _sampleCubes[i].transform.localScale = new Vector3(20,
                    (AudioController._bandbuffer[bandAssignments[i]] * _scaleMultiplier) + _startScale,
                    20);
                
                Color color = new Color(AudioController.audioBandBuffer[bandAssignments[i]],
                    AudioController.audioBandBuffer[bandAssignments[i]], 
                    AudioController.audioBandBuffer[bandAssignments[i]]);
                material.SetColor("_EmissionColor", color);
                
            }
            if (_useBuffer == false)
            {
                material = _sampleCubes[i].GetComponent<MeshRenderer>().materials[0];
                _sampleCubes[i].transform.localScale = new Vector3(20,
                    (AudioController._freqband[bandAssignments[i]] * _scaleMultiplier) + _startScale,
                    20);
                
                Color color = new Color(AudioController.audioBandBuffer[bandAssignments[i]],
                   AudioController.audioBandBuffer[bandAssignments[i]],
                   AudioController.audioBandBuffer[bandAssignments[i]]);
                material.SetColor("_EmissionColor", color);
                
            }
        }
    }
}
