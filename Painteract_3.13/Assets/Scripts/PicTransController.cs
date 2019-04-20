using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
// using System.Runtime.Serialization;  
// using System.Runtime.Serialization.Formatters.Binary;  
/// <summary>
/// 暂时挂在了canvas上
/// </summary>

public class PicTransController : MonoBehaviour {
    public List<AttractParticle> Pic1Atoms;
    public List<AttractParticle> Pic2Atoms;
    public List<AttractParticle> Pic2AtomsTemp; //存储图2 所有原子的链表，因为Pic2Atoms在找完对应索引后元素就被一个个删除了
    GameObject CanvasOBJ;
    public Transform MyPixelsTF;

    /// <summary>
    /// pic 2
    /// </summary>
    public Texture2D Img2 = null;
    private int myScreemWidth = UnityEngine.Screen.width;
    private int myScreemHeight = UnityEngine.Screen.height;

    void Start () {
        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        MyPixelsTF = GameObject.FindWithTag ("MyPixels").transform;
        Pic1Atoms = new List<AttractParticle> ();
        Pic2Atoms = new List<AttractParticle> ();
        Pic2AtomsTemp = new List<AttractParticle> ();

    }
    void ParticlesSetUp () {
        //随机巧妙解决数量不对应问题 但是数量不太对，哪里有问题
        if (Pic2Atoms.Count > Pic1Atoms.Count) { //图2点更多
            int AddNum = Pic2Atoms.Count - Pic1Atoms.Count;
            for (int i = 0; i < AddNum; i++) {
                int RandomNum = Random.Range (0, Pic1Atoms.Count - 1);
                AttractParticle pixel = Pic1Atoms[RandomNum];
                //还要新生成几个物体 复制物体还是确保图2的点一定比图1小？//////////////////////确保小会更简单，更便宜
                // Instantiate(Pic1Atoms[RandomNum].myOBJ,MyPixelsTF);
                // Pic1Atoms.Add (pixel);
            }
        }
        if (Pic2Atoms.Count < Pic1Atoms.Count) { //图2点更少 需要给图2的链表里加重复的索引 以便数量对应
            int AddNum = Pic1Atoms.Count - Pic2Atoms.Count;
            // Debug.Log ("addnum is " + AddNum);
            for (int i = 0; i < AddNum; i++) {
                int RandomNum = Random.Range (0, Pic2Atoms.Count - 1);
                // Debug.Log (RandomNum);
                AttractParticle pixel = Pic2Atoms[RandomNum];
                Pic2Atoms.Add (pixel);
            }
        }

        //寻找自己的索引，随机，还可以做点其他有规律的算法吗？
        foreach (AttractParticle particle in Pic1Atoms) {
            for (int i = 0; i < Pic2Atoms.Count; i++) {
                if (particle.chosed == false) {
                    if (ColourDistance (particle.pixelColor, Pic2Atoms[i].pixelColor) < 0.5) { //去查如何比较颜色的相似性c#
                        particle.target = Pic2Atoms[i].pos;
                        particle.targetColor = Pic2Atoms[i].pixelColor;
                        particle.attractindex = Pic2Atoms[i].orinIndex;
                        Pic2Atoms.RemoveAt (i);
                        // Pic1Atoms.Remove(particle);
                        particle.chosed = true;
                        break;
                    }
                }
            }
        }
        foreach (AttractParticle particle in Pic1Atoms) {
            for (int i = 0; i < Pic2Atoms.Count; i++) {
                if (particle.chosed == false) {
                    int myRandom = Random.Range (0, Pic2Atoms.Count - 1);
                    particle.target = Pic2Atoms[myRandom].pos;
                    particle.targetColor = Pic2Atoms[myRandom].pixelColor;
                    particle.attractindex = Pic2Atoms[i].orinIndex;
                    Pic2Atoms.RemoveAt (myRandom);
                    particle.chosed = true;
                    break;
                }
            }
        }

        // //寻找自己的索引，这个算法复杂度太高， 还能改进吗
        // foreach (AttractParticle particle in Pic1Atoms) {
        //     for (int i = 0; i < Pic2Atoms.Count; i++) {
        //         if (particle.chosed == false && Pic2Atoms[i].chosed == false) {
        //             // Debug.Log("++++++++"+ColourDistance (particle.pixelColor, Pic2Atoms[i].pixelColor));
        //             if (ColourDistance (particle.pixelColor, Pic2Atoms[i].pixelColor) < 0.5) { //去查如何比较颜色的相似性c#
        //                 particle.target = Pic2Atoms[i].pos;
        //                 particle.attractindex = i;
        //                 particle.targetColor = Pic2Atoms[i].pixelColor;
        //                 particle.chosed = true;
        //                 Pic2Atoms[i].chosed = true;
        //                 break;
        //             }
        //         }
        //     }
        // }
        // //剩下的颜色差距过大。一个一个对应可以修改成随机对应吧？
        // foreach (AttractParticle particle in Pic1Atoms) {
        //     for (int i = 0; i < Pic2Atoms.Count; i++) {
        //         // Debug.Log("++++++++"+ColourDistance (particle.pixelColor, Pic2Atoms[i].pixelColor));
        //         if (particle.chosed == false && Pic2Atoms[i].chosed == false) {
        //             particle.target = Pic2Atoms[i].pos;
        //             particle.attractindex = i;
        //             particle.targetColor = Pic2Atoms[i].pixelColor;
        //             particle.chosed = true;
        //             Pic2Atoms[i].chosed = true;
        //             break;
        //         }
        //     }
        // }

    }

    float ColourDistance (Color a, Color b) { //这个比较颜色的算法很随意，待改进
        float abr = Mathf.Abs (a.r - b.r);
        float abg = Mathf.Abs (a.g - b.g);
        float abb = Mathf.Abs (a.b - b.b);
        float com = Mathf.Sqrt (abr * abr + abg * abg + abb * abb);
        return com;
    }

    // public static double ColourDistance (Color e1, Color e2) {
    //     long rmean = ((long) e1.r + (long) e2.r) / 2;
    //     long r = (long) e1.r - (long) e2.r;
    //     long g = (long) e1.g - (long) e2.g;
    //     long b = (long) e1.b - (long) e2.b;
    //     return Mathf.Sqrt ((((512 + rmean) * r * r) >> 8) + 4 * g * g + (((767 - rmean) * b * b) >> 8));
    // }
    ///
    /// https://www.compuphase.com/cmetric.htm
    /// 

    //OPEN PIC 2 FROM PC
    /// <summary>
    /// 所以之前看到说多写工具，少写脚本，这里就体现了，如果之前我写的打开图片的代码段被我包装成了一个工具，现在就可以直接用了，
    /// 而不是复制一遍代码（因为内部调用了一些gameobject之类的，耦合性太高，加上这个之后也会被手机端的导入图片所代替，所以没改写），
    /// 之后安卓端的最好有工具思想！！
    /// </summary>
    /// 

    public void AddPic2(string filepath){
        StartCoroutine (GetTexture (filepath));
    }
    
    public void OpenPic2 () {
        OpenFileDialog od = new OpenFileDialog ();
        od.Title = "请选择头像图片";
        od.Multiselect = false;
        od.Filter = "图片文件(*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp";
        if (od.ShowDialog () == DialogResult.OK) {
            StartCoroutine (GetTexture ("file://" + od.FileName));
        }

    }
    IEnumerator GetTexture (string url) {
        WWW www = new WWW (url);
        yield return www;
        if (www.isDone && www.error == null) {
            Img2 = ResizePic (www.texture); //现在我们得到了图2
            //需要显示第二张图是什么吗，那ui上也要做些事才行，略麻烦
            Pic2Process ();

        }
    }
    Texture2D ResizePic (Texture2D pic) {
        int picW = pic.width;
        int picH = pic.height;
        if (Mathf.Max (picW, picH) == picW) {
            pic = ScaleTexture (pic, myScreemWidth, (int) (myScreemWidth * picH / picW));
        } else {
            pic = ScaleTexture (pic, (int) (picW * myScreemWidth / picH), myScreemWidth);
        }
        return pic;

    }

    Texture2D ScaleTexture (Texture2D source, int targetWidth, int targetHeight) {
        Texture2D result = new Texture2D (targetWidth, targetHeight, source.format, false);
        float incX = (1.0f / (float) targetWidth);
        float incY = (1.0f / (float) targetHeight);

        for (int i = 0; i < result.height; ++i) {
            for (int j = 0; j < result.width; ++j) {
                Color newColor = source.GetPixelBilinear ((float) j / (float) result.width, (float) i / (float) result.height);
                result.SetPixel (j, i, newColor);
            }
        }
        result.Apply ();
        return result;
    }
    public void Pic2Process () {
        if (Img2 == null) return;
        int width = Mathf.FloorToInt (Img2.width);
        int height = Mathf.FloorToInt (Img2.height);
        Color[, ] ImageColor2d = new Color[height, width]; //initialize
        Color[] pix = Img2.GetPixels (0, 0, width, height);
        int pixScale = CanvasOBJ.GetComponent<ReadPic> ().pixScale;
        //align to center

        int rowNum = (int) Mathf.Floor (height / pixScale);
        int cloNum = (int) Mathf.Floor (width / pixScale);

        Color[, ] pixColors2 = new Color[rowNum, cloNum];

        //covert 1d color[] to 2d color[,]
        for (int y = 0, orinIndex = 0, row = 0; row < rowNum; y = y + pixScale, row++) {
            for (int x = 0, clo = 0; clo < cloNum; x = x + pixScale, clo++) {
                ImageColor2d[y, x] = pix[width * y + x]; //pay attention to the writing style of 2d array in c#
                // Debug.Log (ImageColor2d[y, x]);
                Pic2Atoms.Add (new AttractParticle (new Vector3 (x, y, 10.0f), ImageColor2d[y, x], null, orinIndex)); //z是10.0f 在 readPic中这么写的
                Pic2AtomsTemp.Add (new AttractParticle (new Vector3 (x, y, 10.0f), ImageColor2d[y, x], null, orinIndex)); //z是10.0f 在 readPic中这么写的
                pixColors2[row, clo] = ImageColor2d[y, x];
                orinIndex++;
            }
        }

        Debug.Log ("pic2 processed done");
        ParticlesSetUp (); //图片都传入后才能处理两个链表
    }

    /// <summary>
    /// pic2 end
    /// </summary>
}