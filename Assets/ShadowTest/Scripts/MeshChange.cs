using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.VR;
using System.Collections.ObjectModel;
using UnityEngine.XR.WSA;
using HoloToolkit.Unity.SpatialMapping;
public class MeshChange : MonoBehaviour, IInputClickHandler {
    public int buttonNum;
    bool iswire = false;
    public Material shadowMaterial;
    public GameObject spatialMapping;
    public Material wireframeMaterial;
    ShowTriCount stc;
    void Start()
    {
        stc = GameObject.Find("TriText").GetComponent<ShowTriCount>();
    }
    void OnSelect()
    {
       
        switch(buttonNum)
        {
            case 0:
                iswire = !iswire;
                if (iswire)
                    SpatialMappingManager.Instance.SurfaceMaterial = wireframeMaterial;
                else
                    SpatialMappingManager.Instance.SurfaceMaterial = shadowMaterial;
                break;
            case 1:
                SpatialMappingManager.Instance.GetComponent<SpatialMappingObserver>().TrianglesPerCubicMeter += 100;
                stc.ChangeNum();
                break;
            case 2:
                SpatialMappingManager.Instance.GetComponent<SpatialMappingObserver>().TrianglesPerCubicMeter -= 100;
                stc.ChangeNum();
                break;
        }
    }
    public void OnInputClicked(InputClickedEventData eventData)
    {
        OnSelect();
    }
}
