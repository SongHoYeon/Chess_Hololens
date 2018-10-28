using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HoloToolkit.Sharing.Tests;
using HoloToolkit.Sharing;
using SpectatorView;
using UnityEngine.UI;
using HoloToolkit.Unity;
using System;
using HoloToolkit.Unity.InputModule.Tests;

public class communicator : MonoBehaviour
{    
    public Transform playerP;
    public Transform DroneP;

    public Vector3 calibP;

    public bool isSpectatorView;
    
    public bool invX = false;
    public bool invY = false;
    public bool invZ = false;

    private Vector3 prevDroneP = Vector3.zero;
    private Quaternion prevDroneR = Quaternion.identity;

    int inv_x = 1;
    int inv_y = 1;
    int inv_z = 1;

    void Start ()
    {
        if(DroneP != null)
        {
            CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.HeadTransform] = this.receiveHeadPos;
            CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.DroneTransform] = this.receiveDronePos;
        }       
    }
	
    void receiveHeadPos(NetworkInMessage msg)
    {
        long id = msg.ReadInt64();

        Vector3 p = CustomMessages.Instance.ReadVector3(msg);
        Quaternion r = CustomMessages.Instance.ReadQuaternion(msg);

     //   playerP.localPosition = new Vector3(p.x * inv_x + calibP.x, p.y * inv_y + calibP.y, p.z * inv_z + calibP.z);
     //   playerP.localRotation = r;

        playerP.position = new Vector3(p.x * inv_x + calibP.x, p.y * inv_y + calibP.y, p.z * inv_z + calibP.z);
        playerP.rotation = r;
    }

    void receiveDronePos(NetworkInMessage msg)
    {
        long id = msg.ReadInt64();

        Vector3 p = CustomMessages.Instance.ReadVector3(msg);
        Quaternion r = CustomMessages.Instance.ReadQuaternion(msg);

        //DroneP.localPosition = new Vector3(p.x, p.y, p.z);
        //DroneP.localRotation = r;

     //   DroneP.position = new Vector3(p.x * inv_x + calibP.x, p.y * inv_y + calibP.y, p.z * inv_z + calibP.z);
     //   DroneP.rotation = r;

        DroneP.localPosition = new Vector3(p.x * inv_x + calibP.x, p.y * inv_y + calibP.y, p.z * inv_z + calibP.z);
        DroneP.localRotation = r;

    //    Debug.Log("디버그" + DroneP.localPosition);
    }

    void reciveTarget(NetworkInMessage msg)
    {
        long id = msg.ReadInt64();

        int targetSwitch = CustomMessages.Instance.ReadInt(msg);

     //   globalParameter.trackingObjSwitch = targetSwitch;

     //   Debug.Log("targetSwitch : " + targetSwitch);
    }
        
    /*
     * 
     * 
     * 
     * 
     */
     
    void sendHeadPos()
    {
        Transform headTransform = Camera.main.transform;

//        Vector3 headPosition = this.transform.InverseTransformPoint(headTransform.position);
//        Quaternion headRotation = Quaternion.Inverse(this.transform.rotation) * headTransform.rotation;

        Vector3 headPosition = headTransform.position;
        Quaternion headRotation = headTransform.rotation;

        CustomMessages.Instance.SendHeadTransform(headPosition, headRotation);
    }

    void sendDronePos()
    {
        Transform droneTransform = DroneP.transform;

        bool compP = false;
        bool compR = false;

        Vector3 dronePosition = this.transform.InverseTransformPoint(droneTransform.position);
        Quaternion droneRotation = Quaternion.Inverse(this.transform.rotation) * droneTransform.rotation;

   //     Vector3 dronePosition = droneTransform.position;
   //     Quaternion droneRotation = droneTransform.rotation;

        if (prevDroneP == dronePosition)
        {
            compP = true;
        }

        if(prevDroneR == droneRotation)
        {
            compR = true;
        }

        if(compR == false || compP == false)
        {
            CustomMessages.Instance.SendDroneTransform(dronePosition, droneRotation);

            prevDroneP = dronePosition;
            prevDroneR = droneRotation;
        }            
    }

    void Update ()
    {
        if(invX == true)
        {
            inv_x = -1;
        }
        else
        {
            inv_x = 1;
        }

        if(invY == true)
        {
            inv_y = -1;
        }
        else
        {
            inv_y = 1;
        }

        if(invZ == true)
        {
            inv_z = -1;
        }
        else
        {
            inv_z = 1;
        }        

        if (isSpectatorView == false)
        {
            if (DroneP != null)
            {
                sendDronePos();
            }

            if (playerP != null)
            {
                sendHeadPos();
            }
        }

    //    gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
	}

   public void OnClickX()
    {
        inv_x *= -1;
    }

    public void OnClickY()
    {
        inv_y *= -1;
    }

    public void OnClickZ()
    {
        inv_z *= -1;
    }

    public void OnClickRS()
    {
        inv_x = 1;
        inv_y = 1;
        inv_z = 1;
    }
}
