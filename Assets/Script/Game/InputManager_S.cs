using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class InputManager_S : MonoBehaviour
{
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    bool flag1 = false;
    bool flag2 = false;

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
        Debug.Log("a");
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

        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);


        if (!GameManager.isGameStart)
            return;

        if (state.ThumbSticks.Left.X > -1 && flag1)
        { flag1 = false; }
        if (state.ThumbSticks.Left.X < 1 && flag2)
        { flag2 = false; }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || state.ThumbSticks.Left.X <= -1 && !flag1)
        {
            flag1 = true;
            currentTargetIdx--;
            if (currentTargetIdx < 0)
                currentTargetIdx = GameManager.currentTurnPieceList.Count - 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || state.ThumbSticks.Left.X >= 1 && !flag2)
        {
            flag2 = true;
            currentTargetIdx++;
            if (currentTargetIdx > GameManager.currentTurnPieceList.Count - 1)
                currentTargetIdx = 0;
        }
        if (Input.GetKeyDown(KeyCode.A) || prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            BoardPoint currentPoint = GameManager.currentTurnPieceList[currentTargetIdx].GetPoint();

            if (GameManager.currentTurnPlayer == Enums.Player.Player1)
            {
                if (currentPoint.GetYPos() > 0)
                {
                    GameManager.currentTurnPieceList[currentTargetIdx].SetMove(PointCreater.pointCompList[currentPoint.GetXPos(), currentPoint.GetYPos() - 1], () => gameManager.TurnChange());
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
