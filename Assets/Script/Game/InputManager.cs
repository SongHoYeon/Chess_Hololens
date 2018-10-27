using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject player1SelectCursorObj;
    [SerializeField]
    private GameObject player2SelectCursorObj;

    [SerializeField]
    private GameObject moveDirectionViewerObj;

    private Enums.Player player;
    private int currentTargetIdx;
    private GameObject player1SelectCursor;
    private GameObject player2SelectCursor;
    private GameObject currentCursor;
    private List<GameObject> moveDirectionViewerList;

    private void Start()
    {
        moveDirectionViewerList = new List<GameObject>();

        player1SelectCursor = GameObject.Instantiate(player1SelectCursorObj);
        player2SelectCursor = GameObject.Instantiate(player2SelectCursorObj);
        player1SelectCursor.SetActive(false);
        player2SelectCursor.SetActive(false);
        currentCursor = player1SelectCursor;
    }

    public void TurnChange(Enums.Player to)
    {
        currentCursor.SetActive(false);
        if (to == Enums.Player.Player1)
            currentCursor = player1SelectCursor;
        else if (to == Enums.Player.Player2)
            currentCursor = player2SelectCursor;
        currentCursor.SetActive(true);

        currentTargetIdx = 0;

        TargettingEffect();
    }

    void Update()
    {
        if (!GameManager.isGameStart)
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentTargetIdx--;
            if (currentTargetIdx < 0)
                currentTargetIdx = GameManager.currentTurnPieceList.Count - 1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentTargetIdx++;
            if (currentTargetIdx > GameManager.currentTurnPieceList.Count - 1)
                currentTargetIdx = 0;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            BoardPoint currentPoint = GameManager.currentTurnPieceList[currentTargetIdx].GetPoint();
            
            if (GameManager.currentTurnPlayer == Enums.Player.Player1)
            {
                if (currentPoint.GetYPos() > 0)
                {
                    GameManager.currentTurnPieceList[currentTargetIdx].SetMove(PointCreater.pointCompList[currentPoint.GetXPos(), currentPoint.GetYPos()-1], () => gameManager.TurnChange());
                }
            }
            else if (GameManager.currentTurnPlayer == Enums.Player.Player2)
            {
                if (currentPoint.GetYPos() < 9)
                {
                    GameManager.currentTurnPieceList[currentTargetIdx].SetMove(PointCreater.pointCompList[currentPoint.GetXPos(), currentPoint.GetYPos() + 1], () => gameManager.TurnChange());
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.TurnChange();
        }
        TargettingEffect();
    }

    private void TargettingEffect()
    {
        currentCursor.transform.parent = GameManager.currentTurnPieceList[currentTargetIdx].transform;
        currentCursor.transform.localPosition = Vector3.zero;

        //ShowMoveDirection();
    }

    private void ShowMoveDirection()
    {
        Piece currentTurnPiece = GameManager.currentTurnPieceList[currentTargetIdx];
        if (GameManager.currentTurnPlayer == Enums.Player.Player1)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (currentTurnPiece.GetPieceMovement().moveDirection[i, j] == 2)
                    {

                    }
                }
            }
            if (currentTurnPiece.GetPieceMovement().isMoveOnlyInField)
            {
                
            }
        }
        else if (GameManager.currentTurnPlayer == Enums.Player.Player2)
        {

        }
    }
}
