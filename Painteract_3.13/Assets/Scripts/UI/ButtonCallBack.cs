using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCallBack : MonoBehaviour
{
    
    public int Control;
    public GameObject CanvasOBJ;

    public Text BtnText;
    public Image BtnImage;

    // Start is called before the first frame update
    void Start () {
        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        BtnText = this.transform.Find("Text").GetComponent<Text>();
        BtnText.text = Control.ToString();
        
        Button btn = this.GetComponent<Button> ();
        btn.onClick.AddListener (OnClick);
        
    }

    private void OnClick(){

    CanvasOBJ.GetComponent<ReadPic>().SetControlTo(Control);
    Debug.Log("SetControlTo(Control);"+Control);

    }

  
}
