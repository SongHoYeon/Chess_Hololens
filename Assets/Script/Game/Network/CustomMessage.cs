using System;
using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;
using HoloToolkit.Sharing;

public class CustomMessage : Singleton<CustomMessage>
{
    public enum MessageType : byte
    {
        MoveTarget = MessageID.UserMessageIDStart,
        CreatePhan,
        Max
    }

    public enum UserMessageChannels
    {
        Anchors = MessageChannel.UserMessageChannelStart
    }

    /// <summary>
    /// Cache the local user's ID to use when sending messages
    /// </summary>
    public long LocalUserID
    {
        get; set;
    }
    public Enums.Player LocalPlayer
    {
        get;set;
    }

    public delegate void MessageCallback(NetworkInMessage msg);
    private Dictionary<MessageType, MessageCallback> messageHandlers = new Dictionary<MessageType, MessageCallback>();
    public Dictionary<MessageType, MessageCallback> MessageHandlers
    {
        get
        {
            return messageHandlers;
        }
    }

    /// <summary>
    /// Helper object that we use to route incoming message callbacks to the member
    /// functions of this class
    /// </summary>
    private NetworkConnectionAdapter connectionAdapter;

    /// <summary>
    /// Cache the connection object for the sharing service
    /// </summary>
    private NetworkConnection serverConnection;

    public void SendMoveTarget(Vector3 position, Quaternion rotation)
    {
        // If we are connected to a session, broadcast our head info
        if (serverConnection != null && serverConnection.IsConnected())
        {
            // Create an outgoing network message to contain all the info we want to send
            NetworkOutMessage msg = CreateMessage((byte)MessageType.MoveTarget);

            AppendTransform(msg, position, rotation);

            // Send the message as a broadcast, which will cause the server to forward it to all other users in the session.
            serverConnection.Broadcast(
                msg,
                MessagePriority.Immediate,
                MessageReliability.ReliableSequenced,
                MessageChannel.Avatar);
        }
    }
    public void SendCreatePhan(Vector3 position)
    {
        // If we are connected to a session, broadcast our head info
        if (serverConnection != null && serverConnection.IsConnected())
        {
            // Create an outgoing network message to contain all the info we want to send
            NetworkOutMessage msg = CreateMessage((byte)MessageType.CreatePhan);

            AppendVector3(msg, position);

            // Send the message as a broadcast, which will cause the server to forward it to all other users in the session.
            serverConnection.Broadcast(
                msg,
                MessagePriority.Immediate,
                MessageReliability.ReliableSequenced,
                MessageChannel.Avatar);
        }
    }
    private void Start()
    {
        // SharingStage should be valid at this point, but we may not be connected.
        if (SharingStage.Instance.IsConnected)
        {
            Connected();
        }
        else
        {
            SharingStage.Instance.SharingManagerConnected += Connected;
        }
    }

    private void Connected(object sender = null, EventArgs e = null)
    {
        SharingStage.Instance.SharingManagerConnected -= Connected;
        InitializeMessageHandlers();
    }
    bool logcheck = false;
    bool idAllocCheck = false;
    private void InitializeMessageHandlers()
    {
        SharingStage sharingStage = SharingStage.Instance;

        if (sharingStage == null)
        {
            Debug.Log("Cannot Initialize CustomMessages. No SharingStage instance found.");
            return;
        }

        serverConnection = sharingStage.Manager.GetServerConnection();
        if (serverConnection == null)
        {
            Debug.Log("Cannot initialize CustomMessages. Cannot get a server connection.");
            return;
        }

        connectionAdapter = new NetworkConnectionAdapter();
        connectionAdapter.MessageReceivedCallback += OnMessageReceived;
        idAllocCheck = true;
        // Cache the local user ID
        LocalUserID = SharingStage.Instance.Manager.GetLocalUser().GetID();
        LocalPlayer = SharingStage.Instance.CurrentRoom.GetUserCount() == 1 ? Enums.Player.Player1 : Enums.Player.Player2;

        
        for (byte index = (byte)MessageType.MoveTarget; index < (byte)MessageType.Max; index++)
        {
            if (MessageHandlers.ContainsKey((MessageType)index) == false)
            {
                MessageHandlers.Add((MessageType)index, null);
            }

            serverConnection.AddListener(index, connectionAdapter);
        }
    }
    private void Update()
    {
        if (!logcheck && idAllocCheck)
        {
            if (WorldAnchorManager.Instance != null && WorldAnchorManager.Instance.AnchorDebugText != null)
            {
                WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\nLocalUserId : %s", LocalUserID.ToString());
                WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\nLocalPlayer : %s", LocalPlayer.ToString());
                logcheck = true;
            }
        }
    }
    private NetworkOutMessage CreateMessage(byte messageType)
    {
        NetworkOutMessage msg = serverConnection.CreateMessage(messageType);
        msg.Write(messageType);
        // Add the local userID so that the remote clients know whose message they are receiving
        msg.Write(LocalUserID);
        return msg;
    }

    //public void SendDinoTransform(Vector3 position, Quaternion rotation, Vector3 wallPos)
    //{
    //    // If we are connected to a session, broadcast our head info
    //    if (serverConnection != null && serverConnection.IsConnected())
    //    {
    //        // Create an outgoing network message to contain all the info we want to send
    //        NetworkOutMessage msg = CreateMessage((byte)MessageType.DinoTransform);

    //        AppendTransform(msg, position, rotation);
    //        AppendVector3(msg, wallPos);

    //        // Send the message as a broadcast, which will cause the server to forward it to all other users in the session.
    //        serverConnection.Broadcast(
    //            msg,
    //            MessagePriority.Immediate,
    //            MessageReliability.UnreliableSequenced,
    //            MessageChannel.Avatar);
    //    }
    //}

    //public void sendCallDino(int dinoNum, Vector3 dinoPos, Quaternion DinoRot, Vector3 wallPos, Quaternion wallRot)
    //{
    //    // If we are connected to a session, broadcast our head info
    //    if (serverConnection != null && serverConnection.IsConnected())
    //    {
    //        // Create an outgoing network message to contain all the info we want to send
    //        NetworkOutMessage msg = CreateMessage((byte)MessageType.CallDino);

    //        msg.Write(dinoNum);
    //        AppendTransform(msg, dinoPos, DinoRot);
    //        AppendTransform(msg, wallPos, wallRot);

    //        // Send the message as a broadcast, which will cause the server to forward it to all other users in the session.
    //        serverConnection.Broadcast(
    //            msg,
    //            MessagePriority.Immediate,
    //            MessageReliability.Reliable,
    //            MessageChannel.Avatar);
    //    }
    //}

    //public void sendHelpButton()
    //{
    //    if (serverConnection != null && serverConnection.IsConnected())
    //    {
    //        NetworkOutMessage msg = CreateMessage((byte)MessageType.helpButton);

    //        serverConnection.Broadcast(
    //            msg,
    //            MessagePriority.Immediate,
    //            MessageReliability.Reliable,
    //            MessageChannel.Avatar);
    //    }
    //}

    //public void sendReturnButton()
    //{
    //    if (serverConnection != null && serverConnection.IsConnected())
    //    {
    //        NetworkOutMessage msg = CreateMessage((byte)MessageType.returnButton);

    //        serverConnection.Broadcast(
    //            msg,
    //            MessagePriority.Immediate,
    //            MessageReliability.Reliable,
    //            MessageChannel.Avatar);
    //    }
    //}

    //public void sendDivideBorn()
    //{
    //    if (serverConnection != null && serverConnection.IsConnected())
    //    {
    //        NetworkOutMessage msg = CreateMessage((byte)MessageType.divideBone);

    //        serverConnection.Broadcast(
    //            msg,
    //            MessagePriority.Immediate,
    //            MessageReliability.Reliable,
    //            MessageChannel.Avatar);
    //    }
    //}

    //public void sendPartReaction()
    //{
    //    if (serverConnection != null && serverConnection.IsConnected())
    //    {
    //        NetworkOutMessage msg = CreateMessage((byte)MessageType.partReaction);

    //        serverConnection.Broadcast(
    //            msg,
    //            MessagePriority.Immediate,
    //            MessageReliability.Reliable,
    //            MessageChannel.Avatar);
    //    }
    //}

    //public void sendHitMaterial(int correct, string tag)
    //{
    //    if (serverConnection != null && serverConnection.IsConnected())
    //    {
    //        NetworkOutMessage msg = CreateMessage((byte)MessageType.hitMaterial);

    //        msg.Write(correct);
    //        msg.Write(tag);

    //        serverConnection.Broadcast(
    //            msg,
    //            MessagePriority.Immediate,
    //            MessageReliability.Reliable,
    //            MessageChannel.Avatar);
    //    }
    //}

    //public void sendMatchBorn(int correct, string tag)
    //{
    //    if (serverConnection != null && serverConnection.IsConnected())
    //    {
    //        NetworkOutMessage msg = CreateMessage((byte)MessageType.matchBorn);

    //        msg.Write(correct);
    //        msg.Write(tag);

    //        serverConnection.Broadcast(
    //            msg,
    //            MessagePriority.Immediate,
    //            MessageReliability.Reliable,
    //            MessageChannel.Avatar);
    //    }
    //}

    //public void sendClickedDividedBone(string tag)
    //{
    //    if (serverConnection != null && serverConnection.IsConnected())
    //    {
    //        NetworkOutMessage msg = CreateMessage((byte)MessageType.clickDivedBone);

    //        msg.Write(tag);

    //        serverConnection.Broadcast(
    //            msg,
    //            MessagePriority.Immediate,
    //            MessageReliability.Reliable,
    //            MessageChannel.Avatar);
    //    }
    //}

    //public void sendMovedDividedBone(Vector3 pos, Quaternion rot)
    //{
    //    if (serverConnection != null && serverConnection.IsConnected())
    //    {
    //        NetworkOutMessage msg = CreateMessage((byte)MessageType.moveDividedBone);

    //        AppendTransform(msg, pos, rot);

    //        serverConnection.Broadcast(
    //            msg,
    //            MessagePriority.Immediate,
    //            MessageReliability.UnreliableSequenced,
    //            MessageChannel.Avatar);
    //    }
    //}

    //public void sendDinoColor(Color color)
    //{
    //    if (serverConnection != null && serverConnection.IsConnected())
    //    {
    //        NetworkOutMessage msg = CreateMessage((byte)MessageType.moveDividedBone);

    //        AppendVector4(msg, new Vector4(color.r, color.g, color.b, color.a));

    //        serverConnection.Broadcast(
    //            msg,
    //            MessagePriority.Immediate,
    //            MessageReliability.UnreliableSequenced,
    //            MessageChannel.Avatar);
    //    }
    //}

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (serverConnection != null)
        {
            for (byte index = (byte)MessageType.MoveTarget; index < (byte)MessageType.Max; index++)
            {
                serverConnection.RemoveListener(index, connectionAdapter);
            }
            connectionAdapter.MessageReceivedCallback -= OnMessageReceived;
        }
    }

    private void OnMessageReceived(NetworkConnection connection, NetworkInMessage msg)
    {
        byte messageType = msg.ReadByte();
        MessageCallback messageHandler = MessageHandlers[(MessageType)messageType];
        if (messageHandler != null)
        {
            messageHandler(msg);
        }
    }

    #region HelperFunctionsForWriting

    private void AppendTransform(NetworkOutMessage msg, Vector3 position, Quaternion rotation)
    {
        AppendVector3(msg, position);
        AppendQuaternion(msg, rotation);
    }

    private void AppendTarget(NetworkOutMessage msg, int target)
    {
        AppentInt(msg, target);
    }

    private void AppendVector3(NetworkOutMessage msg, Vector3 vector)
    {
        msg.Write(vector.x);
        msg.Write(vector.y);
        msg.Write(vector.z);
    }

    private void AppendVector4(NetworkOutMessage msg, Vector4 vector)
    {
        msg.Write(vector.x);
        msg.Write(vector.y);
        msg.Write(vector.z);
        msg.Write(vector.w);
    }

    private void AppendQuaternion(NetworkOutMessage msg, Quaternion rotation)
    {
        msg.Write(rotation.x);
        msg.Write(rotation.y);
        msg.Write(rotation.z);
        msg.Write(rotation.w);
    }
    private void AppentInt(NetworkOutMessage msg, int target)
    {
        msg.Write(target);
    }

    private void AppentFloat(NetworkOutMessage msg, float target)
    {
        msg.Write(target);
    }

    private void AppendString(NetworkOutMessage msg, XString str)
    {

    }

    #endregion

    #region HelperFunctionsForReading

    public Vector3 ReadVector3(NetworkInMessage msg)
    {
        return new Vector3(msg.ReadFloat(), msg.ReadFloat(), msg.ReadFloat());
    }

    public Vector4 ReadVector4(NetworkInMessage msg)
    {
        return new Vector4(msg.ReadFloat(), msg.ReadFloat(), msg.ReadFloat(), msg.ReadFloat());
    }

    public Quaternion ReadQuaternion(NetworkInMessage msg)
    {
        return new Quaternion(msg.ReadFloat(), msg.ReadFloat(), msg.ReadFloat(), msg.ReadFloat());
    }

    public int ReadInt(NetworkInMessage msg)
    {
        return msg.ReadInt32();
    }

    public float ReadFloat(NetworkInMessage msg)
    {
        return msg.ReadFloat();
    }

    #endregion    
}
