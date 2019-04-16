using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParticleController : MonoBehaviour {
    ParticleSystem myParticleSystem;
    private int numParticlesAlive;
    ParticleSystem.Particle[] myParticles;

    GameObject CanvasOBJ;
    Component ReadPicCom;
    int picTotalNum;
    Color[, ] pixColors;
    void Start () {
        myParticleSystem = GetComponent<ParticleSystem> ();
        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        picTotalNum = CanvasOBJ.GetComponent<ReadPic> ().rowNum * CanvasOBJ.GetComponent<ReadPic> ().cloNum;
        pixColors = CanvasOBJ.GetComponent<ReadPic> ().pixColors;

        var myMain = myParticleSystem.main;
        myMain.maxParticles = picTotalNum;
        //unity particle system change the color of each particle via c#
        //https://www.reddit.com/r/Unity3D/comments/1idq7d/how_do_i_change_particle_colors_onthefly/

    }

    private void LateUpdate () {
        InitializeIfNeeded ();

        myParticles = new ParticleSystem.Particle[myParticleSystem.main.maxParticles];
        numParticlesAlive = myParticleSystem.GetParticles (myParticles);
        // myParticleSystem.GetComponent<ParticleSystemRenderer>().material.SetColor ("_EmissionColor", Color.green);;
        // for (int i = 0; i < numParticlesAlive; i++) {
        //     // myParticles[i].velocity += Vector3.up * m_Drift;
        //     myParticles[i].startColor = Color.red;
     
        // }
        for (int x = 0, i = 0; x < pixColors.GetLength (0); x++) { //行数
            for (int y = 0; y < pixColors.GetLength (1); y++) //列数
            {
                if(i>numParticlesAlive) return;
                myParticles[i].startColor = pixColors[x, y];
                // myParticles[i].GetComponent<ParticleSystemRenderer>().material.color = pixColors[x, y];
                i++;
            }

        }
        // Apply the particle changes to the Particle System
        myParticleSystem.SetParticles (myParticles, numParticlesAlive);

    }
    void InitializeIfNeeded () {
        if (myParticleSystem == null)
            myParticleSystem = GetComponent<ParticleSystem> ();

        if (myParticles == null || myParticles.Length < myParticleSystem.main.maxParticles)
            myParticles = new ParticleSystem.Particle[myParticleSystem.main.maxParticles];
    }
}