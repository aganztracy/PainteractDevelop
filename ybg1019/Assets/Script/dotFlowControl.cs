using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
public class dotFlowControl : MonoBehaviour
{
    public int scl = 10; //scale of vector feild, the smaller scl,the densitier feild
    public vectorField[] flowfield; //ector feild
    public int cols, rows;

    // Use this for initialization
    void Start()
    {
        cols = (int)Mathf.Floor(UnityEngine.Screen.width / scl);
        rows = (int)Mathf.Floor(UnityEngine.Screen.height / scl);
        flowfield = new vectorField[cols * rows];
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                int index1 = x + y * cols;
                Vector2 v = new Vector2(1, 1);
                v = v * (float)0.004; //set the Magnitude of vector
                flowfield[index1] = new vectorField(x, y, scl);
                //flowfield[index1].draw();
            }
        }

        //show vecterfeilds
		// for (int y = 0; y < rows; y++)
        // {
        //     for (int x = 0; x < cols; x++)
        //     {
        //         int index1 = x + y * cols;
        //         flowfield[index1].draw();
        //     }

        //     //gameobject needs add boxcollider component
        // }
    }

    public void changeFeild(Vector2 mouseDirec)
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                int index1 = x + y * cols;
                flowfield[index1].checkchange(mouseDirec);

            }
            //gameobject needs add boxcollider component
        }
    }
}
public class vectorField
{
    int x; //col number
    int y; //row number
    public Vector2 direction; 
    int scl;


    public SpriteRenderer spr;
    public vectorField(int X, int Y, int Scl)
    {
        x = X;
        y = Y;
        direction = new Vector2(0, 0);
        scl = Scl;

		// GameObject pixShape = new GameObject();
        // pixShape.AddComponent<SpriteRenderer>();
        // spr = pixShape.GetComponent<SpriteRenderer>();
        // spr.color = Color.green;
        // Sprite sp = (Sprite)Resources.Load("sprites/circle", typeof(Sprite)) as Sprite;
        // spr.sprite = sp;
        // pixShape.transform.localPosition = new Vector3(x, y, 10f);//Sets the coordinates relative to the parent object 
    }
    public void checkchange(Vector2 mouseDirec)
    {
        if (Mathf.Abs(Input.mousePosition.x - x * scl) < scl && Mathf.Abs(Input.mousePosition.y - y * scl) < scl)
        {
            direction = mouseDirec;
        }
    }
}
