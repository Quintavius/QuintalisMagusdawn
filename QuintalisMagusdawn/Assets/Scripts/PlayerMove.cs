using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public float moveSpeed = 1;

	// Use this for initialization
	void Start () {
        CharacterController controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Horizontal") != 0)
        {

        }
        if (Input.GetAxis("Vertical") != 0)
        {

        }
	}
}
