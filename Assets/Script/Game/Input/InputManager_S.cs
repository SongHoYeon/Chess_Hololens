using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.Windows.Speech;
using System.Linq;

public class InputManager_S : MonoBehaviour
{
    public static InputManager_S _instance = null;
    public static InputManager_S instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("InputManager Inst is NULL");
            return _instance;
        }
    }

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
    private GameObject mySelectCursor;
    private GameObject yourSelectCursor;
    private GameObject currentCursor;
    private List<GameObject> moveDirectionViewerList;
    private bool isMyTurn;

    [HideInInspector]
    public List<Piece> currentTurnPieceList;
    [HideInInspector]
    public int currentTargetIdx;

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    private void OnEnable()
    {
        _instance = this;

        currentTurnPieceList = new List<Piece>();
        moveDirectionViewerList = new List<GameObject>();

        isMyTurn = false;
        mySelectCursor = GameObject.Instantiate(player1SelectCursorObj);
        yourSelectCursor = GameObject.Instantiate(player2SelectCursorObj);
        mySelectCursor.SetActive(false);
        yourSelectCursor.SetActive(false);
        currentCursor = mySelectCursor;
    }

    public void CheckMyTurn()
    {
        currentCursor.SetActive(false);
        if (GameManager.currentTurnPlayer == CustomMessage.Instance.LocalPlayer)
        {
            isMyTurn = true;

            currentTargetIdx = 0;
            currentCursor = mySelectCursor;
            currentCursor.SetActive(true);

            currentTurnPieceList.Clear();

            List<Piece> tmpList = new List<Piece>();

            foreach (Piece piece in PieceManager.myPieces.Values)
                tmpList.Add(piece);

            for (int i = 0; i < Defines.BoardProperty.COL_COUNT; i++)
            {
                List<Piece> foundPiece = tmpList.FindAll(x => x.GetPoint().GetXPos() == i && x.GetIsAlive());
                for (int j = 0; j < foundPiece.Count; j++)
                    currentTurnPieceList.Add(foundPiece[j]);
            }
            TargettingEffect();
        }
        else
            isMyTurn = false;
    }

    void Update()
    {
        #region Controller Setting
        if (globalParameter.LeftThumbstickX > -1 && flag1)
        { flag1 = false; }
        if (globalParameter.LeftThumbstickX < 1 && flag2)
        { flag2 = false; }

        #endregion
        if (Input.GetKeyDown(KeyCode.A))
        {
            BoardPoint currentPoint = PieceManager.myPieces[0].GetPoint();

            if (currentPoint.GetYPos() > 0)
            {
                PieceManager.myPieces[0].SetMove(PointCreater.pointCompList[currentPoint.GetXPos(), currentPoint.GetYPos() - 1], () =>
                {
                    //CustomMessage.Instance.SendTurnChange((int)CustomMessage.Instance.LocalPlayer);
                });
            }
        }
        if (!GameManager.isGameStart)
            return;
        if (!isMyTurn)
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || (globalParameter.LeftThumbstickX <= -1 && !flag1))
        {
            flag1 = true;
            currentTargetIdx--;
            if (currentTargetIdx < 0)
                currentTargetIdx = currentTurnPieceList.Count - 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || (globalParameter.LeftThumbstickX >= 1 && !flag2))
        {
            flag2 = true;
            currentTargetIdx++;
            if (currentTargetIdx > currentTurnPieceList.Count - 1)
                currentTargetIdx = 0;
        }
        if (Input.GetKeyDown(KeyCode.A) || globalParameter.Press_ButttonA == 1)
        {
            BoardPoint currentPoint = currentTurnPieceList[currentTargetIdx].GetPoint();

            if (currentPoint.GetYPos() > 0)
            {
                CustomMessage.Instance.SendMoveTarget((int)CustomMessage.Instance.LocalPlayer, (int)currentTurnPieceList[currentTargetIdx].GetPieceSettingIdx(), currentPoint.GetYPos() - 1, currentPoint.GetXPos());
                currentTurnPieceList[currentTargetIdx].SetMove(PointCreater.pointCompList[currentPoint.GetXPos(), currentPoint.GetYPos() - 1], () =>
                { });
            }
        }

        TargettingEffect();
    }

    private void TargettingEffect()
    {
        currentCursor.transform.parent = currentTurnPieceList[currentTargetIdx].transform;
        currentCursor.transform.localPosition = new Vector3(0f, 1f, 0f);

        //ShowMoveDirection();
    }

    private void ShowMoveDirection()
    {
        //Piece currentTurnPiece = GameManager.currentTurnPieceList[currentTargetIdx];
        //if (GameManager.currentTurnPlayer == Enums.Player.Player1)
        //{
        //    for (int i = 0; i < 7; i++)
        //    {
        //        for (int j = 0; j < 7; j++)
        //        {
        //            if (currentTurnPiece.GetPieceMovement().moveDirection[i, j] == 2)
        //            {

        //            }
        //        }
        //    }
        //    if (currentTurnPiece.GetPieceMovement().isMoveOnlyInField)
        //    {

        //    }
        //}
        //else if (GameManager.currentTurnPlayer == Enums.Player.Player2)
        //{

        //}
    }
}
