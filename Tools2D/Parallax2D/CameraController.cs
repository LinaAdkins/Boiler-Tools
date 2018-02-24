using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    //float xin, yin = 0;
    public float movespeed = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float xin = Input.GetAxis("Horizontal");
        float yin = Input.GetAxis("Vertical");

        transform.Translate(xin*Time.deltaTime*movespeed, yin*Time.deltaTime*movespeed, 0f);
	}
}
