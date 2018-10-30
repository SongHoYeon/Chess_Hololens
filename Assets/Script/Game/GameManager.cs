using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance = null;
    public static GameManager instance
    {
        get {
            if (_instance == null)
                Debug.Log("GameManager Inst is NULL");
            return _instance;
        }
    }
    [SerializeField]
    private InputManager_S inputManager;
    [SerializeField]
    private PointCreater pointCreater;
    [SerializeField]
    private PieceManager pieceManager;

    public static Enums.Player currentTurnPlayer;

    public static bool isGameStart;

    void OnEnable()
    {
        _instance = this;

        //WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\nGameManager Enable");
        pointCreater.CreatePoints();
        pieceManager.CreateMyPiece();
        isGameStart = false;
    }

    public void GameStart()
    {
        //WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\nGameManager GameStart");
        isGameStart = true;
        currentTurnPlayer = Enums.Player.Player1;
        InputManager_S.instance.CheckMyTurn();
    }

    public void TurnChange(int from)
    {
        if (from == (int)Enums.Player.Player1)
            currentTurnPlayer = Enums.Player.Player2;
        else
            currentTurnPlayer = Enums.Player.Player1;
        WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\nGameManager-Changing");

        InputManager_S.instance.CheckMyTurn();
    }
}
