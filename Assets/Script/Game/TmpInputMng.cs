using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TmpInputMng : MonoBehaviour, IInputClickHandler
{

    Rigidbody rb;
    public int cubeIdx;
    
    void Start () {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (GazeManager.Instance.HitObject == this.gameObject)
        {
            this.gameObject.transform.Rotate(0, 90, 0);
            if (cubeIdx == 0)
            {
                BoardPoint currentPoint = InputManager_S.instance.currentTurnPieceList[InputManager_S.instance.currentTargetIdx].GetPoint();

                if (currentPoint.GetYPos() > 0)
                {
                    CustomMessage.Instance.SendMoveTarget((int)CustomMessage.Instance.LocalPlayer, (int)InputManager_S.instance.currentTurnPieceList[InputManager_S.instance.currentTargetIdx].GetPieceSettingIdx(), currentPoint.GetYPos() - 1, currentPoint.GetXPos());
                    InputManager_S.instance.currentTurnPieceList[InputManager_S.instance.currentTargetIdx].SetMove(PointCreater.pointCompList[currentPoint.GetXPos(), currentPoint.GetYPos() - 1], () =>
                    {
                        CustomMessage.Instance.SendTurnChange((int)CustomMessage.Instance.LocalPlayer);
                    });
                }
            }
            else if (cubeIdx == 1)
            {
                InputManager_S.instance.currentTargetIdx--;
                if (InputManager_S.instance.currentTargetIdx < 0)
                    InputManager_S.instance.currentTargetIdx = InputManager_S.instance.currentTurnPieceList.Count - 1;
            }
            else if (cubeIdx == 2)
            {
                InputManager_S.instance.currentTargetIdx++;
                if (InputManager_S.instance.currentTargetIdx > InputManager_S.instance.currentTurnPieceList.Count - 1)
                    InputManager_S.instance.currentTargetIdx = 0;
            }
        }
    }
}
