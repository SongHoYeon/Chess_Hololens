using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InputManager inputManager;

    public static Enums.Player currentTurnPlayer;

    [SerializeField]
    private Camera camera_Player1;
    [SerializeField]
    private Camera camera_Player2;

    public static List<Piece> currentTurnPieceList;
    public static bool isGameStart;

    void Start()
    {
        isGameStart = true;
        currentTurnPlayer = Enums.Player.Player1;
        currentTurnPieceList = new List<Piece>();

        camera_Player1.depth = 1;
        camera_Player2.depth = 0;
    }

    public void TurnChange()
    {
        currentTurnPieceList.Clear();

        int minXPos = 0;

        if (currentTurnPlayer == Enums.Player.Player1)
        {
            currentTurnPlayer = Enums.Player.Player2;
            camera_Player1.depth = 0;
            camera_Player2.depth = 1;

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
        else
        {
            currentTurnPlayer = Enums.Player.Player1;
            camera_Player1.depth = 1;
            camera_Player2.depth = 0;

            minXPos = 0;
            List<Piece> tmpList = new List<Piece>();

            foreach (Piece piece in PieceManager.player1Pieces.Values)
                tmpList.Add(piece);

            for (int i = 0; i < Defines.BoardProperty.COL_COUNT; i++)
            {
                List<Piece> foundPiece = tmpList.FindAll(x => x.GetPoint().GetXPos() == i && x.GetIsAlive());
                for (int j = 0; j < foundPiece.Count; j++)
                    currentTurnPieceList.Add(foundPiece[j]);
            }
        }

        inputManager.TurnChange(currentTurnPlayer);
    }
}
