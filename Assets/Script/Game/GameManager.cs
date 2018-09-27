using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static Enums.Player currentTurnPlayer;

    [SerializeField]
    private Camera camera_Player1;
    [SerializeField]
    private Camera camera_Player2;

    void Start()
    {
        currentTurnPlayer = Enums.Player.Player1;
        camera_Player1.depth = 1;
        camera_Player2.depth = 0;
    }

    public void TurnChange()
    {
        if (currentTurnPlayer == Enums.Player.Player1)
        {
            currentTurnPlayer = Enums.Player.Player2;
            camera_Player1.depth = 0;
            camera_Player2.depth = 1;
        }
        else
        {
            currentTurnPlayer = Enums.Player.Player1;
            camera_Player1.depth = 1;
            camera_Player2.depth = 0;
        }
    }
}
