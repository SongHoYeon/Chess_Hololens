using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

public class ShowTriCount : MonoBehaviour {

    TextMesh textMesh;
	// Use this for initialization
	void Start () {
        textMesh = this.GetComponent<TextMesh>();
        textMesh.text = SpatialMappingManager.Instance.GetComponent<SpatialMappingObserver>().TrianglesPerCubicMeter.ToString();
	}
    public void ChangeNum()
    {
        textMesh.text = SpatialMappingManager.Instance.GetComponent<SpatialMappingObserver>().TrianglesPerCubicMeter.ToString();
    }

}
