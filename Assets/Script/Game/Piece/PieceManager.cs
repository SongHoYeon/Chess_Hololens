using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class PieceManager : MonoBehaviour
{
    [SerializeField]
    private Transform pieceParent;

    [SerializeField]
    private GameObject king_p1;
    [SerializeField]
    private GameObject sha_p1;
    [SerializeField]
    private GameObject cha_p1;
    [SerializeField]
    private GameObject pho_p1;
    [SerializeField]
    private GameObject mha_p1;
    [SerializeField]
    private GameObject shang_p1;
    [SerializeField]
    private GameObject jol_p1;
    [SerializeField]
    private GameObject king_p2;
    [SerializeField]
    private GameObject sha_p2;
    [SerializeField]
    private GameObject cha_p2;
    [SerializeField]
    private GameObject pho_p2;
    [SerializeField]
    private GameObject mha_p2;
    [SerializeField]
    private GameObject shang_p2;
    [SerializeField]
    private GameObject jol_p2;

    public static Dictionary<Enums.PieceSetting, Piece> myPieces;
    public static Dictionary<Enums.PieceSetting, Piece> yourPieces;

    public void CreateMyPiece()
    {
        myPieces = new Dictionary<Enums.PieceSetting, Piece>();

        GameObject obj = null;

        // 킹
        obj = GameObject.Instantiate<GameObject>(king_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[4, 8], Enums.PieceSetting.King);
        obj.transform.name = "Player1_" + Enums.PieceSetting.King.ToString();
        myPieces.Add(Enums.PieceSetting.King, obj.GetComponent<Piece>());

        // 사
        obj = GameObject.Instantiate<GameObject>(sha_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[3, 9], Enums.PieceSetting.Sha1);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Sha1.ToString();
        myPieces.Add(Enums.PieceSetting.Sha1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(sha_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[5, 9], Enums.PieceSetting.Sha2);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Sha2.ToString();
        myPieces.Add(Enums.PieceSetting.Sha2, obj.GetComponent<Piece>());

        // 차
        obj = GameObject.Instantiate<GameObject>(cha_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[0, 9], Enums.PieceSetting.Cha1);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Cha1.ToString();
        myPieces.Add(Enums.PieceSetting.Cha1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(cha_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[8, 9], Enums.PieceSetting.Cha2);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Cha2.ToString();
        myPieces.Add(Enums.PieceSetting.Cha2, obj.GetComponent<Piece>());

        //// 포
        obj = GameObject.Instantiate<GameObject>(pho_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[1, 7], Enums.PieceSetting.Pho1);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Pho1.ToString();
        myPieces.Add(Enums.PieceSetting.Pho1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(pho_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[7, 7], Enums.PieceSetting.Pho2);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Pho2.ToString();
        myPieces.Add(Enums.PieceSetting.Pho2, obj.GetComponent<Piece>());

        //// 마
        obj = GameObject.Instantiate<GameObject>(mha_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[2, 9], Enums.PieceSetting.Mha1);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Mha1.ToString();
        myPieces.Add(Enums.PieceSetting.Mha1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(mha_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[6, 9], Enums.PieceSetting.Mha2);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Mha2.ToString();
        myPieces.Add(Enums.PieceSetting.Mha2, obj.GetComponent<Piece>());

        //// 상
        obj = GameObject.Instantiate<GameObject>(shang_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[1, 9], Enums.PieceSetting.Shang1);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Shang1.ToString();
        myPieces.Add(Enums.PieceSetting.Shang1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(shang_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[7, 9], Enums.PieceSetting.Shang2);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Shang2.ToString();
        myPieces.Add(Enums.PieceSetting.Shang2, obj.GetComponent<Piece>());

        //// 졸
        obj = GameObject.Instantiate<GameObject>(jol_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[0, 6], Enums.PieceSetting.Jol1);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Jol1.ToString();
        myPieces.Add(Enums.PieceSetting.Jol1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[2, 6], Enums.PieceSetting.Jol2);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Jol2.ToString();
        myPieces.Add(Enums.PieceSetting.Jol2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[4, 6], Enums.PieceSetting.Jol3);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Jol3.ToString();
        myPieces.Add(Enums.PieceSetting.Jol3, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[6, 6], Enums.PieceSetting.Jol4);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Jol4.ToString();
        myPieces.Add(Enums.PieceSetting.Jol4, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol_p1);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[8, 6], Enums.PieceSetting.Jol5);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Jol5.ToString();
        myPieces.Add(Enums.PieceSetting.Jol5, obj.GetComponent<Piece>());

        CustomMessage.Instance.SendCreateMyPiece();
    }

    public void CreateYourPiece()
    {
        yourPieces = new Dictionary<Enums.PieceSetting, Piece>();

        GameObject obj = null;

        obj = GameObject.Instantiate<GameObject>(king_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[4, 1], Enums.PieceSetting.King);
        obj.transform.name = "Player2_" + Enums.PieceSetting.King.ToString();
        yourPieces.Add(Enums.PieceSetting.King, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(sha_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[5, 0], Enums.PieceSetting.Sha1);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Sha1.ToString();
        yourPieces.Add(Enums.PieceSetting.Sha1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(sha_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[3, 0], Enums.PieceSetting.Sha2);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Sha2.ToString();
        yourPieces.Add(Enums.PieceSetting.Sha2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(cha_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[8, 0], Enums.PieceSetting.Cha1);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Cha1.ToString();
        yourPieces.Add(Enums.PieceSetting.Cha1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(cha_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[0, 0], Enums.PieceSetting.Cha2);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Cha2.ToString();
        yourPieces.Add(Enums.PieceSetting.Cha2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(pho_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[7, 2], Enums.PieceSetting.Pho1);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Pho1.ToString();
        yourPieces.Add(Enums.PieceSetting.Pho1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(pho_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[1, 2], Enums.PieceSetting.Pho2);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Pho2.ToString();
        yourPieces.Add(Enums.PieceSetting.Pho2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(mha_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[6, 0], Enums.PieceSetting.Mha1);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Mha1.ToString();
        yourPieces.Add(Enums.PieceSetting.Mha1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(mha_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[2, 0], Enums.PieceSetting.Mha2);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Mha2.ToString();
        yourPieces.Add(Enums.PieceSetting.Mha2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(shang_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[7, 0], Enums.PieceSetting.Shang1);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Shang1.ToString();
        yourPieces.Add(Enums.PieceSetting.Shang1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(shang_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[1, 0], Enums.PieceSetting.Shang2);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Shang2.ToString();
        yourPieces.Add(Enums.PieceSetting.Shang2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[8, 3], Enums.PieceSetting.Jol1);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Jol1.ToString();
        yourPieces.Add(Enums.PieceSetting.Jol1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[6, 3], Enums.PieceSetting.Jol2);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Jol2.ToString();
        yourPieces.Add(Enums.PieceSetting.Jol2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[4, 3], Enums.PieceSetting.Jol3);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Jol3.ToString();
        yourPieces.Add(Enums.PieceSetting.Jol3, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[2, 3], Enums.PieceSetting.Jol4);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Jol4.ToString();
        yourPieces.Add(Enums.PieceSetting.Jol4, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol_p2);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[0, 3], Enums.PieceSetting.Jol5);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Jol5.ToString();
        yourPieces.Add(Enums.PieceSetting.Jol5, obj.GetComponent<Piece>());

        GameManager.instance.GameStart();
    }
}
