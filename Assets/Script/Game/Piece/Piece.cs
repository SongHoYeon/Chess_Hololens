using System.Collections.Generic;
using UnityEngine;

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

    protected PieceMovement pieceMovement;

    public abstract void Init(BoardPoint point);
}
