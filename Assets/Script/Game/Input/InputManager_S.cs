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
    private GameObject currentCursor;

    [SerializeField]
    private cgChessBoardScript boardComp;

    private List<cgChessPieceScript> currentPiecesList;

    private int lensIdx;
    private int currentTurnLensIdx;
    private int selectPieceIdx;


    public bool isGameStart = false;
    public bool isMultiGame = false;

    private bool isSelectLegalBlock = false;
    private List<cgSquareScript> highlightSqureList;
    private int highlightSqureIdx;

    private void Awake()
    {
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
        if (currentCursor != null)
        {
            Network.Destroy(currentCursor);
            //DestroyImmediate(currentCursor);
        }

        SetPieces();

        selectPieceIdx = 0;

        if (!isServer)
            return;
        if (turnLensIdx == 0)
        {
            currentCursor =  Network.Instantiate(player1SelectCursorObj, Vector3.zero, Quaternion.identity, 0) as GameObject;
            //currentCursor = Instantiate(player1SelectCursorObj);
        }
        else
        {
            currentCursor = Network.Instantiate(player1SelectCursorObj, Vector3.zero, Quaternion.identity, 0) as GameObject;
            //currentCursor = Instantiate(player2SelectCursorObj);
        }

        TargettingEffect(currentPiecesList[selectPieceIdx]);
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
                TargettingEffect(currentPiecesList[selectPieceIdx]);
            }
            else if (inputCode == 1)//Right
            {
                selectPieceIdx++;
                if (selectPieceIdx > currentPiecesList.Count - 1)
                    selectPieceIdx = 0;
                TargettingEffect(currentPiecesList[selectPieceIdx]);
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
                            DestroyImmediate(currentCursor);
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
                TargettingEffect(currentPiecesList[selectPieceIdx]);
            }
        }
    }

    public void SetPieces()
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
    }

    private void TargettingEffect(cgChessPieceScript target)
    {
        currentCursor.transform.parent = target.transform;
        currentCursor.transform.localPosition = Vector3.zero + new Vector3(0f, 0f, -1f);
    }

    private void TargettingEffect(cgSquareScript target)
    {
        currentCursor.transform.parent = target.transform;
        currentCursor.transform.localPosition = Vector3.zero + new Vector3(0f, 0f, -2f);
    }

    public void CheckMyTurn()
    {
        //currentCursor.SetActive(false);
        //if (GameManager.currentTurnPlayer == CustomMessage.Instance.LocalPlayer)
        //{
        //    isMyTurn = true;

        //    currentTargetIdx = 0;
        //    currentCursor = yourSelectCursor;
        //    currentCursor.SetActive(true);

        //    currentTurnPieceList.Clear();

        //    List<Piece> tmpList = new List<Piece>();

        //    foreach (Piece piece in PieceManager.myPieces.Values)
        //        tmpList.Add(piece);

        //    for (int i = 0; i < Defines.BoardProperty.COL_COUNT; i++)
        //    {
        //        List<Piece> foundPiece = tmpList.FindAll(x => x.GetPoint().GetXPos() == i && x.GetIsAlive());
        //        for (int j = 0; j < foundPiece.Count; j++)
        //            currentTurnPieceList.Add(foundPiece[j]);
        //    }
        //    WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\nMyTurnCheck. pieceCount : \"{0}\"", currentTurnPieceList.Count.ToString());
        //    TargettingEffect();
        //}
        //else
        //    isMyTurn = false;
    }

    //void Update()
    //{
    //    if (!GameManager.isGameStart)
    //        return;
    //    if (!isMyTurn)
    //        return;

    //    if (Input.GetKeyDown(KeyCode.LeftArrow) || (globalParameter.LeftThumbstickX <= -1 && !flag1))
    //    {
    //        flag1 = true;
    //        currentTargetIdx--;
    //        if (currentTargetIdx < 0)
    //            currentTargetIdx = currentTurnPieceList.Count - 1;
    //    }

    //    if (Input.GetKeyDown(KeyCode.RightArrow) || (globalParameter.LeftThumbstickX >= 1 && !flag2))
    //    {
    //        flag2 = true;
    //        currentTargetIdx++;
    //        if (currentTargetIdx > currentTurnPieceList.Count - 1)
    //            currentTargetIdx = 0;
    //    }
    //    if (Input.GetKeyDown(KeyCode.A) || globalParameter.Press_ButttonA == 1)
    //    {
    //        BoardPoint currentPoint = currentTurnPieceList[currentTargetIdx].GetPoint();
    //        WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\nMoveTargetName : \"{0}\"", currentTurnPieceList[currentTargetIdx].name);
    //        if (currentPoint.GetYPos() > 0)
    //        {
    //            CustomMessage.Instance.SendMoveTarget((int)CustomMessage\.Instance.LocalPlayer, (int)currentTurnPieceList[currentTargetIdx].GetPieceSettingIdx(), currentPoint.GetYPos() - 1, currentPoint.GetXPos());
    //            currentTurnPieceList[currentTargetIdx].SetMove(PointCreater.pointCompList[currentPoint.GetXPos(), currentPoint.GetYPos() - 1], () =>
    //            {
    //                CheckMyTurn();
    //            });
    //        }
    //    }

    //    TargettingEffect();
    //}
}
