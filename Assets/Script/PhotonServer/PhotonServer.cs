using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonServer : Photon.PunBehaviour {
    [HideInInspector]
    public PNetObj pNetObj;

	void Start () {
        if (!GameObject.Find("SpectatorView").GetComponent<HoloToolkit.Unity.Preview.SpectatorView.SpectatorView>().IsHost)
            return;
        PhotonNetwork.ConnectUsingSettings("v1.0");
	}

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Join Lobby");
        PhotonNetwork.JoinRoom("Game");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Join Room");

        pNetObj = PhotonNetwork.Instantiate("PNetLens", Vector3.zero, Quaternion.identity, 0).GetComponent<PNetObj>();
    }
}
