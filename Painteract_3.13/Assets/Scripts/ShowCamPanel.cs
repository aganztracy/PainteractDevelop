using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCamPanel : MonoBehaviour
{
	public GameObject CameraPanelOBJ;
	bool panelState=false;
    // Use this for initialization
    public void ShowAndHide()
    {
		 panelState=!panelState;
          CameraPanelOBJ.SetActive(panelState);
    }
}
