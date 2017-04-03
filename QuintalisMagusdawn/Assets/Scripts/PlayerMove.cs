using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public float moveSpeed = 1;
    public CharacterController controller;

	// Use this for initialization
	void Start () {
        //Grab controller
        CharacterController controller = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () {
        var hSpeed = Input.GetAxis("Horizontal");
        var vSpeed = Input.GetAxis("Vertical");

        //Give me a world based movement vector
        var moveDir = new Vector3(hSpeed * moveSpeed,0, vSpeed * moveSpeed);
        //Slap that shit on the camera
        moveDir = Camera.main.transform.TransformDirection(moveDir);
        //holy shit we movin
        controller.SimpleMove(moveDir);
	}
}
