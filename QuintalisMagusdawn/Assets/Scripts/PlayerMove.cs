using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    [Header("Character Values")]
    public float moveSpeed = 1;
    public float lookSpeed = 1;

    [Header("Debug Values")]
    public float hSpeed;
    public float vSpeed;
    public float hLook;
    public CharacterController controller;
    public GameObject boom;

    // Use this for initialization
    void Start () {
        //Grab controller
        controller = GetComponent<CharacterController>();
        boom = GameObject.FindWithTag("CameraBoom");
	}
	
	// Update is called once per frame
	void Update () {
        hSpeed = Input.GetAxis("Horizontal");
        vSpeed = Input.GetAxis("Vertical");
        if (hSpeed != 0 || vSpeed != 0)
        {
            MovePlayer();
        }

        hLook = Input.GetAxis("LookHorizontal");
        if (hLook != 0)
        {
            MoveCamera();
        }
    }

    void MovePlayer()
    {
        var moveDir = new Vector3(0, 0, 0);
        if (Mathf.Abs(hSpeed) == 1 && Mathf.Abs(vSpeed) == 1)
        {
            //Keyboard Hack
            moveDir = new Vector3(hSpeed * 0.75f, 0, vSpeed * 0.75f);
        }
        else
        {
            moveDir = new Vector3(hSpeed, 0, vSpeed);
        }
        moveDir = Camera.main.transform.TransformDirection(moveDir);
        controller.SimpleMove(moveDir * moveSpeed);                      //holy shit we movin
    }

    void MoveCamera()
    {
        //var vLook = Input.GetAxis("LookVertical");                    //Let's not for now
        //Move Boom, not Camera
        boom.transform.Rotate(0, hLook * lookSpeed, 0);
    }
}
