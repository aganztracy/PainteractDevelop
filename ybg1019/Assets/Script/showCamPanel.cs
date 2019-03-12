using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showCamPanel : MonoBehaviour
{
	public GameObject cameraPanel;
	bool panelState=false;
    // Use this for initialization
    public void showAndHide()
    {
		 panelState=!panelState;
          cameraPanel.SetActive(panelState);
    }
}
