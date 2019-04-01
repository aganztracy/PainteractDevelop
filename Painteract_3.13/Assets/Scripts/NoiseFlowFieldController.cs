using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFlowFieldController : MonoBehaviour {
    FastNoise _fastNoise;
    public Vector3Int _girdSize; //三维网格
    public Vector3[, , ] _flowfieldDirection; //方向场
    public float _increment; //增量
    public float _cellsize;//网格细胞大小
    public Vector3 _offset, _offsetSpeed; //偏移和偏移速度

    //particle
    public GameObject _particlePrefab;
    public int _amountOfParticles;
    public List<FlowFieldParticle> _particles;
    public List<MeshRenderer> _particleMeshRenderer;

    public float _spawnRadius;
    public float _particleScale, _particleMoveSpeed, _particleRotateSpeed;

    public int rowNum;
    public int cloNum;
    public int pixScale;


    GameObject CanvasOBJ;
    GameObject MyPixelOBJ;

    bool _particleSpawnVaildation (Vector3 position) {//粒子之间的距离限制，防止距离过近
        bool vaild = true;

        foreach (FlowFieldParticle particle in _particles) {
            if (Vector3.Distance (position, particle.transform.position) < _spawnRadius) {
                vaild = false;
                break;
            }
        }

        if (vaild) {
            return true;
        } else {
            return false;
        }
    }

    // Use this for initialization
    //Awake() Awake is called when the script instance is being loaded.
    //说明会在AudioFlowField.cs中的start()函数执行之后执行

    void Start () {

        CanvasOBJ = GameObject.FindWithTag ("Canvas");
        MyPixelOBJ = GameObject.FindWithTag ("MyPixels");
        rowNum = CanvasOBJ.GetComponent<ReadPic> ().rowNum;
        cloNum = CanvasOBJ.GetComponent<ReadPic> ().cloNum;
        pixScale = CanvasOBJ.GetComponent<ReadPic> ().pixScale;

        _amountOfParticles = rowNum * cloNum;
        _particleScale = 10;

        _girdSize = new Vector3Int (cloNum, rowNum, 25);
        _cellsize = pixScale;

        _flowfieldDirection = new Vector3[_girdSize.x, _girdSize.y, _girdSize.z];
        _fastNoise = new FastNoise ();
        _particles = new List<FlowFieldParticle> ();
        _particleMeshRenderer = new List<MeshRenderer> ();

        for (int i = 0; i < _amountOfParticles; i++) {//给每个粒子一个网格内的随机位置，且位置不重复

            int attempt = 0;

            while (attempt < 100) {

                Vector3 randomPos = new Vector3 (
                    Random.Range (this.transform.position.x, this.transform.position.x + _girdSize.x * _cellsize),
                    Random.Range (this.transform.position.y, this.transform.position.y + _girdSize.y * _cellsize),
                    Random.Range (this.transform.position.z, this.transform.position.z + _girdSize.z * _cellsize)
                );

                bool isVaild = _particleSpawnVaildation (randomPos);

                if (isVaild) {
                    GameObject particleInstance = gameObject.transform.GetChild (i).gameObject;
                    particleInstance.transform.position = randomPos;
                    particleInstance.transform.parent = this.transform;
                    particleInstance.transform.localScale = new Vector3 (_particleScale, _particleScale, _particleScale);
                    _particles.Add (particleInstance.GetComponent<FlowFieldParticle> ());
                    _particleMeshRenderer.Add (particleInstance.GetComponent<MeshRenderer> ());
                    break;
                }
                if (!isVaild) {
                    attempt++;
                }
            }

        }

        Debug.Log (_particles.Count);

    }

    // Update is called once per frame
    void Update () {
        CalculateFlowfieldDirection ();
        ParticleBehavior ();
    }

    void CalculateFlowfieldDirection () {//计算网格内的方向场，粒子随着场运动

        _offset = new Vector3 (_offset.x + (_offsetSpeed.x * Time.deltaTime), _offset.y + (_offsetSpeed.y * Time.deltaTime), _offset.z + (_offsetSpeed.z * Time.deltaTime));

        float xOff = 0f;
        for (int x = 0; x < _girdSize.x; x++) {
            float yOff = 0f;
            for (int y = 0; y < _girdSize.y; y++) {
                float zOff = 0f;
                for (int z = 0; z < _girdSize.z; z++) {
                    float noise = _fastNoise.GetSimplex (xOff + _offset.x, yOff + _offset.y, zOff + _offset.z) + 1;
                    Vector3 noiseDirection = new Vector3 (Mathf.Cos (noise * Mathf.PI), Mathf.Sin (noise * Mathf.PI), Mathf.Cos (noise * Mathf.PI));

                    _flowfieldDirection[x, y, z] = Vector3.Normalize (noiseDirection);
                    // Gizmos.color = new Color(noiseDirection.normalized.x,noiseDirection.normalized.y,noiseDirection.normalized.z,0.4f);
                    // Vector3 pos = new Vector3(x,y,z) + transform.position;
                    // Vector3 endpos = pos + Vector3.Normalize(noiseDirection);
                    // Gizmos.DrawLine(pos,endpos);
                    // Gizmos.DrawSphere(endpos,0.1f);

                    zOff += _increment;
                }
                yOff += _increment;
            }
            xOff += _increment;
        }

    }

    void ParticleBehavior () {

        foreach (FlowFieldParticle p in _particles) {

            //粒子限制在网格内，当超过网格区域后进行返回对面壁的操作

            //X Edges
            if (p.transform.position.x > this.transform.position.x + (_girdSize.x * _cellsize)) {

                p.transform.position = new Vector3 (this.transform.position.x, p.transform.position.y, p.transform.position.z);
            }
            if (p.transform.position.x < this.transform.position.x) {
                p.transform.position = new Vector3 (this.transform.position.x + (_girdSize.x * _cellsize), p.transform.position.y, p.transform.position.z);
            }
            //Y Edges
            if (p.transform.position.y > this.transform.position.y + (_girdSize.y * _cellsize)) {

                p.transform.position = new Vector3 (p.transform.position.x, this.transform.position.y, p.transform.position.z);
            }
            if (p.transform.position.y < this.transform.position.y) {
                p.transform.position = new Vector3 (p.transform.position.x, this.transform.position.y + (_girdSize.y * _cellsize), p.transform.position.z);
            }
            //Z Edges
            if (p.transform.position.z > this.transform.position.z + (_girdSize.z * _cellsize)) {

                p.transform.position = new Vector3 (this.transform.position.x, p.transform.position.y, this.transform.position.z);
            }
            if (p.transform.position.z < this.transform.position.z) {
                p.transform.position = new Vector3 (this.transform.position.x, p.transform.position.y, this.transform.position.z + (_girdSize.z * _cellsize));
            }

            Vector3Int particlePos = new Vector3Int (//计算粒子在网格空间中的位置
                Mathf.FloorToInt (Mathf.Clamp ((p.transform.position.x - this.transform.position.x) / _cellsize, 0, _girdSize.x - 1)),
                Mathf.FloorToInt (Mathf.Clamp ((p.transform.position.y - this.transform.position.y) / _cellsize, 0, _girdSize.y - 1)),
                Mathf.FloorToInt (Mathf.Clamp ((p.transform.position.z - this.transform.position.z) / _cellsize, 0, _girdSize.z - 1))
            );

            //将粒子位置的场方向作用到粒子上
            p.ApplyRotation (_flowfieldDirection[particlePos.x, particlePos.y, particlePos.z], _particleRotateSpeed);
            p._moveSpeed = _particleMoveSpeed;
            //p.transform.localScale = new Vector3 (_particleScale, _particleScale, _particleScale);
        }
    }
    private void OnDrawGizmos () {

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube (this.transform.position + new Vector3 ((_girdSize.x * _cellsize) * 0.5f, (_girdSize.y * _cellsize) * 0.5f, (_girdSize.z * _cellsize) * 0.5f),
            new Vector3 (_girdSize.x * _cellsize, _girdSize.y * _cellsize, _girdSize.z * _cellsize));

    }
}