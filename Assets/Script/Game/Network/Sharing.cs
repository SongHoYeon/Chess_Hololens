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
    [SerializeField]
    private PieceManager pieceManager;

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

        Piece targetPiece = PieceManager.yourPieces.Values.First(x => ((int)x.GetPieceSettingIdx()) == pieceIdx);
        WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\nMove - from : \"{0}\" pieceName : \"{1}\", \nx : \"{2}\", y : \"{3}\"", from, 
            targetPiece.name, (Defines.BoardProperty.COL_COUNT - 1) - col, (Defines.BoardProperty.ROW_COUNT - 1) - row);
        targetPiece.SetMove(PointCreater.pointCompList[(Defines.BoardProperty.COL_COUNT - 1) - col, (Defines.BoardProperty.ROW_COUNT - 1) - row], () => 
        {
            WorldAnchorManager.Instance.AnchorDebugText.text += string.Format("\nCallback");

            GameManager.instance.TurnChange(from);
            CustomMessage.Instance.SendTurnChange(from);
        });

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
        pieceManager.CreateYourPiece();
    }

    private void receiveTurnChange(NetworkInMessage msg)
    {
        long id = msg.ReadInt64();

        int fromIdx = CustomMessage.Instance.ReadInt(msg);
        GameManager.instance.TurnChange(fromIdx);
    }
    /*
    private void receiveCallDino(NetworkInMessage msg)
    {
        long id = msg.ReadInt64();

        int dinoNum = msg.ReadInt32();
        Vector3 p = HoloDinosaurCustomMessages.Instance.ReadVector3(msg) + calibP;
        Quaternion r = HoloDinosaurCustomMessages.Instance.ReadQuaternion(msg);

        Vector3 wallP = HoloDinosaurCustomMessages.Instance.ReadVector3(msg) + calibP;
        Quaternion wallR = HoloDinosaurCustomMessages.Instance.ReadQuaternion(msg);

        if (TrexControll.Instance != null)
        {
            TrexControll.Instance.CallDinoForSpectator(dinoNum, p, r, wallP, wallR);
        }
    }

    private void receiveHelpButton(NetworkInMessage msg)
    {
        if (TrexControll.Instance != null)
        {
            TrexControll.Instance.HelpButtonForSpectator();
        }
    }

    private void receiveReturnButton(NetworkInMessage msg)
    {
        if (TrexControll.Instance != null)
        {
            TrexControll.Instance.returnButtonForSpectator();
        }
    }

    private void receiveDivideBorn(NetworkInMessage msg)
    {
        if (TrexControll.Instance != null)
        {
            TrexControll.Instance.divideBornForSpectator();
        }
    }

    private void receiveMatchBorn(NetworkInMessage msg)
    {
        long id = msg.ReadInt64();

        int correct = msg.ReadInt32();
        string tag = msg.ReadString();

        if (TrexControll.Instance != null)
        {
            TrexControll.Instance.matchBorn(correct, tag);
        }
    }

    private void receiveClickedDivideBone(NetworkInMessage msg)
    {
        long id = msg.ReadInt64();

        string tag = msg.ReadString();

        if (TrexControll.Instance != null)
        {
            TrexControll.Instance.clickDividecBorn(tag);
        }
    }

    private void receiveMovedDivideBone(NetworkInMessage msg)
    {
        long id = msg.ReadInt64();

        Vector3 pos = HoloDinosaurCustomMessages.Instance.ReadVector3(msg) + calibP;
        Quaternion r = HoloDinosaurCustomMessages.Instance.ReadQuaternion(msg);

        if (TrexControll.Instance != null)
        {
            TrexControll.Instance.movedDivideBone(pos, r);
        }
    }

    private void receiveDinoColor(NetworkInMessage msg)
    {
        long id = msg.ReadInt64();

        Vector4 v4 = HoloDinosaurCustomMessages.Instance.ReadVector4(msg);

        Color color = new Color(v4.x, v4.y, v4.z, v4.z);

        if (TrexControll.Instance != null)
        {
            TrexControll.Instance.ChangeDinoColorForSpectatorView(color);
        }
    }
    */
    void Update()
    {
        //setCalibValue();
    }

    //private void setCalibValue()
    //{
    //    if (invX == true)
    //    {
    //        inv_x = -1;
    //    }
    //    else
    //    {
    //        inv_x = 1;
    //    }
    //    if (invY == true)
    //    {
    //        inv_y = -1;
    //    }
    //    else
    //    {
    //        inv_y = 1;
    //    }

    //    if (invZ == true)
    //    {
    //        inv_z = -1;
    //    }
    //    else
    //    {
    //        inv_z = 1;
    //    }
    //}

    //public void OnClickX()
    //{
    //    inv_x *= -1;
    //}

    //public void OnClickY()
    //{
    //    inv_y *= -1;
    //}

    //public void OnClickZ()
    //{
    //    inv_z *= -1;
    //}

    //public void OnClickRS()
    //{
    //    inv_x = 1;
    //    inv_y = 1;
    //    inv_z = 1;
    //}

    //private bool compareVec3(Vector3 r, Vector3 l)
    //{
    //    if (r == l)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //private bool compareQuaternion(Quaternion r, Quaternion l)
    //{
    //    if (r == l)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}
