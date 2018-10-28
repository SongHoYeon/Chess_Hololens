using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TmpInputMng : MonoBehaviour {

    Rigidbody rb;
    
    void Start () {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (GazeManager.Instance.HitObject == this.gameObject)
        {
            BoardPoint currentPoint = InputManager_S.instance.currentTurnPieceList[InputManager_S.instance.currentTargetIdx].GetPoint();

            if (currentPoint.GetYPos() > 0)
            {
                InputManager_S.instance.currentTurnPieceList[InputManager_S.instance.currentTargetIdx].SetMove(PointCreater.pointCompList[currentPoint.GetXPos(), currentPoint.GetYPos() - 1], () =>
                {
                    CustomMessage.Instance.SendTurnChange((int)CustomMessage.Instance.LocalPlayer);
                });
            }
        }
    }
}
