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
    public float cameraDistance = -7.5f;
    public float cameraHeight = 0.8f;
    public AnimationCurve panCurve;
    [SerializeField]
    [Range(0f, 3f)]
    public float lerpSpeed = 0.02f;

    [Header("Debug Values")]
    public float hSpeed;
    public float vSpeed;
    public float hLook;
    public CharacterController controller;
    public GameObject boom;
    public Vector3 cameraStart;
    public Vector3 startOffset;
    public Vector3 moveDir;
    public Vector3 camPos;
    public float hMod = 0f;
    public float vMod = 0f;

    // Use this for initialization
    void Start () {
        //Grab controller
        controller = GetComponent<CharacterController>();
        boom = GameObject.FindWithTag("CameraBoom");
        startOffset = Camera.main.transform.localPosition;
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
            RotateCamera();
        }

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
        boom.transform.Rotate(0, hLook * lookSpeed, 0);
    }
}
