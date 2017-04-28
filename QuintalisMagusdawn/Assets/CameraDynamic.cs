using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDynamic : MonoBehaviour {
    public float raycastDistance = 15f;
    public float spherecastDistance = 5f;
    public float sphereRadius = 2f;
    public float speedToFixClipping = 0.2f;
    public float smoothing;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = Camera.main.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        FixClippingThroughWalls();
	}

    void FixClippingThroughWalls()
    {
        RaycastHit hit;
        Vector3 direction = transform.parent.position - transform.position;
        Vector3 localPos = transform.localPosition;

        for (float i = offset.z; i <= 0f; i += speedToFixClipping)
        {
            Vector3 pos = transform.TransformPoint(new Vector3(localPos.x, localPos.y, i));

            if (Physics.Raycast(pos, direction, out hit, raycastDistance))
            {
                if (!hit.collider.CompareTag("Player"))
                {
                    continue;
                }
                if (!Physics.SphereCast(localPos, sphereRadius, Camera.main.transform.forward * -1, out hit, spherecastDistance))
                {
                    Debug.Log("Fixing");
                    localPos.z = i;
                    break;
                }
            }
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, localPos, smoothing * Time.deltaTime);
    }
}
