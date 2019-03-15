using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesBoom : MonoBehaviour {

    MyPixel MyPixelOBJ;
    GameObject CanvasOBJ;

    Material Pmaterial;
    Sprite sp;

    // Use this for initialization
    void Start () {

        MyPixelOBJ = gameObject.GetComponent<MyPixel> ();
        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        //Pmaterial = Resources.Load ("Material/particle") as Material;
        Pmaterial = Resources.Load ("Material/P") as Material;
        sp = (Sprite) Resources.Load ("sprites/circle", typeof (Sprite)) as Sprite;
    }

    // Update is called once per frame
    void Update () {

    }

    IEnumerator OnMouseDown () {
        //将物体由世界坐标系转化为屏幕坐标系 ，由vector3 结构体变量ScreenSpace存储，以用来明确屏幕坐标系Z轴的位置  
        Vector3 ScreenSpace = Camera.main.WorldToScreenPoint (transform.position);
        //完成了两个步骤，1由于鼠标的坐标系是2维的，需要转化成3维的世界坐标系，2只有三维的情况下才能来计算鼠标位置与物体的距离，offset即是距离  
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));

        //当鼠标左键按下时  
        while (Input.GetMouseButton (0)) {
            //Debug.Log("particleboom!");   //done 

            //得到现在鼠标的2维坐标系位置  
            Vector3 curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z);
            //将当前鼠标的2维位置转化成三维的位置  
            Vector3 CurPosition = Camera.main.ScreenToWorldPoint (curScreenSpace);

            GameObject particleOBJ = new GameObject ();
            particleOBJ.transform.SetParent (CanvasOBJ.GetComponent<ReadPic> ().MyPixelsTF);
            particleOBJ.AddComponent<ParticleSystem> ();
            //添加材质模块
            particleOBJ.GetComponent<Renderer> ().material = Pmaterial;

            ParticleSystem P = particleOBJ.GetComponent<ParticleSystem> (); // 实例化粒子系统对象
            particleOBJ.transform.SetParent (CanvasOBJ.GetComponent<ReadPic> ().MyPixelsTF);
            particleOBJ.transform.localPosition = new Vector3 (MyPixelOBJ.PosXY.x, MyPixelOBJ.PosXY.y, 10f); //Sets the coordinates relative to the parent object 

            Debug.Log ("addmaterial finished!"); //no
            //实例化粒子系统上各属性模块

            var Pmain = P.main;
            var Pshape = P.shape;
            var Pemission = P.emission;
            Pemission.rateOverTime = 5.0f;

            Pmain.maxParticles = 10;
            Pmain.startColor = MyPixelOBJ.Col;
            //Pmain.gravityModifier = 0.8f;
            Pmain.startSize = 100f;
            Pmain.startSpeed = 10f;
            Pmain.loop = true;
            Pmain.startLifetime = 50;

            Pshape.sprite = sp;
            Pshape.shapeType =  ParticleSystemShapeType.Sphere;

            //销毁原粒子
            Destroy (gameObject);
            //Destroy(P);

            //这个很主要  
            yield return new WaitForFixedUpdate ();
        }

    }
}