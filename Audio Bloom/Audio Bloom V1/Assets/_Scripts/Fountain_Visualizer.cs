using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain_Visualizer: MonoBehaviour {
    public GameObject _sampleCubePrefab;
    private GameObject[] _sampleCubes = new GameObject[224];
    private int[] bandAssignments = new int[224];
    private float[] circleAngles;
    private int[] layerCounts;
    public float _startScale;
    public float _scaleMultiplier;
    public bool _useBuffer;
    public float speed = 10;

    // Use this for initialization of circular cubes formation
    void Start () {
        circleAngles = new float[9]; // Start from top to bottom, remember we only need 6 layers to be calculated
        circleAngles[0] = (360 / 6) * -1; // 1st Circle
        circleAngles[1] = (360 / 8) * -1; // 2nd Circle
        circleAngles[2] = (360 / 10) * -1; // 3nd Circle
        circleAngles[3] = (360 / 20) * -1; // 4nd Circle
        circleAngles[4] = (360 / 30) * -1; // 5nd Circle
        circleAngles[5] = (360 / 40) * -1; // 6nd Circle
        circleAngles[6] = (360 / 50) * -1; // 7nd Circle
        circleAngles[7] = (360 / 60) * -1; // 8nd Circle

        layerCounts = new int[8];
        for (int i = 0; i < layerCounts.Length; i++)
        {
            layerCounts[i] = 0;
        }

        for (int i = 0; i < _sampleCubes.Length; i++)
        {
            GameObject _temp = (GameObject)Instantiate(_sampleCubePrefab); //Create a temp cube to put in array
            _temp.transform.position = this.transform.position; 
            _temp.transform.parent = this.transform;
            _temp.name = "Fountain Cube " + i;
            //  Layer 1 if, draw 6 cubes, occurs twice
            if (i < 6)
            {
                this.transform.eulerAngles = new Vector3(0, circleAngles[0] * layerCounts[0], 0);
                _temp.transform.position = new Vector3(0, 0, 10);
                bandAssignments[i] = 0;
                layerCounts[0]++;
            }
            //  Layer 2 if, draw 8 cubes, occurs twice
            else if (i > 5 && i < 14)
            {
                this.transform.eulerAngles = new Vector3(0, circleAngles[1] * layerCounts[1], 0); //This rotates the object at its origin
                _temp.transform.position = new Vector3(0, 0, 20);
                bandAssignments[i] = 1;
                layerCounts[1]++;
            }
            //  Layer 3 if, draw 10 cubes, occurs twice
            else if (i > 13 && i < 24)
            {
                this.transform.eulerAngles = new Vector3(0, circleAngles[2] * layerCounts[2], 0); //This rotates the object at its origin
                _temp.transform.position = new Vector3(0, 0, 30);
                bandAssignments[i] = 2;
                layerCounts[2]++;
            }
            //  Layer 4 if, draw 20 cubes, occurs twice
            else if (i > 23 && i < 44)
            {
                this.transform.eulerAngles = new Vector3(0, circleAngles[3] * layerCounts[3], 0); //This rotates the object at its origin
                _temp.transform.position = new Vector3(0, 0, 40);
                bandAssignments[i] = 3;
                layerCounts[3]++;
            }
            //  Layer 5 if, draw 30 cubes, occurs twice
            else if (i > 43 && i < 74)
            {   
                this.transform.eulerAngles = new Vector3(0, circleAngles[4] * layerCounts[4], 0); //This rotates the object at its origin
                _temp.transform.position = new Vector3(0, 0, 50);
                bandAssignments[i] = 4;
                layerCounts[4]++;
            }
            //  Layer 6 if, draw 40 cubes, occurs twice
            else if (i > 73 && i < 114)
            {
                this.transform.eulerAngles = new Vector3(0, circleAngles[5] * layerCounts[5], 0); //This rotates the object at its origin
                _temp.transform.position = new Vector3(0, 0, 60);
                bandAssignments[i] = 5;
                layerCounts[5]++;
            }
            //  Layer 7 if, draw 50 cubes, occurs twice
            else if (i > 113 && i < 164)
            {
                this.transform.eulerAngles = new Vector3(0, circleAngles[6] * layerCounts[6], 0); //This rotates the object at its origin
                _temp.transform.position = new Vector3(0, 0, 70);
                bandAssignments[i] = 6;
                layerCounts[6]++;
            }
            //  Layer 8 if, draw 60 cubes, occurs twice
            else if (i > 163 && i < 224)
            {
                this.transform.eulerAngles = new Vector3(0, circleAngles[7] * layerCounts[7], 0); //This rotates the object at its origin
                _temp.transform.position = new Vector3(0, 0, 80);
                bandAssignments[i] = 7;
                layerCounts[7]++;
            }
            _sampleCubes[i] = _temp; //stores temp cube in the array of cubes
        }
    }

    void RotateCircles()
    {
        for (int i = 0; i < _sampleCubes.Length; i++)
        {
            if (i < 6)
            {
                _sampleCubes[i].transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 1, 0), speed * Time.deltaTime);
            }
            //  Layer 2 if, draw 8 cubes, occurs twice
            else if (i > 5 && i < 14)
            {
                _sampleCubes[i].transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, -1, 0), speed * Time.deltaTime);
            }
            //  Layer 3 if, draw 10 cubes, occurs twice
            else if (i > 13 && i < 24)
            {
                _sampleCubes[i].transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 1, 0), speed * Time.deltaTime);
            }
            //  Layer 4 if, draw 20 cubes, occurs twice
            else if (i > 23 && i < 44)
            {
                _sampleCubes[i].transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, -1, 0), speed * Time.deltaTime);
            }
            //  Layer 5 if, draw 30 cubes, occurs twice
            else if (i > 43 && i < 74)
            {
                _sampleCubes[i].transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 1, 0), speed * Time.deltaTime);
            }
            //  Layer 6 if, draw 40 cubes, occurs twice
            else if (i > 73 && i < 114)
            {
                _sampleCubes[i].transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, -1, 0), speed * Time.deltaTime);
            }
            //  Layer 7 if, draw 50 cubes, occurs twice
            else if (i > 113 && i < 164)
            {
                _sampleCubes[i].transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 1, 0), speed * Time.deltaTime);
            }
            //  Layer 8 if, draw 60 cubes, occurs twice
            else if (i > 163 && i < 224)
            {
                _sampleCubes[i].transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, -1, 0), speed * Time.deltaTime);
            }
        }
    }

    // Update is called once per to animate the cubes bases on sound read in
    void Update () {
        for (int i = 0; i < _sampleCubes.Length; i++)
        {
            if (_useBuffer == true)
            {
                _sampleCubes[i].transform.localScale = new Vector3(20,
                    (AudioController._bandbuffer[bandAssignments[i]] * _scaleMultiplier) + _startScale,
                    20);
            }
            if (_useBuffer == false)
            {
                _sampleCubes[i].transform.localScale = new Vector3(20,
                    (AudioController._freqband[bandAssignments[i]] * _scaleMultiplier) + _startScale,
                    20);
            }
        }
        RotateCircles();
    }
}
