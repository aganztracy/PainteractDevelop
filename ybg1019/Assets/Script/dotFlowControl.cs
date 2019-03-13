using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
public class DotFlowControl : MonoBehaviour
{
    public int scl = 10; //scale of vector feild, the smaller scl,the densitier feild
    public VectorField[] FlowField; //ector feild
    public int Cols, Rows;

    // Use this for initialization
    void Start()
    {
        Cols = (int)Mathf.Floor(UnityEngine.Screen.width / scl);
        Rows = (int)Mathf.Floor(UnityEngine.Screen.height / scl);
        FlowField = new VectorField[Cols * Rows];
        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Cols; x++)
            {
                int index1 = x + y * Cols;
                Vector2 v = new Vector2(1, 1);
                v = v * (float)0.004; //set the Magnitude of vector
                FlowField[index1] = new VectorField(x, y, scl);
                //FlowField[index1].draw();
            }
        }

        //show vecterfeilds
		// for (int y = 0; y < Rows; y++)
        // {
        //     for (int x = 0; x < Cols; x++)
        //     {
        //         int index1 = x + y * Cols;
        //         FlowField[index1].draw();
        //     }

        //     //gameobject needs add boxcollider component
        // }
    }

    public void ChangeFeild(Vector2 mouseDirec)
    {
        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Cols; x++)
            {
                int index1 = x + y * Cols;
                FlowField[index1].CheckChange(mouseDirec);

            }
            //gameobject needs add boxcollider component
        }
    }
}
public class VectorField
{
    int x; //col number
    int y; //row number
    public Vector2 Direction; 
    int scl;


    public SpriteRenderer spr;
    public VectorField(int X, int Y, int Scl)
    {
        x = X;
        y = Y;
        Direction = new Vector2(0, 0);
        scl = Scl;

		// GameObject pixShape = new GameObject();
        // pixShape.AddComponent<SpriteRenderer>();
        // spr = pixShape.GetComponent<SpriteRenderer>();
        // spr.color = Color.green;
        // Sprite sp = (Sprite)Resources.Load("sprites/circle", typeof(Sprite)) as Sprite;
        // spr.sprite = sp;
        // pixShape.transform.localPosition = new Vector3(x, y, 10f);//Sets the coordinates relative to the parent object 
    }
    public void CheckChange(Vector2 mouseDirec)
    {
        if (Mathf.Abs(Input.mousePosition.x - x * scl) < scl && Mathf.Abs(Input.mousePosition.y - y * scl) < scl)
        {
            Direction = mouseDirec;
        }
    }
}
