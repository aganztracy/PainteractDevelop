 using System.Collections.Generic;
 using System.Collections;
 using UnityEngine;

 [RequireComponent (typeof (MeshFilter))] //????
 public class MeshGeneratorController : MonoBehaviour {
     GameObject CanvasOBJ;
     Mesh mesh;
     Vector3[] vertices;
     int[] triangles;

     Vector2[] uvs;
     Color[] colors;
     Color[, ] pixColors;

     public int xSize = 0; //得到列数 
     public int ySize = 0; //行数



     void Start () {
         CanvasOBJ = GameObject.FindWithTag ("Canvas");
         ySize = CanvasOBJ.GetComponent<ReadPic> ().rowNum;
         xSize = CanvasOBJ.GetComponent<ReadPic> ().cloNum;
         pixColors = CanvasOBJ.GetComponent<ReadPic> ().pixColors;

         mesh = new Mesh ();
         GetComponent<MeshFilter> ().mesh = mesh;
         //  StartCoroutine (CreateShape ());
         CreateShape ();
         UpdateMesh ();
     }
     void Update () {
         //  UpdateMesh ();

     }
     void CreateShape () {
         //创建顶点
         vertices = new Vector3[(xSize + 1) * (ySize + 1)];
         for (int i = 0, y = 0; y <= ySize; y++) {
             for (int x = 0; x <= xSize; x++) {

                //  float y = 0f;
                 float z = Mathf.PerlinNoise (x * .3f, y * .3f) * 2f;
                 vertices[i] = new Vector3 (x, y, z);
                 i++;
             }
         }

         //创建三角形
         triangles = new int[xSize * ySize * 6];
         int vert = 0;
         int tris = 0;
         for (int y = 0; y < ySize; y++) { //循环行数次
             for (int x = 0; x < xSize; x++) { //绘制一排三角形，一个四边形是两个三角形。4个顶点，2组顶点顺序，每组按顺时针顺序排列的3个点
                 triangles[tris + 0] = vert + 0;
                 triangles[tris + 1] = vert + xSize + 1;
                 triangles[tris + 2] = vert + 1;
                 triangles[tris + 3] = vert + 1;
                 triangles[tris + 4] = vert + xSize + 1;
                 triangles[tris + 5] = vert + xSize + 2;
                 vert++;
                 tris += 6;
                 //  yield return new WaitForSeconds (.01f);
             }
             vert++; //终止这个三角形，跳到下一行去画三角形，佛则第一行的顶点到第二行的顶点就还会有连线，而且此时lighting打光就会很奇怪（因为平面布线太奇怪）
         }

         //  //创建uv映射
         //  uvs = new Vector2[vertices.Length];
         //  for (int i = 0, z = 0; z <= ySize; z++) {
         //      for (int x = 0; x <= xSize; x++) {

         //          uvs[i] = new Vector2 ((float) x / xSize, (float) z / ySize); //因为我们计算的值有小数点，所以要（float）
         //          i++;
         //      }
         //  }

         colors = new Color[vertices.Length];

         for (int i = 0, y = 0; y< ySize; y++) {
             for (int x = 0; x < xSize; x++) {
                 //有类似的color数组吗
                  colors[i]=pixColors[y,x];//颜色的映射有问题？？？？拿纸出来算一下，是怎么回事，或者做点测试图啊看看   
                 i++;
             }
         }

     }
     void UpdateMesh () {
         mesh.Clear ();
         mesh.vertices = vertices;
         mesh.triangles = triangles;
         //  mesh.uv = uvs;

         mesh.colors = colors;
         
         mesh.RecalculateNormals ();
     }

     //  //用gizmos 看顶点
     //  private void OnDrawGizmos () {
     //      if (vertices == null) return;
     //      for (int i = 0; i < vertices.Length; i++) {
     //          Gizmos.DrawSphere (vertices[i], .1f);
     //      }
     //  }
 }