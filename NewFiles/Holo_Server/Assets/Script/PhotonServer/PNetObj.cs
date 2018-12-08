using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNetObj : Photon.PunBehaviour
{

    // Standard Val
    public int id = 0;//0:master 1:lens 2:server

    // Server
    public Dictionary<int, int> playerDic;
    public List<int> totalList;
    public List<int> lensList;
    public List<int> controllerList;
    public Dictionary<int, int> controllerLensConnection;// actorId, target

    private int currentTurnLensIdx;

    private void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            controllerLensConnection = new Dictionary<int, int>();
            playerDic = new Dictionary<int, int>();
            totalList = new List<int>();
            lensList = new List<int>();
            controllerList = new List<int>();
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        if (PhotonNetwork.isMasterClient)
        {
            base.OnPhotonPlayerDisconnected(otherPlayer);

            int targetRemoveViewId;
            playerDic.TryGetValue(otherPlayer.ID, out targetRemoveViewId);
            totalList.Remove(targetRemoveViewId);

            if (lensList.Exists(x => x == targetRemoveViewId))
                lensList.Remove(targetRemoveViewId);
            else if (controllerList.Exists(x => x == targetRemoveViewId))
            {
                int beforeTargetIdx;
                controllerLensConnection.TryGetValue(otherPlayer.ID, out beforeTargetIdx);
                for (int i = 0; i < lensList.Count; i++)
                {
                    PhotonView.Find(lensList[i]).RPC("ControllerDisConnect", PhotonTargets.OthersBuffered, (int)beforeTargetIdx);
                }
                controllerList.Remove(targetRemoveViewId);
                controllerLensConnection.Remove(otherPlayer.ID);
            }
        }
    }

    [PunRPC]
    public void ConnectToRoom(int viewId, int type, int playerId)
    {
        if (!PhotonNetwork.isMasterClient)
            return;
        PhotonView newObj = PhotonView.Find(viewId);
        totalList.Add(viewId);
        playerDic.Add(playerId, viewId);
        if (type == 1)
        {
            lensList.Add(totalList[totalList.Count - 1]);
            newObj.RPC("LensIndexing", PhotonTargets.AllBuffered, lensList.Count - 1);

            if (lensList.Count == 2)
            {
                Debug.Log("Lens Count is 2. Multi Ready");
                for (int i = 0; i < controllerList.Count; i++)
                    PhotonView.Find(controllerList[i]).RPC("MultiReady", PhotonTargets.OthersBuffered, lensList[0], lensList[1]);
            }
        }
        else if (type == 2)
        {
            controllerList.Add(totalList[totalList.Count - 1]);
            Debug.Log("Controller connectionId : " + (totalList.Count - 1).ToString());
        }
    }

    [PunRPC]
    public void MultiGameStartBtnEvent()// ServerOnly
    {
        if (!photonView.isMine)
            return;
        currentTurnLensIdx = 0;
        for (int i = 0; i < totalList.Count; i++)
        {
            PhotonView.Find(totalList[i]).RPC("MultiGameStartBtnEvent", PhotonTargets.OthersBuffered, null);
            PhotonView.Find(totalList[i]).RPC("GameStart", PhotonTargets.OthersBuffered, null);
            PhotonView.Find(totalList[i]).RPC("SetTurnIdx", PhotonTargets.OthersBuffered, currentTurnLensIdx);
        }
    }

    [PunRPC]
    public void SingleGameStartBtnEvent()// ServerOnly
    {
        if (!photonView.isMine)
            return;
        currentTurnLensIdx = 0;
        for (int i = 0; i < totalList.Count; i++)
        {
            PhotonView.Find(totalList[i]).RPC("SingleGameStartBtnEvent", PhotonTargets.OthersBuffered, null);
            PhotonView.Find(totalList[i]).RPC("GameStart", PhotonTargets.OthersBuffered, null);
            PhotonView.Find(totalList[i]).RPC("SetTurnIdx", PhotonTargets.OthersBuffered, currentTurnLensIdx);
        }
    }

    [PunRPC]
    public void GameStart() //Controller, Lens Only
    {

    }

    [PunRPC]
    public void SetTurnIdx(int idx) //Controller, Lens Only
    {

    }

    [PunRPC]
    public void ControllerMove(int targetIdx, int input)
    {
        if (!photonView.isMine)
            return;
        Debug.Log("Move Target :  " + targetIdx.ToString() + "  Input : " + input.ToString());
        for (int i = 0; i < lensList.Count; i++)
        {
            PhotonView.Find(lensList[i]).RPC("ControllerMove", PhotonTargets.OthersBuffered, targetIdx, input);
        }
    }

    [PunRPC]
    public void ReturnToMain() //Server, Controller, Lens Only
    {
        if (!photonView.isMine)
            return;
        for (int i = 0; i < totalList.Count; i++)
        {
            PhotonView.Find(totalList[i]).RPC("ReturnToMain", PhotonTargets.OthersBuffered);
        }
    }

    [PunRPC]
    public void TurnEnd()//Server Only
    {
        if (currentTurnLensIdx == 0)
            currentTurnLensIdx = 1;
        else
            currentTurnLensIdx = 0;
        for (int i = 0; i < totalList.Count; i++)
        {
            PhotonView.Find(totalList[i]).RPC("SetTurnIdx", PhotonTargets.Others, currentTurnLensIdx);
        }
    }

    [PunRPC]
    public void LensIndexing(int idx)// LensOnly
    {

    }

    [PunRPC]
    public void MultiReady(int firstOwnerId, int secondOwnerId)//ControllerOnly
    {

    }

    [PunRPC]
    public void ControllerConnect(int actorId, int targetIdx)
    {
        if (!photonView.isMine)
            return;
        if (controllerLensConnection.ContainsKey(actorId))
            controllerLensConnection.Remove(actorId);
        controllerLensConnection.Add(actorId, targetIdx);

        for (int i = 0; i < lensList.Count; i++)
        {
            PhotonView.Find(lensList[i]).RPC("ControllerConnect", PhotonTargets.OthersBuffered, actorId, targetIdx);
        }
    }
    [PunRPC]
    public void ControllerDisConnect(int beforeTargetIdx)
    {
        if (!photonView.isMine)
            return;
        for (int i = 0; i < lensList.Count; i++)
        {
            PhotonView.Find(lensList[i]).RPC("ControllerDisConnect", PhotonTargets.OthersBuffered, beforeTargetIdx);
        }
    }
}
