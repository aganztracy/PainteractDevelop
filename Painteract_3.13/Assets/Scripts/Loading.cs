using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Load",0.5f);
    }
    void Load(){
        Debug.Log(":)");
        SceneManager.LoadScene("MOTA(lo-fi)", LoadSceneMode.Single);
    }


}
