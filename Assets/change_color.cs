using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
public class change_color : MonoBehaviour,IInputClickHandler {

    Rigidbody rb;
    public Material mat;
    //Renderer renderer = th
	// Use this for initialization
	void Start () {
        rb = this.gameObject.GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {


    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(GazeManager.Instance.HitObject==this.gameObject)
        {
            this.gameObject.transform.Rotate(0, 90, 0);

            funcRigidbody();
        }
  
    }

    public void funcRigidbody()
    {
        mat.color = Color.yellow;

       /* Debug.Log("1");
        rb.useGravity = !rb.useGravity;*/

        
    }
}
