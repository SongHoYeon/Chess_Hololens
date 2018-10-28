using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InputManager_S inputManager;

    public static Enums.Player currentTurnPlayer;

    public static List<Piece> currentTurnPieceList;
    public static bool isGameStart;

    void Start()
    {
        currentTurnPieceList = new List<Piece>();

        TurnChange();
    }

    public void TurnChange()
    {
        currentTurnPieceList.Clear();

        int minXPos = 0;

        if (currentTurnPlayer == Enums.Player.Player2 || !isGameStart)
        {
            currentTurnPlayer = Enums.Player.Player1;
            minXPos = Defines.BoardProperty.COL_COUNT;
            List<Piece> tmpList = new List<Piece>();

            foreach (Piece piece in PieceManager.player1Pieces.Values)
                tmpList.Add(piece);

            for (int i = 0; i < Defines.BoardProperty.COL_COUNT; i++)
            {
                List<Piece> foundPiece = tmpList.FindAll(x => x.GetPoint().GetXPos() == i && x.GetIsAlive());
                for (int j = 0; j < foundPiece.Count; j++)
                    currentTurnPieceList.Add(foundPiece[j]);
            }
            
            isGameStart = true;
        }

        else if (currentTurnPlayer == Enums.Player.Player1)
        {
            currentTurnPlayer = Enums.Player.Player2;

            minXPos = Defines.BoardProperty.COL_COUNT;
            List<Piece> tmpList = new List<Piece>();

            foreach (Piece piece in PieceManager.player2Pieces.Values)
                tmpList.Add(piece);

            for (int i = minXPos - 1; i >= 0; i--)
            {
                List<Piece> foundPiece = tmpList.FindAll(x => x.GetPoint().GetXPos() == i && x.GetIsAlive());
                for (int j = 0; j < foundPiece.Count; j++)
                    currentTurnPieceList.Add(foundPiece[j]);
            }
        }

        inputManager.TurnChange(currentTurnPlayer);
    }
}
