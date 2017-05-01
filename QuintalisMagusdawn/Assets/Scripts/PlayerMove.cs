using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    [Header("Character Values")]
    public float moveSpeed = 1;
    public float lookSpeed = 1;
    public float hVelocity = 0f;
    public float vVelocity = 0f;
    public float maximumOffset = 4;
    public float cameraHeight = 0.8f;
    [SerializeField]
    [Range(0f, 3f)]
    public float lerpSpeed = 0.02f;

    float hSpeed;
    float vSpeed;
    float hLook;
    float vLook;
    CharacterController controller;
    GameObject boom;
    Vector3 moveDir;
    float hMod = 0f;
    float vMod = 0f;

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
        vLook = Input.GetAxis("LookVertical");
        if (hLook != 0 || vLook != 0)
        {
            RotateCamera();
        }

        //Point camera at player
        Camera.main.transform.LookAt(boom.transform);

        //Move the BOOM towards player movement
        var camTarget = Vector3.zero;

        hMod = Mathf.SmoothDamp(hMod,hSpeed, ref hVelocity,lerpSpeed);
        vMod = Mathf.SmoothDamp(vMod, vSpeed, ref vVelocity, lerpSpeed);
        camTarget = new Vector3(hMod * maximumOffset, 0,vMod * maximumOffset);
        camTarget = boom.transform.TransformDirection(camTarget);

        boom.transform.localPosition = camTarget;
        boom.transform.localPosition += new Vector3(0, cameraHeight,0);
    } 

    void MovePlayer()
    {
        moveDir = new Vector3(0, 0, 0);
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

    void RotateCamera()
    {
        //var vLook = Input.GetAxis("LookVertical");                    //Let's not for now
        //Move Boom, not Camera
        //boom.transform.Rotate(0, hLook * lookSpeed, 0);
        boom.transform.RotateAround(controller.transform.position,Vector3.up,hLook*lookSpeed);
        boom.transform.RotateAround(controller.transform.position, Camera.main.transform.right, vLook * lookSpeed);

    }
}
