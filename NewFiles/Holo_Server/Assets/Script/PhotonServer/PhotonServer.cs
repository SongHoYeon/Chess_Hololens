using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonServer : Photon.PunBehaviour {

    [SerializeField]
    private GameObject pNetObj;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        RoomOptions option = new RoomOptions();
        option.MaxPlayers = 10;
        PhotonNetwork.CreateRoom("Game", option, null);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Create Room");

        PhotonNetwork.Instantiate("PNetServer", Vector3.zero, Quaternion.identity, 0);
    }
}
