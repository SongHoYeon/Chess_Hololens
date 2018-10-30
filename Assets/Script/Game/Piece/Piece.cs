using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity;

public struct PieceMovement
{
    public bool isMoveOnlyInField;      //궁성안에서만 움직일수있는가.
    public bool mustJumpOtherPiece;     //꼭 다른 말은 넘어야하는가.
    public bool canUseDiagonal;         //궁성의 대각선을 이용할수있는가.
    public bool canMoveInfinite;        //어느 방향의 끝까지 이동할수있는가.

    public int[,] moveDirection;   //움직일수있는 방향 배열
}

public abstract class Piece : MonoBehaviour
{
    protected BoardPoint currentPoint;
    protected Enums.PieceType type;
    protected Enums.PieceSetting settingIdx;
    protected bool isAlive;
    protected PieceMovement pieceMovement;

    private bool moveFlag;
    private BoardPoint toMovePoint;
    private Vector3 toMovePos;
    private float moveTimer;
    private Action moveEndCallback;

    public virtual void Init(BoardPoint point, Enums.PieceSetting pieceSettingIdx)
    {
        isAlive = true;
        this.settingIdx = pieceSettingIdx;
        transform.localPosition = new Vector3(point.transform.localPosition.x, 0f, point.transform.localPosition.z);
        transform.localEulerAngles = new Vector3(-90f, 90f, 0f);
    }

    public BoardPoint GetPoint() { return currentPoint; }
    public PieceMovement GetPieceMovement() { return pieceMovement; }
    public Enums.PieceSetting GetPieceSettingIdx() { return settingIdx; }
    public bool GetIsAlive() { return isAlive; }

    public void SetMove(BoardPoint to, Action moveEndCallback)
    {
        WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\n\"{0}\"Move Start", name);
        //Debug.Log("start");
        toMovePoint = to;

        moveTimer = 0f;
        moveFlag = true;
        this.moveEndCallback = moveEndCallback;
    }
    private void Update()
    {
        if (moveFlag)
        {
            moveTimer += Time.deltaTime / 1f;
            transform.position = Vector3.Lerp(transform.position, toMovePoint.transform.position, moveTimer);
            if (Vector3.Distance(transform.localPosition, toMovePoint.transform.localPosition) < 0.1f)
            {
                //Debug.Log("End");
                WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\n\"{0}\"Move End", name);
                currentPoint = toMovePoint;
                moveFlag = false;
                if (moveEndCallback != null)
                    moveEndCallback();
            }
        }
    }
}
