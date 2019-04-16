using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplacementControl : MonoBehaviour
{

    public float displacementAmount;
    public ParticleSystem explosionParticles;
    MeshRenderer meshRender;

    // Start is called before the first frame update
    void Start()
    {   
        displacementAmount=0.002f;
        meshRender = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        displacementAmount = Mathf.Lerp(displacementAmount, 0, Time.deltaTime);
        meshRender.material.SetFloat("_Amount", displacementAmount);

        if (Input.GetKeyDown("space") ){
            Debug.Log("space");
            displacementAmount += 0.002f;
            explosionParticles.Play();
        }

    }
}
