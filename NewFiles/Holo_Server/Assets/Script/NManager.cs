using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
public class NManager : NetworkManager
{
    public List<NetworkConnection> totalList;
    public List<NetworkConnection> lensList;
    public List<NetworkConnection> controllerList;

    private int currentTurnLensIdx;

    void Start()
    {
        StartServer();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        totalList = new List<NetworkConnection>();
        lensList = new List<NetworkConnection>();
        controllerList = new List<NetworkConnection>();

        NetworkServer.RegisterHandler(CustomMsgType.Receive_Connect, OnCustomMsgHandler);
        NetworkServer.RegisterHandler(CustomMsgType.Receive_ControllerMove, OnCustomMsgHandler);
        NetworkServer.RegisterHandler(CustomMsgType.Send_TurnEnd, OnCustomMsgHandler);
        NetworkServer.RegisterHandler(CustomMsgType.Receive_MultiGameStartBtnEvent, OnCustomMsgHandler);
        NetworkServer.RegisterHandler(CustomMsgType.Receive_SingleGameStartBtnEvent, OnCustomMsgHandler);
        NetworkServer.RegisterHandler(CustomMsgType.Send_ReturnToMain, OnCustomMsgHandler);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {//클라 접속
        base.OnServerConnect(conn);

        totalList.Add(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        totalList.Remove(conn);

        if (lensList.Exists(x => x == conn))
            lensList.Remove(conn);
        else if (controllerList.Exists(x => x == conn))
            controllerList.Remove(conn);
    }

    private void OnCustomMsgHandler(NetworkMessage msg)
    {//서버가 받는 메세지들
        Debug.Log(msg.ToString() + " : ");
        if (msg.msgType == CustomMsgType.Receive_Connect)
        {
            Receive_Connect_Message message = msg.ReadMessage<Receive_Connect_Message>();
            if (message.id == 1)
            {
                lensList.Add(totalList[totalList.Count - 1]);
                Send_LensIdx_Message tmpMsg = new Send_LensIdx_Message()
                {
                    idx = lensList.Count - 1
                };
                lensList[lensList.Count - 1].Send(CustomMsgType.Send_LensIdx, tmpMsg);

                Debug.Log("Lens Idx : " + (lensList.Count - 1).ToString() + "  UniqueId : " + (totalList.Count - 1).ToString());

                if (lensList.Count == 2)
                {
                    Debug.Log("Lens Count is 2. Multi Ready");
                    for (int i = 0; i < controllerList.Count; i++)
                        controllerList[i].Send(CustomMsgType.Send_TwoClientReady, new Send_TwoClientReady_Message()
                        {
                            firstClientId = lensList[0].address + " : " + lensList[0].connectionId,
                            secondClientId = lensList[1].address + " : " + lensList[1].connectionId
                        });
                }
            }
            else if (message.id == 2)
            {
                controllerList.Add(totalList[totalList.Count - 1]);
                Debug.Log("Controller connectionId : " + (totalList.Count - 1).ToString());
            }
        }
        else if (msg.msgType == CustomMsgType.Receive_ControllerMove)
        {
            Receive_ControllerMove_Message message = msg.ReadMessage<Receive_ControllerMove_Message>();
            Debug.Log("Move Target :  " + message.targetIdx.ToString() + "  Input : " + message.input.ToString());
            for (int i = 0; i < lensList.Count; i++)
            {
                lensList[i].Send(CustomMsgType.Receive_ControllerMove, new Receive_ControllerMove_Message()
                {
                    targetIdx = message.targetIdx,
                    input = message.input
                });
            }
        }
        else if (msg.msgType == CustomMsgType.Send_TurnEnd)
        {
            if (currentTurnLensIdx == 0)
                currentTurnLensIdx = 1;
            else
                currentTurnLensIdx = 0;
            for (int i = 0; i < totalList.Count; i++)
            {
                totalList[i].Send(CustomMsgType.Send_SetTurnIdx, new Send_SetTurnIdx_Message()
                {
                    targetIdx = currentTurnLensIdx
                });
            }
        }
        else if (msg.msgType == CustomMsgType.Receive_MultiGameStartBtnEvent)
        {
            currentTurnLensIdx = 0;
            for (int i = 0; i < totalList.Count; i++)
            {
                totalList[i].Send(CustomMsgType.Receive_MultiGameStartBtnEvent, new EmptyMessage());
                totalList[i].Send(CustomMsgType.Send_GameStart, new EmptyMessage());
                totalList[i].Send(CustomMsgType.Send_SetTurnIdx, new Send_SetTurnIdx_Message()
                {
                    targetIdx = currentTurnLensIdx
                });
            }
        }
        else if (msg.msgType == CustomMsgType.Receive_SingleGameStartBtnEvent)
        {
            currentTurnLensIdx = 0;
            for (int i = 0; i < totalList.Count; i++)
            {
                totalList[i].Send(CustomMsgType.Receive_SingleGameStartBtnEvent, new EmptyMessage());
                totalList[i].Send(CustomMsgType.Send_GameStart, new EmptyMessage());
                totalList[i].Send(CustomMsgType.Send_SetTurnIdx, new Send_SetTurnIdx_Message()
                {
                    targetIdx = currentTurnLensIdx
                });
            }
        }
        else if (msg.msgType == CustomMsgType.Send_ReturnToMain)
        {
            for (int i = 0; i < totalList.Count; i++)
            {
                totalList[i].Send(CustomMsgType.Send_ReturnToMain, new EmptyMessage());
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentTurnLensIdx = 0;
            for (int i = 0; i < totalList.Count; i++)
            {
                totalList[i].Send(CustomMsgType.Receive_SingleGameStartBtnEvent, new EmptyMessage());
                totalList[i].Send(CustomMsgType.Send_GameStart, new EmptyMessage());
                totalList[i].Send(CustomMsgType.Send_SetTurnIdx, new Send_SetTurnIdx_Message()
                {
                    targetIdx = currentTurnLensIdx
                });
            }
        }
    }
}
