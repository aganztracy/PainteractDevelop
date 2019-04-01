using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainterCamControl : MonoBehaviour
{
    public Camera PainterCametra;
    public float ScaleCVel = 500.0f;
    private float scaleCVelNow = 0.0f;

    public float RotateCVel = 100f;
    private float rotateCVelNow = 0.0f;

    public float MoveCVel = 0.1f;
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

        PainterCametra.transform.Translate(DT *0.01f* moveCVelNow);
        PainterCametra.transform.Rotate(Vector3.forward, DT *10.0f* rotateCVelNow);
        PainterCametra.orthographicSize += DT *0.1f* scaleCVelNow;
       // PainterCametra.orthographicSize = Mathf.Clamp(PainterCametra.orthographicSize, 48.0f, 480.0f);

    }

    public void 恢复原始尺寸()
    {
        PainterCametra.transform.localPosition = originPos;
        PainterCametra.transform.localRotation = originAngle;
        PainterCametra.transform.localScale = originScale;
    }

    public void 记录原始尺寸()
    {
        originPos = PainterCametra.transform.localPosition;
        originScale = PainterCametra.transform.localScale;
        originAngle = PainterCametra.transform.localRotation;
    }

    public void 开始放大()
    {
        scaleCVelNow = -ScaleCVel;
		
    }

    public void 开始缩小()
    {
        scaleCVelNow = ScaleCVel;
    }

    public void 停止缩放()
    {
        scaleCVelNow = 0.0f;
    }

    public void 开始左旋()
    {
        rotateCVelNow = RotateCVel;
    }

    public void 开始右旋()
    {
    	rotateCVelNow = -RotateCVel;
    }

    public void 停止旋转()
    {
    	rotateCVelNow = 0.0f;
    }

    public void 开始左移()
    {
        moveCVelNow.x = MoveCVel;
    }

    public void 开始右移()
    {
        moveCVelNow.x = -MoveCVel;
    }

    public void 开始上移()
    {
        moveCVelNow.y = -MoveCVel;
    }

    public void 开始下移()
    {
        moveCVelNow.y = MoveCVel;
    }

    public void 停止移动()
    {
        moveCVelNow = Vector2.zero;
    }

}
