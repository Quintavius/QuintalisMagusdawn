using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDynamic : MonoBehaviour {
    public float raycastDistance = 15f;
    public float spherecastDistance = 5f;
    public float sphereRadius = 2f;
    public float speedToFixClipping = 0.2f;
    public float smoothing;
    public float offset;
    public Vector3 pos;
    public Vector3 debugPos;
    public float moveVal;
    GameObject player;

    private bool col = false;

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        offset = Vector3.Distance(Camera.main.transform.position, player.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        //FixClippingThroughWalls();
        ClipFix();
	}
    void ClipFix()
    {
        RaycastHit camHit;

        Vector3 camPos = Camera.main.transform.position;
        Vector3 playerDir = Camera.main.transform.forward;
        Vector3 playerPos = player.transform.position;
        float camDist = Vector3.Distance(camPos, playerPos);

        if (Physics.Raycast(camPos, playerDir, out camHit, raycastDistance))
        {
            if (!camHit.collider.CompareTag("Player"))
            {
                Debug.Log("Something in front of player, move closer");
                Camera.main.transform.position = Vector3.MoveTowards(camPos, playerPos, smoothing);
            }
            else if (!col)
            {
                if (camDist <= offset)
                {
                    Debug.Log("Clear View, within range. Move back!");
                    Camera.main.transform.position = Vector3.MoveTowards(camPos, playerPos, smoothing * -1);
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        col = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        col = false;
    }

    void FixClippingThroughWalls()
    {
        /*offset = Camera.main.transform.localPosition;
        RaycastHit hit;
        Vector3 direction = Camera.main.transform.forward;//transform.parent.position - transform.position;
        Vector3 localPos = Camera.main.transform.localPosition;

        for (float i = offset.z; i <= 0f; i += speedToFixClipping)
        {
            Vector3 pos = transform.TransformPoint(new Vector3(localPos.x, localPos.y, i));

            if (Physics.Raycast(pos, direction, out hit, raycastDistance))
            {
                Debug.DrawRay(pos, direction, Color.green);
                if (!hit.collider.CompareTag("Player"))
                {
                    continue;
                }
                if (!Physics.SphereCast(pos, sphereRadius, this.transform.forward * -1, out hit, spherecastDistance))
                {
                    Debug.Log("Fixing");
                    debugPos = pos;
                    localPos.z = i;
                    //moveVal = i;
                    break;
                }
            }
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, localPos, smoothing * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(pos, transform.parent.position, moveVal);*/
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(debugPos, sphereRadius);
    }
}
