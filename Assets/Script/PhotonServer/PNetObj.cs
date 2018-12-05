using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNetObj : Photon.PunBehaviour {

    // Standard Val
    public int id = 0;//0:master 1:lens 2:server

    // Lens
    public int myLensIdx;
    private InputManager_S inputComp;
    private cgChessBoardScript boardComp;
    public PhotonView serverObj;

    private void Start()
    {
        if (!photonView.isMine)
            return;
        if (!GameObject.Find("SpectatorView").GetComponent<HoloToolkit.Unity.Preview.SpectatorView.SpectatorView>().IsHost)
            return;
        inputComp = GameObject.Find("InputManager").GetComponent<InputManager_S>();
        boardComp = GameObject.Find("ChessBoard 8x8").GetComponent<cgChessBoardScript>();
        serverObj = GameObject.Find("PNetServer(Clone)").GetComponent<PhotonView>();

        serverObj.RPC("ConnectToRoom", PhotonTargets.AllBuffered, photonView.viewID, 1, PhotonNetwork.player.ID);
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
    public void LensIndexing(int idx) // LensOnly
    {
        if (!photonView.isMine)
            return;
        Debug.Log("MyIndex is " + idx);

        myLensIdx = idx;
        inputComp.Init(myLensIdx); 
    }

    [PunRPC]
    public void MultiGameStartBtnEvent()// Server, Lens Only
    {
        if (!photonView.isMine)
            return;
        boardComp.Mode = cgChessBoardScript.BoardMode.PlayerVsPlayer;
        inputComp.isMultiGame = true;
    }
    [PunRPC]
    public void SingleGameStartBtnEvent()// Server, Lens Only
    {
        if (!photonView.isMine)
            return;
        boardComp.Mode = cgChessBoardScript.BoardMode.PlayerVsEngine;
    }
    [PunRPC]
    public void GameStart() //Controller, Lens Only
    {
        if (!photonView.isMine)
            return;

        inputComp.isGameStart = true;
        boardComp.Init();
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
        inputComp.TurnChangeEvent(idx);
    }

    [PunRPC]
    public void ControllerMove(int targetIdx, int input) // Server, Lens Only
    {
        if (!photonView.isMine)
            return;
        inputComp.ControlInput(input);
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
        boardComp.ResetBoard();
        boardComp.GetComponent<Animator>().SetTrigger("BackStart");
    }


    [PunRPC]
    public void MultiReady(int firstOwnerId, int secondOwnerId) //ControllerOnly
    {
    }
    [PunRPC]
    public void TurnEnd()//Server Only
    {
    }
}
