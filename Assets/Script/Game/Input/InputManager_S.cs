using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using HoloToolkit.Unity;
using UnityEngine.Networking;

public class InputManager_S : NetworkBehaviour
{
    [SerializeField]
    private PhotonServer nMngComp;
    [SerializeField]
    private GameObject player1SelectCursorObj;
    [SerializeField]
    private GameObject player2SelectCursorObj;

    [SerializeField]
    private cgChessBoardScript boardComp;

    private List<cgChessPieceScript> currentPiecesList;

    private Vector3 CursorDestoryPos;
    private int lensIdx;
    private int currentTurnLensIdx;
    public int selectPieceIdx;
    public Vector3 curserPos;
    private GameObject currentCursor;

    public bool isGameStart = false;
    public bool isMultiGame = false;

    private bool isSelectLegalBlock = false;
    private List<cgSquareScript> highlightSqureList;
    private int highlightSqureIdx;

    private void Awake()
    {
        CursorDestoryPos = new Vector3(-10000f, 0f, 0f);
        isGameStart = false;
        isMultiGame = false;
        isSelectLegalBlock = false;

        currentPiecesList = new List<cgChessPieceScript>();
    }

    public void Init(int lensIdx)
    {
        this.lensIdx = lensIdx;
    }

    public void TurnChangeEvent(int turnLensIdx)
    {
        isSelectLegalBlock = false;
        this.currentTurnLensIdx = turnLensIdx;
        if (!isServer)
            return;
        if (currentCursor != null)
            currentCursor.transform.localPosition = CursorDestoryPos;

        SetPieces();

        selectPieceIdx = 0;

        if (currentTurnLensIdx == 0)
            currentCursor = player1SelectCursorObj;
        else
            currentCursor = player2SelectCursorObj;

        TargettingEffect(currentPiecesList[selectPieceIdx].square);
    }

    public void ControlInput(int inputCode)
    {
        if (!isServer)
            return;
        if (!isSelectLegalBlock)
        {
            if (inputCode == 0)//Left
            {
                selectPieceIdx--;
                if (selectPieceIdx < 0)
                    selectPieceIdx = currentPiecesList.Count - 1;
                TargettingEffect(currentPiecesList[selectPieceIdx].square);
            }
            else if (inputCode == 1)//Right
            {
                selectPieceIdx++;
                if (selectPieceIdx > currentPiecesList.Count - 1)
                    selectPieceIdx = 0;
                TargettingEffect(currentPiecesList[selectPieceIdx].square);
            }
            else if (inputCode == 2)//confirm
            {
                highlightSqureList = boardComp._pieceDown_(currentPiecesList[selectPieceIdx]);
                if (highlightSqureList.Count > 0)
                {
                    isSelectLegalBlock = true;
                    highlightSqureIdx = 0;
                    TargettingEffect(highlightSqureList[highlightSqureIdx]);
                }
            }
            else if (inputCode == 3)//cancel
            {

            }
        }
        else
        {
            if (inputCode == 0)//Left
            {
                highlightSqureIdx--;
                if (highlightSqureIdx < 0)
                    highlightSqureIdx = highlightSqureList.Count - 1;
                TargettingEffect(highlightSqureList[highlightSqureIdx]);
            }
            else if (inputCode == 1)//Right
            {
                highlightSqureIdx++;
                if (highlightSqureIdx > highlightSqureList.Count - 1)
                    highlightSqureIdx = 0;
                TargettingEffect(highlightSqureList[highlightSqureIdx]);
            }
            else if (inputCode == 2)//confirm
            {
                boardComp._pieceUp(currentPiecesList[selectPieceIdx], highlightSqureList[highlightSqureIdx], () => {
                    if (isMultiGame && currentTurnLensIdx == lensIdx)
                        nMngComp.pNetObj.TurnEndSend();

                    if (!isMultiGame)
                    {
                        if (currentCursor != null)
                            currentCursor.transform.localPosition = CursorDestoryPos;
                    }
                });
            }
            else if (inputCode == 3)//cancel
            {
                boardComp._downPiece = null;

                if (boardComp.highlightLastMove)
                {//revert colors if highlighting is active
                    foreach (cgSquareScript square in boardComp.getSquares()) square.changeColor(square.startColor);
                }
                isSelectLegalBlock = false;
                TargettingEffect(currentPiecesList[selectPieceIdx].square);
            }
        }
    }

    public void SetPieces(bool isDeadCall = false)
    {
        currentPiecesList.Clear();
        for (int i = 0; i < boardComp._livePieces.Count; i++)
        {
            if (currentTurnLensIdx == 0)
            {
                if (boardComp._livePieces[i].white)
                    currentPiecesList.Add(boardComp._livePieces[i]);
            }
            else
            {
                if (!boardComp._livePieces[i].white)
                    currentPiecesList.Add(boardComp._livePieces[i]);
            }
        }

        if (!isDeadCall)
            return;
        if (!isServer)
            return;
        if (currentCursor != null)
            currentCursor.transform.localPosition = CursorDestoryPos;

        selectPieceIdx = 0;

        if (currentTurnLensIdx == 0)
            currentCursor = player1SelectCursorObj;
        else
            currentCursor = player2SelectCursorObj;

        TargettingEffect(currentPiecesList[selectPieceIdx].square);
    }

    private void TargettingEffect(cgSquareScript target)
    {
        if (isServer)
            currentCursor.transform.localPosition = target.transform.localPosition + new Vector3(0f, 0f, -0.15f);
    }
}
