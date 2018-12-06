using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualCones_Visualizer : MonoBehaviour
{
    public AudioController_V2 aC;
    public GameObject _sampleCubePrefab;
    private GameObject[] _sampleCubes = new GameObject[512];
    private int[] bandAssignments = new int[512];
    private int[] layerCounts;
    private float[] sphereAngles;
    public float sphereStart;
    public float _startScale;
    public float _scaleMultiplier;
    public bool _useBuffer;
    public float speed = 10;

    // Use this for initialization of circular cubes formation
    void Start()
    {
        /*  We want the cubes to be in a circle, a circle is 360 degrees
        *   If we want to space out the cubes evenly we should divide 360 degrees by the number of cubes in a layer
        *   The are 512 cubes, with varying cubes in each layer
        *   We also want to multiply this by i so that the degree values increases and so the cubes don't stack
        *   
        *  Sphere Math for layers
        *  1st Layer = 70 total cubes                          | 512 - 70 = 442
        *  2nd Layer = 60 -> 60 * 2 layers = 120 total cubes   | 442 - 120 = 322
        *  3rd Layer = 50 -> 50 * 2 layers = 100 total cubes   | 322 - 100 = 222
        *  4th Layer = 40 -> 40 * 2 layers = 80 total cubes    | 132 - 80 = 142
        *  5th Layer = 30 -> 30 * 2 layers = 60 total cubes    | 142 - 60 = 82
        *  6th layer = 20 -> 20 * 2 layers = 40 total cubes    | 82 - 40 = 42 
        *  7th layer = 10 -> 10 * 2 layers = 20 total cubes    | 42 - 20 = 22 
        *  8th layer = 7 -> 7 * 2 layers = 14 total cubes      | 22 - 14 = 8
        *  9th layer = 4 -> 4 * 2 layers = 8 total cubes       | 8 - 8 = 0
        *  Layer Draw order -> 9-8-7-6-5-4-3-2-1-2-3-4-5-6-7-8-9
         */
        sphereAngles = new float[9]; // Start from top to bottom, remember we only need 6 layers to be calculated
        sphereAngles[0] = (360 / 4) * -1; // Layer 1 and 17
        sphereAngles[1] = (360 / 7) * -1; // Layer 2 and 16
        sphereAngles[2] = (360 / 10) * -1; // Layer 3 and 15
        sphereAngles[3] = (360 / 20) * -1; // Layer 4 and 14
        sphereAngles[4] = (360 / 30) * -1; // Layer 5 and 13
        sphereAngles[5] = (360 / 40) * -1; // Layer 6 and 12
        sphereAngles[6] = (360 / 50) * -1; // Layer 7 and 11
        sphereAngles[7] = (360 / 60) * -1; // Layer 8 and 10
        sphereAngles[8] = (360 / 70) * -1; // Layer 9

        layerCounts = new int [17];
        for(int i = 0; i < layerCounts.Length; i++)
        {
            layerCounts[i] = 0;
        }

        int count = 0;
        for (int i = 0; i < 512; i++)
        {
            GameObject _temp = (GameObject)Instantiate(_sampleCubePrefab); //Create a temp cube to put in array
            _temp.transform.position = this.transform.position;
            _temp.transform.parent = this.transform;
            _temp.name = "Cube" + i;

            //  Layer 9 if, draw 4 cubes, occurs twice
            if (i < 4 || i > 507)
            {
                if (i < 4) // 1st layer of sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[0] * layerCounts[0], 0);
                    _temp.transform.position = new Vector3(0, sphereStart + 40, 5);
                    layerCounts[0]++;
                }
                else      // 17th Layer of Sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[0] * layerCounts[16], 0);
                    _temp.transform.position = new Vector3(0, sphereStart - 40, 5);
                    layerCounts[16]++;
                }
            }
            //  Layer 8 if, draw 7 cubes, occurs twice
            else if ((i > 3 && i < 11) || (i > 500 && i < 508))
            {
                if (i > 3 && i < 11) // 2nd layer of sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[1] * layerCounts[1], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart + 35, 10);
                    layerCounts[1]++;
                }
                else      // 16th Layer of Sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[1] * layerCounts[15], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart - 35, 10);
                    layerCounts[15]++;
                }
            }
            //  Layer 7 if, draw 10 cubes, occurs twice
            else if ((i > 10 && i < 21) || (i > 490 && i < 501))
            {
                if ((i > 10 && i < 21)) // 3rd layer of sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[2] * layerCounts[2], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart + 30, 15);
                    layerCounts[2]++;
                }
                else      // 15th Layer of Sphere
                {
                    this.transform.eulerAngles = new Vector3(0, -sphereAngles[2] * layerCounts[14], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart  - 30, 15);
                    layerCounts[14]++;
                }
            }
            //  Layer 6 if, draw 20 cubes, occurs twice
            else if ((i > 20 && i < 41) || (i > 470 && i < 491))
            {
                if (i > 20 && i < 41) // 4th layer of sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[3] * layerCounts[3], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart + 25, 20);
                    layerCounts[3]++;
                }
                else      // 14th Layer of Sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[3] * layerCounts[13], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart - 25, 20);
                    layerCounts[13]++;
                }
            }
            //  Layer 5 if, draw 30 cubes, occurs twice
            else if ((i > 40 && i < 71) || (i > 440 && i < 471))
            {
                if (i > 40 && i < 71) // 5th layer of sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[4] * layerCounts[4], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart + 20, 25);
                    layerCounts[4]++;
                }
                else      // 13th Layer of Sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[4] * layerCounts[12], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart - 20, 25);
                    layerCounts[12]++;
                }
            }
            //  Layer 4 if, draw 40 cubes, occurs twice
            else if ((i > 70 && i < 111) || (i > 400 && i < 441))
            {
                if (i > 70 && i < 111) // 6th layer of sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[5] * layerCounts[5], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart + 15, 30);
                    layerCounts[5]++;
                }
                else      // 12th Layer of Sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[5] * layerCounts[11], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart - 15, 30);
                    layerCounts[11]++;
                }
            }
            //  Layer 3 if, draw 50 cubes, occurs twice
            else if ((i > 110 && i < 161) || (i > 350 && i < 401))
            {
                if (i > 110 && i < 161) // 7th layer of sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[6] * layerCounts[6], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart + 10, 35);
                    layerCounts[6]++;
                }
                else      // 11th Layer of Sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[6] * layerCounts[10], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart - 10, 35);
                    layerCounts[10]++;
                }
            }
            //  Layer 2 if, draw 60 cubes, occurs twice
            else if ((i > 160 && i < 221) || (i > 290 && i < 351))
            {
                if (i > 160 && i < 221) // 8th layer of sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[7] * layerCounts[7], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart + 5, 40);
                    layerCounts[7]++;
                }
                else      // 10th Layer of Sphere
                {
                    this.transform.eulerAngles = new Vector3(0, sphereAngles[7] * layerCounts[9], 0); //This rotates the object at its origin
                    _temp.transform.position = new Vector3(0, sphereStart - 5, 40);
                    layerCounts[9]++;
                }
            }
            //  layer 1 if, draw 70 cubes, occurs once
            else if (i > 220 && i < 291)
            {
                // 9th Layer of Sphere
                this.transform.eulerAngles = new Vector3(0, sphereAngles[8] * layerCounts[8], 0); //This rotates the object at its origin
                _temp.transform.position = new Vector3(0, sphereStart, 45);
                layerCounts[8]++;
            }
            bandAssignments[i] = 0;
            if (count == 7)
                count = 0;
            else
                count++;

            _sampleCubes[i] = _temp;
        }
    }

    void RotateCircles()
    {
        for (int i = 0; i < _sampleCubes.Length; i++)
        {
            if (i < 4 || i > 507)
            {
                if (i < 4) // 1st layer of sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(-1, -1, 0), speed * Time.deltaTime);
                }
                else      // 17th Layer of Sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(1, -1, 0), speed * Time.deltaTime);
                }
            }
            //  Layer 8 if, draw 7 cubes, occurs twice
            else if ((i > 3 && i < 11) || (i > 500 && i < 508))
            {
                if (i > 3 && i < 11) // 2nd layer of sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(1, -1, 0), speed * Time.deltaTime);
                }
                else      // 16th Layer of Sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(-1, -1, 0), speed * Time.deltaTime);
                }
            }
            //  Layer 7 if, draw 10 cubes, occurs twice
            else if ((i > 10 && i < 21) || (i > 490 && i < 501))
            {
                if ((i > 10 && i < 21)) // 3rd layer of sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(-1, -1, 0), speed * Time.deltaTime);
                }
                else      // 15th Layer of Sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(1, -1, 0), speed * Time.deltaTime);
                }
            }
            //  Layer 6 if, draw 20 cubes, occurs twice
            else if ((i > 20 && i < 41) || (i > 470 && i < 491))
            {
                if (i > 20 && i < 41) // 4th layer of sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(1, -1, 0), speed * Time.deltaTime);
                }
                else      // 14th Layer of Sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(-1, -1, 0), speed * Time.deltaTime);
                }
            }
            //  Layer 5 if, draw 30 cubes, occurs twice
            else if ((i > 40 && i < 71) || (i > 440 && i < 471))
            {
                if (i > 40 && i < 71) // 5th layer of sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(-1, -1, 0), speed * Time.deltaTime);
                }
                else      // 13th Layer of Sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(1, -1, 0), speed * Time.deltaTime);
                }
            }
            //  Layer 4 if, draw 40 cubes, occurs twice
            else if ((i > 70 && i < 111) || (i > 400 && i < 441))
            {
                if (i > 70 && i < 111) // 6th layer of sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(1, -1, 0), speed * Time.deltaTime);
                }
                else      // 12th Layer of Sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(-1, -1, 0), speed * Time.deltaTime);
                }
            }
            //  Layer 3 if, draw 50 cubes, occurs twice
            else if ((i > 110 && i < 161) || (i > 350 && i < 401))
            {
                if (i > 110 && i < 161) // 7th layer of sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(-1, -1, 0), speed * Time.deltaTime);
                }
                else      // 11th Layer of Sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(1, -1, 0), speed * Time.deltaTime);
                }
            }
            //  Layer 2 if, draw 60 cubes, occurs twice
            else if ((i > 160 && i < 221) || (i > 290 && i < 351))
            {
                if (i > 160 && i < 221) // 8th layer of sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(1, -1, 0), speed * Time.deltaTime);
                }
                else      // 10th Layer of Sphere
                {
                    _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(-1, -1, 0), speed * Time.deltaTime);
                }
            }
            //  layer 1 if, draw 70 cubes, occurs once
            else if (i > 220 && i < 291)
            {
                _sampleCubes[i].transform.RotateAround(new Vector3(0, 250, 0), new Vector3(0, -1, 0), speed * Time.deltaTime);
            }
        }
    }

            // Update is called once per to animate the cubes bases on sound read in
            void Update()
    {
        for (int i = 0; i < _sampleCubes.Length; i++)
        {
            if (_useBuffer == true)
            {
                _sampleCubes[i].transform.localScale = new Vector3(
                    (aC._audioBandBuffer[bandAssignments[i]] * _scaleMultiplier) + _startScale,
                    (aC._audioBandBuffer[bandAssignments[i]] * _scaleMultiplier) + _startScale,
                    (aC._audioBandBuffer[bandAssignments[i]] * _scaleMultiplier) + _startScale);
            }
            if (_useBuffer == false)
            {
                _sampleCubes[i].transform.localScale = new Vector3(
                    (aC._AmplitudeBuffer * _scaleMultiplier) + _startScale,
                    (aC._AmplitudeBuffer * _scaleMultiplier) + _startScale,
                    (aC._AmplitudeBuffer * _scaleMultiplier) + _startScale);
            }
        }
        RotateCircles();
    }
}