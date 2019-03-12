using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class painterCamControl : MonoBehaviour
{
    public Camera painterCametra;
    public float scaleCVel = 500.0f;
    private float scaleCVelNow = 0.0f;

    public float rotateCVel = 100f;
    private float rotateCVelNow = 0.0f;

    public float moveCVel = 0.1f;
    private Vector2 moveCVelNow = Vector2.zero;

    private Vector3 originPos, originScale;
    private Quaternion originAngle;
    // Use this for initialization

    void Start() {
    记录原始尺寸();
    }
    void Update()
    {
        float DT = Time.deltaTime;

        painterCametra.transform.Translate(DT *0.01f* moveCVelNow);
        painterCametra.transform.Rotate(Vector3.forward, DT *10.0f* rotateCVelNow);
        painterCametra.orthographicSize += DT *0.1f* scaleCVelNow;
       // painterCametra.orthographicSize = Mathf.Clamp(painterCametra.orthographicSize, 48.0f, 480.0f);

    }

    public void 恢复原始尺寸()
    {
        painterCametra.transform.localPosition = originPos;
        painterCametra.transform.localRotation = originAngle;
        painterCametra.transform.localScale = originScale;
    }

    public void 记录原始尺寸()
    {
        originPos = painterCametra.transform.localPosition;
        originScale = painterCametra.transform.localScale;
        originAngle = painterCametra.transform.localRotation;
    }

    public void 开始放大()
    {
        scaleCVelNow = -scaleCVel;
		
    }

    public void 开始缩小()
    {
        scaleCVelNow = scaleCVel;
    }

    public void 停止缩放()
    {
        scaleCVelNow = 0.0f;
    }

    public void 开始左旋()
    {
        rotateCVelNow = rotateCVel;
    }

    public void 开始右旋()
    {
    	rotateCVelNow = -rotateCVel;
    }

    public void 停止旋转()
    {
    	rotateCVelNow = 0.0f;
    }

    public void 开始左移()
    {
        moveCVelNow.x = moveCVel;
    }

    public void 开始右移()
    {
        moveCVelNow.x = -moveCVel;
    }

    public void 开始上移()
    {
        moveCVelNow.y = -moveCVel;
    }

    public void 开始下移()
    {
        moveCVelNow.y = moveCVel;
    }

    public void 停止移动()
    {
        moveCVelNow = Vector2.zero;
    }

}
