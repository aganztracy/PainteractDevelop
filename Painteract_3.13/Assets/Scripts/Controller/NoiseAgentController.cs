using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseAgentController : MonoBehaviour {
    FastNoise _fastNoise;
    Transform MyPixelsTF;

    ///     int agentsCount = 450;
    // Agent[] agents = new Agent[1000];
    float noiseScale = 440, noiseStrength = 20; // try change noiseScale!!
    float agentAlpha = 76;
    Color agentColor;

    // ------ mouse interaction ------
    // int offsetX = 0, offsetY = 0, clickX = 0, clickY = 0, zoom = 100;
    // float rotationX = 0, rotationY = 0, targetRotationX = 0, targetRotationY = 0, clickRotationX, clickRotationY;
    int spaceSizeX = 800, spaceSizeY = 1300, spaceSizeZ = 800; // control the move region of point(line)

    bool isOutside = false;
    Vector3 p;
    float offset, stepSize, angleY, angleZ, angleX;
    float noiseRun = 0;

    void Awake () {
        _fastNoise = new FastNoise ();
        MyPixelsTF=GameObject.FindGameObjectWithTag("MyPixels").transform;
    }

    void Start () {
        p = new Vector3 (0, 0, 0);
        setRandomPostition (); //Randomly generated position
        this.transform.localPosition = p;//initialize position
        offset = 10000;
        stepSize = Random.Range (5, 28); //stepSize is distance of the point to the last point
        // how many points has the ribbon
        // ribbon = new Ribbon3d (p, (int) Random.Range (50, 300)); //start point,length(num of points in the ribbon(line )

    }

    void Update () {
        noiseRun += 0.005f;
        //first update the value of p
        //then give it to ribbon,as a p0.and ribbon change p1 to before p0;
        angleX = Perlin.Noise(p.x/noiseScale, p.y/noiseScale+offset, p.z/noiseScale) * noiseStrength; 
        angleY =Perlin.Noise (p.x/noiseScale, p.y/noiseScale, p.z/noiseScale+offset) * noiseStrength; 
        angleZ = Perlin.Noise(p.x/noiseScale+offset, p.y/noiseScale, p.z/noiseScale) * noiseStrength; 
        // angleY = Perlin.Noise (p.x / noiseScale + noiseRun, p.y / noiseScale + noiseRun, p.z / noiseScale + noiseRun) * noiseStrength;
        // angleZ = Perlin.Noise (p.x / noiseScale + offset, p.y / noiseScale, p.z / noiseScale + offset) * noiseStrength;
        /* convert polar to cartesian coordinates
         stepSize is distance of the point to the last point
         angleY is the angle for rotation around y-axis
         angleZ is the angle for rotation around z-axis
         */
        p.x += Mathf.Cos (angleZ) * Mathf.Cos (angleY) * stepSize;
        p.y -= Mathf.Sin (angleZ) * stepSize;
        p.z += Mathf.Cos (angleZ) * Mathf.Sin (angleY) * stepSize;
        //println(angleZ);
        //p.x += Mathf.Sin(angleZ)* stepSize;
        //p.y += Mathf.Sin(angleY)* stepSize;
        //p.z += Mathf.Sin(angleZ)* stepSize;

        // boundingbox wrap

        if (p.x < -spaceSizeX || p.x > spaceSizeX ||
            p.y < -spaceSizeY || p.y > spaceSizeY ||
            p.z < -spaceSizeZ || p.z > spaceSizeZ) {
            setRandomPostition ();
            isOutside = true;
        }

        this.transform.localPosition = p;
        // create ribbons
        isOutside = false;

    }
    void setRandomPostition () {
        //p.y=Random.Range(-spaceSizeY*0.6, spaceSizeY*1);
        //p.x=Random.Range(0, 0);
        //p.z=Random.Range(0, 0);

        p.y=spaceSizeY*0.5f;
        p.x=0;
        p.z=Random.Range(0, 0);

        // p.z = Random.Range (-spaceSizeZ * 1, spaceSizeZ * 1);
        // p.x = Random.Range (-spaceSizeX * 1, spaceSizeX * 1);
        // p.y = spaceSizeY;
    }
    // private void OnDrawGizmos () {

    //     Gizmos.color = Color.white;
    //     Gizmos.DrawWireCube (this.transform.position + new Vector3 ((_girdSize.x * _cellsize) * 0.5f, (_girdSize.y * _cellsize) * 0.5f, (_girdSize.z * _cellsize) * 0.5f),
    //         new Vector3 (_girdSize.x * _cellsize, _girdSize.y * _cellsize, _girdSize.z * _cellsize));

    // }
}