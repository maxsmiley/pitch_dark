﻿using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	public float panSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			transform.Translate(new Vector3(panSpeed * Time.deltaTime,0,0));
		}
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			transform.Translate(new Vector3(-panSpeed * Time.deltaTime,0,0));
		}
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			transform.Translate(new Vector3(0,-panSpeed * Time.deltaTime,0));
		}
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			transform.Translate(new Vector3(0,panSpeed * Time.deltaTime,0));
		}
	}
}
