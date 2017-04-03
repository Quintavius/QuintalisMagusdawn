using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public float moveSpeed = 1;
    public CharacterController controller;
    public GameObject boom;
    public float lookSpeed = 1;

	// Use this for initialization
	void Start () {
        //Grab controller
        controller = GetComponent<CharacterController>();
        boom = GameObject.FindWithTag("CameraBoom");
	}
	
	// Update is called once per frame
	void Update () {
        //Basic Movement
        var hSpeed = Input.GetAxis("Horizontal");
        var vSpeed = Input.GetAxis("Vertical");
 
        var moveDir = new Vector3(hSpeed, 0, vSpeed);                    //Give me a world based movement vector
        moveDir = Camera.main.transform.TransformDirection(moveDir);     //Slap that shit on the camera
        controller.SimpleMove(moveDir * moveSpeed);                      //holy shit we movin

        //Camera Control
        var hLook = Input.GetAxis("LookHorizontal");
        //var vLook = Input.GetAxis("LookVertical");                    //Let's not for now

        boom.transform.Rotate(0, hLook * lookSpeed, 0);
    }
}
