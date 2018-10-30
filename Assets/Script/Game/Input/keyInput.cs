using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLensXboxController;
using HoloToolkit.Unity.SpatialMapping;
using System;

public class keyInput : MonoBehaviour
{
#if !UNITY_EDITOR
    ControllerInput controller;
#endif
    //public member property
    public Transform dron;


 
    
    void Start()
    {
#if !UNITY_EDITOR
        controller = new ControllerInput(0, 0.19f);
#endif
        

        if (dron == null)
        {
            dron = GameObject.FindWithTag(Tags.Drone).GetComponent<Transform>();
        }

    }


    void Update()
    {

        //float L_trigger;
        //float R_trigger;

        float leftStickX;
        float leftStickY;

        //float rightStickX;
        //float rightStickY;
        globalParameter.Press_ButttonA = 0;
        

        //float crossButtonX;


#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.JoystickButton0)) // A 버튼 
        {

           
            globalParameter.Press_ButttonA = 1;
            Debug.Log("A");
          
        }
        
       
        leftStickX = Input.GetAxisRaw("CONTROLLER_LEFT_STICK_HORIZONTAL");
        leftStickY = Input.GetAxisRaw("CONTROLLER_LEFT_STICK_VERTICAL");

        globalParameter.LeftThumbstickX = leftStickX;
       
#endif



#if !UNITY_EDITOR

        if (controller == null)
        {
            return;
        }

        try
        {
            controller.Update();
        }
        catch (Exception e)
        {
            return;
        }

        if (controller.GetButtonUp(ControllerButton.A))
        {
          globalParameter.Press_ButttonA = 1;
                
        }

        

        leftStickX = controller.GetAxisLeftThumbstickX();
        leftStickY = controller.GetAxisLeftThumbstickY();
        globalParameter.LeftThumbstickX = leftStickX;
        


#endif

    }
}
