using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {

    public float checkDeg;
	// Update is called once per frame
	void Update () {
        
        Vector3 cameraVec = Camera.main.transform.forward;
        var cameraRotation = Camera.main.transform.eulerAngles;
        Vector3 localVec = transform.forward;
        Vector3 v = cameraVec - localVec;
        Debug.Log(Mathf.Acos(Vector3.Dot(cameraVec, localVec))*Mathf.Rad2Deg);
        if (Mathf.Acos(Vector3.Dot(cameraVec, localVec)) * Mathf.Rad2Deg > checkDeg)
        {
           transform.eulerAngles = cameraRotation;
        }
	}
}
