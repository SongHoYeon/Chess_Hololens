using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
public class NManager : NetworkManager
{
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

    private void OnCustomMsgHandler(NetworkMessage msg)
    {//서버가 받는 메세지들
        Debug.Log(msg.ToString() + " : ");

        if (msg.msgType == CustomMsgType.Send_LensIdx)
        {
            Send_LensIdx_Message message = msg.ReadMessage<Send_LensIdx_Message>();
            Debug.Log("MyLensIdx :  " + message.idx.ToString());
            myLensIdx = message.idx;
        }
        else if (msg.msgType == CustomMsgType.Receive_ControllerMove)
        {
            Receive_ControllerMove_Message message = msg.ReadMessage<Receive_ControllerMove_Message>();

            if (message.targetIdx == myLensIdx)
                Debug.Log("MyTern - Move Target :  " + message.targetIdx.ToString() + "  Input : " + message.input.ToString());
            else
                Debug.Log("OtherTern - Move Target :  " + message.targetIdx.ToString() + "  Input : " + message.input.ToString());
            
            // TODO : Move
        }
    }
}
