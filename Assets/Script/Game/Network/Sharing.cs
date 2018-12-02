using HoloToolkit.Sharing;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using System.Linq;

public class Sharing : MonoBehaviour
{
    [SerializeField]
    private VoiceCommand voiceCommandMng;
    [SerializeField]
    private GameObject phanObj;
    //[SerializeField]
    //private PieceManager pieceManager;

    void Start()
    {
        //pc에서 돌아갈때 (spectatorView일때)
        //isSpectatorView = true;

#if !UNITY_EDITOR
        //isSpectatorView = false; // 홀로렌즈에서 작동할때
#endif

        CustomMessage.Instance.MessageHandlers[CustomMessage.MessageType.MoveTarget] = this.receiveMoveTarget;
        CustomMessage.Instance.MessageHandlers[CustomMessage.MessageType.CreatePhan] = this.receiveCreatePhan;
        CustomMessage.Instance.MessageHandlers[CustomMessage.MessageType.CreateMyPiece] = this.receiveCreateMyPiece;
        CustomMessage.Instance.MessageHandlers[CustomMessage.MessageType.TurnChange] = this.receiveTurnChange;
    }

    private void receiveMoveTarget(NetworkInMessage msg)
    {
        long id = msg.ReadInt64();

        int from = CustomMessage.Instance.ReadInt(msg);
        int pieceIdx = CustomMessage.Instance.ReadInt(msg);
        int row = CustomMessage.Instance.ReadInt(msg);
        int col = CustomMessage.Instance.ReadInt(msg);

        //Piece targetPiece = PieceManager.yourPieces.Values.First(x => ((int)x.GetPieceSettingIdx()) == pieceIdx);
        //WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\nMove - from : \"{0}\" pieceName : \"{1}\", \nx : \"{2}\", y : \"{3}\"", from, 
        //    targetPiece.name, (Defines.BoardProperty.COL_COUNT - 1) - col, (Defines.BoardProperty.ROW_COUNT - 1) - row);
        //targetPiece.SetMove(PointCreater.pointCompList[(Defines.BoardProperty.COL_COUNT - 1) - col, (Defines.BoardProperty.ROW_COUNT - 1) - row], () => 
        //{
        //    WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\nCallback");

        //    GameManager.instance.TurnChange(from);
        //    CustomMessage.Instance.SendTurnChange(from);
        //});

    }
    private void receiveCreatePhan(NetworkInMessage msg)
    {
        long id = msg.ReadInt64();

        Vector3 p = CustomMessage.Instance.ReadVector3(msg);

        CustomMessage.Instance.LocalPlayer = Enums.Player.Player2;
        if (WorldAnchorManager.Instance != null && WorldAnchorManager.Instance.AnchorDebugText != null)
            WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\nLocalPlayer : \"{0}\"", CustomMessage.Instance.LocalPlayer.ToString());

        phanObj.SetActive(true);
        phanObj.transform.position = p;
        phanObj.transform.localEulerAngles = new Vector3(-25f, 0f, 0f);

        voiceCommandMng.check = true;
    }

    private void receiveCreateMyPiece(NetworkInMessage msg)
    {
        long id = msg.ReadInt64();
        //WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\nSharing ReceiveMyPiece");
        //pieceManager.CreateYourPiece();
    }

    private void receiveTurnChange(NetworkInMessage msg)
    {
        long id = msg.ReadInt64();

        //int fromIdx = CustomMessage.Instance.ReadInt(msg);
        //GameManager.instance.TurnChange(fromIdx);
    }
}
