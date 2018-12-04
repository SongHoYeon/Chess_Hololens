using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
public class NManager : NetworkManager
{
    [SerializeField]
    private InputManager_S inputComp;
    [SerializeField]
    private cgChessBoardScript boardComp;

    public NetworkConnection serverConnection;
    public int myLensIdx;

    void Start()
    {
        StartClient();
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);

        client.RegisterHandler(CustomMsgType.Send_LensIdx, OnCustomMsgHandler);
        client.RegisterHandler(CustomMsgType.Receive_ControllerMove, OnCustomMsgHandler);
        client.RegisterHandler(CustomMsgType.Send_GameStart, OnCustomMsgHandler);
        client.RegisterHandler(CustomMsgType.Send_SetTurnIdx, OnCustomMsgHandler);
        client.RegisterHandler(CustomMsgType.Receive_MultiGameStartBtnEvent, OnCustomMsgHandler);
        client.RegisterHandler(CustomMsgType.Receive_SingleGameStartBtnEvent, OnCustomMsgHandler);
        client.RegisterHandler(CustomMsgType.Send_ReturnToMain, OnCustomMsgHandler);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {// 서버에 접속
        base.OnClientConnect(conn);

        serverConnection = conn;

        serverConnection.Send(CustomMsgType.Receive_Connect, new Receive_Connect_Message()
        {
            id = 1
    });
    }

    private void Update()
    {
        //Debug.Log("Is Host" + )
    }
    private void OnCustomMsgHandler(NetworkMessage msg)
    {//서버가 받는 메세지들
        Debug.Log(msg.ToString() + " : ");

        if (msg.msgType == CustomMsgType.Send_LensIdx)
        {
            Send_LensIdx_Message message = msg.ReadMessage<Send_LensIdx_Message>();
            Debug.Log("MyLensIdx :  " + message.idx.ToString());
            myLensIdx = message.idx;

            inputComp.Init(myLensIdx);
        }
        else if (msg.msgType == CustomMsgType.Receive_ControllerMove)
        {
            Receive_ControllerMove_Message message = msg.ReadMessage<Receive_ControllerMove_Message>();

            inputComp.ControlInput(message.input);
            if (message.targetIdx == myLensIdx)
            {
                Debug.Log("MyTern - Move Target :  " + message.targetIdx.ToString() + "  Input : " + message.input.ToString());
            }
            else
            {
                Debug.Log("OtherTern - Move Target :  " + message.targetIdx.ToString() + "  Input : " + message.input.ToString());
            }

            // TODO : Move
        }
        else if (msg.msgType == CustomMsgType.Send_GameStart)
        {
            inputComp.isGameStart = true;
            boardComp.Init();
        }
        else if (msg.msgType == CustomMsgType.Send_SetTurnIdx)
        {
            // TODO : MyTurn Check
            Send_SetTurnIdx_Message message = msg.ReadMessage<Send_SetTurnIdx_Message>();

            if (message.targetIdx == myLensIdx)
            {
                Debug.Log("Setted My Turn");
            }
            else
            {
                Debug.Log("Setted Other Turn");
            }
            inputComp.TurnChangeEvent(message.targetIdx);
        }
        else if (msg.msgType == CustomMsgType.Receive_MultiGameStartBtnEvent)
        {
            boardComp.Mode = cgChessBoardScript.BoardMode.PlayerVsPlayer;
            inputComp.isMultiGame = true;
        }
        else if (msg.msgType == CustomMsgType.Receive_SingleGameStartBtnEvent)
        {
            boardComp.Mode = cgChessBoardScript.BoardMode.PlayerVsEngine;
        }
        else if (msg.msgType == CustomMsgType.Send_ReturnToMain)
        {
            boardComp.ResetBoard();
            boardComp.GetComponent<Animator>().SetTrigger("BackStart");
        }
    }
}
