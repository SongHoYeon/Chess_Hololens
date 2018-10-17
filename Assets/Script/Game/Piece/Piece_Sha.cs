using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_Sha : Piece
{
    public override void Init(BoardPoint point)
    {
        base.Init(point);

        currentPoint = point;
        type = Enums.PieceType.Sha;

        pieceMovement.isMoveOnlyInField = true;
        pieceMovement.mustJumpOtherPiece = false;
        pieceMovement.canUseDiagonal = true;
        pieceMovement.canMoveInfinite = false;

        pieceMovement.moveDirection = new int[,] {
            { 0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0},
            { 0,0,2,2,2,0,0},
            { 0,0,2,1,2,0,0},
            { 0,0,2,2,2,0,0},
            { 0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0},
        };

        transform.localPosition = point.transform.localPosition;
    }
}
