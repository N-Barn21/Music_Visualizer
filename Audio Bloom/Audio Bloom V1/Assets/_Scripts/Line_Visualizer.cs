using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_Visualizer : MonoBehaviour {
    public AudioController_V2 aC;
    public GameObject _sampleCubePrefab;
    private GameObject[] _sampleCubes = new GameObject[64];
    public float _startScale;
    public float _scaleMultiplier;
    public bool _useBuffer;
    Material material;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 64; i++)
        {
            GameObject _temp = (GameObject)Instantiate(_sampleCubePrefab); //Create a temp cube to put in array
            _temp.transform.position = this.transform.position;
            _temp.transform.parent = this.transform;
            _temp.name = "Line Cube" + i;
            this.transform.eulerAngles = new Vector3(0, 0, 0); //This rotates the object at its origin
            _temp.transform.position = Vector3.forward * ((i * 9f) + 1f); // this moves the obect at its origin
            _sampleCubes[i] = _temp; //stores temp cube in the array of cubes

        }
        this.transform.eulerAngles = new Vector3(0, -90, 0); //This rotates the object at its origin
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 64; i++)
        {
            if (_useBuffer == true)
            {
                material = _sampleCubes[i].GetComponent<MeshRenderer>().materials[0];
                _sampleCubes[i].transform.localScale = new Vector3(20,
                    (aC._audioBandBuffer64[i] * _scaleMultiplier) + _startScale,
                    20);
                
                Color color = new Color(aC._audioBandBuffer64[i],
                    aC._audioBandBuffer64[i],
                    aC._audioBandBuffer64[i]);
                material.SetColor("_EmissionColor", color);
                
            }
            if (_useBuffer == false)
            {
                material = _sampleCubes[i].GetComponent<MeshRenderer>().materials[0];
                _sampleCubes[i].transform.localScale = new Vector3(20,
                    (aC._audioBand64[i] * _scaleMultiplier) + _startScale,
                    20);
                
                Color color = new Color(aC._audioBand64[i],
                   aC._audioBand64[i],
                   aC._audioBand64[i]);
                material.SetColor("_EmissionColor", color);
                
            }
        }
    }
}
