using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNetObj : Photon.PunBehaviour {

    // Standard Val
    public int id = 0;//0:master 1:lens 2:server

    // Lens
    public int myLensIdx;
    public PhotonView serverObj;
    private bool haveController;

    private PhotonServer serverComp;

    private void Start()
    {
        if (!photonView.isMine)
            return;
        haveController = false;
        myLensIdx = -1;
        serverComp = GameObject.Find("MyNetwork").GetComponent<PhotonServer>();
        serverObj = GameObject.Find("PNetServer(Clone)").GetComponent<PhotonView>();

        serverObj.RPC("ConnectToRoom", PhotonTargets.AllBuffered, photonView.viewID, id, PhotonNetwork.player.ID);
    }

    public void TurnEndSend()
    {
        serverObj.RPC("TurnEnd", PhotonTargets.MasterClient);
    }

    [PunRPC]
    public void ConnectToRoom(int viewId, int type, int playerId) // ServerOnly
    {
        if (!photonView.isMine)
            return;
    }
    [PunRPC]
    public void ControllerConnect(int actorId, int targetIdx)
    {
        if (!photonView.isMine)
            return;

        if (myLensIdx == targetIdx)
        {
            haveController = true;
        }
    }

    [PunRPC]
    public void ControllerDisConnect(int beforeTargetIdx)
    {
        if (!photonView.isMine)
            return;
        if (myLensIdx == beforeTargetIdx)
        {
            haveController = false;
        }
    }

    [PunRPC]
    public void LensIndexing(int idx) // LensOnly
    {
        if (!photonView.isMine)
            return;
        Debug.Log("MyIndex is " + idx);

        myLensIdx = idx;
        serverComp.inputComp.Init(myLensIdx); 
    }

    [PunRPC]
    public void MultiGameStartBtnEvent()// Server, Lens Only
    {
        if (!photonView.isMine)
            return;
        serverComp.boardComp.Mode = cgChessBoardScript.BoardMode.PlayerVsPlayer;
        serverComp.inputComp.isMultiGame = true;
    }
    [PunRPC]
    public void SingleGameStartBtnEvent()// Server, Lens Only
    {
        if (!photonView.isMine)
            return;
        serverComp.boardComp.Mode = cgChessBoardScript.BoardMode.PlayerVsEngine;
    }
    [PunRPC]
    public void GameStart() //Controller, Lens Only
    {
        if (!photonView.isMine)
            return;

        serverComp.inputComp.isGameStart = true;
        serverComp.boardComp.Init();
    }

    [PunRPC]
    public void SetTurnIdx(int idx) //Controller, Lens Only
    {
        if (!photonView.isMine)
            return;

        if (idx == myLensIdx)
        {
            Debug.Log("Setted My Turn");
        }
        else
        {
            Debug.Log("Setted Other Turn");
        }
        serverComp.inputComp.TurnChangeEvent(idx);
    }

    [PunRPC]
    public void ControllerMove(int targetIdx, int input) // Server, Lens Only
    {
        if (!photonView.isMine)
            return;
        serverComp.inputComp.ControlInput(input);
        if (targetIdx == myLensIdx)
        {
            Debug.Log("MyTern - Move Target :  " + targetIdx.ToString() + "  Input : " + input.ToString());
        }
        else
        {
            Debug.Log("OtherTern - Move Target :  " + targetIdx.ToString() + "  Input : " + input.ToString());
        }

    }
    [PunRPC]
    public void ReturnToMain() //Server, Controller, Lens Only
    {
        if (!photonView.isMine)
            return;
        serverComp.boardComp.ResetBoard();
        serverComp.boardComp.GetComponent<Animator>().SetTrigger("BackStart");
    }


    [PunRPC]
    public void MultiReady(int firstOwnerId, int secondOwnerId) //ControllerOnly
    {
    }
    [PunRPC]
    public void TurnEnd()//Server Only
    {
    }

    private void Update()
    {
        if (!photonView.isMine)
            return;
        if (myLensIdx == -1)
            return;
        if (serverComp != null)
            serverComp.SetControllerConnectUI(haveController);
    }
}
