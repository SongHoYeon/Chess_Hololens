using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
    [SerializeField]
    private GameManager gameManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.TurnChange();
        }
    }
}
