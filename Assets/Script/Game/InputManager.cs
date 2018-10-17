using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
    [SerializeField]
    private GameManager gameManager;

    private Enums.Player player;
    private int currentTargetIdx;

    public void TurnChange(Enums.Player to)
    {
        currentTargetIdx = 0;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.TurnChange();
        }
    }

    private void TargettingEffect()
    {

    }
}
