using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonServer : Photon.PunBehaviour
{
    [HideInInspector]
    public PNetObj pNetObj;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private Text controllerConnectText;
    public InputManager_S inputComp;
    public cgChessBoardScript boardComp;

    public void Init()
    {
        if (!GameObject.Find("SpectatorView").GetComponent<HoloToolkit.Unity.Preview.SpectatorView.SpectatorView>().IsHost)
            return;
        PhotonNetwork.ConnectUsingSettings("v1.0");
    }

    public void SetControllerConnectUI(bool isConnect)
    {
        if (isConnect)
        {
            if (!canvas.activeInHierarchy)
                return;
            canvas.gameObject.SetActive(false);
        }
        else
        {
            if (canvas.activeInHierarchy)
                return;
            canvas.gameObject.SetActive(true);
            controllerConnectText.text = (pNetObj.myLensIdx + 1).ToString() + "PC 컨트롤러를 연결해주세요.";
        }
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
