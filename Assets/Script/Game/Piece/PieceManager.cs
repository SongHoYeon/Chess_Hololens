using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField]
    private Transform pieceParent;

    [SerializeField]
    private GameObject king;
    [SerializeField]
    private GameObject sha;
    [SerializeField]
    private GameObject cha;
    [SerializeField]
    private GameObject pho;
    [SerializeField]
    private GameObject mha;
    [SerializeField]
    private GameObject shang;
    [SerializeField]
    private GameObject jol;

    public static Dictionary<Enums.PieceSetting, Piece> player1Pieces;
    public static Dictionary<Enums.PieceSetting, Piece> player2Pieces;


    public void CreatePiece()
    {
        player1Pieces = new Dictionary<Enums.PieceSetting, Piece>();
        player2Pieces = new Dictionary<Enums.PieceSetting, Piece>();

        GameObject obj = null;

        // 킹
        obj = GameObject.Instantiate<GameObject>(king);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[4, 8]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.King.ToString();
        player1Pieces.Add(Enums.PieceSetting.King, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(king);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[4, 1]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.King.ToString();
        player2Pieces.Add(Enums.PieceSetting.King, obj.GetComponent<Piece>());

        // 사
        obj = GameObject.Instantiate<GameObject>(sha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[3, 9]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Sha1.ToString();
        player1Pieces.Add(Enums.PieceSetting.Sha1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(sha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[5, 9]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Sha2.ToString();
        player1Pieces.Add(Enums.PieceSetting.Sha2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(sha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[3, 0]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Sha1.ToString();
        player2Pieces.Add(Enums.PieceSetting.Sha1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(sha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[5, 0]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Sha2.ToString();
        player2Pieces.Add(Enums.PieceSetting.Sha2, obj.GetComponent<Piece>());

        // 차
        obj = GameObject.Instantiate<GameObject>(cha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[0, 9]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Cha1.ToString();
        player1Pieces.Add(Enums.PieceSetting.Cha1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(cha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[8, 9]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Cha2.ToString();
        player1Pieces.Add(Enums.PieceSetting.Cha2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(cha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[0, 0]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Cha1.ToString();
        player2Pieces.Add(Enums.PieceSetting.Cha1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(cha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[8, 0]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Cha2.ToString();
        player2Pieces.Add(Enums.PieceSetting.Cha2, obj.GetComponent<Piece>());

        //// 포
        obj = GameObject.Instantiate<GameObject>(pho);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[1, 7]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Pho1.ToString();
        player1Pieces.Add(Enums.PieceSetting.Pho1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(pho);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[7, 7]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Pho2.ToString();
        player1Pieces.Add(Enums.PieceSetting.Pho2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(pho);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[1, 2]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Pho1.ToString();
        player2Pieces.Add(Enums.PieceSetting.Pho1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(pho);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[7, 2]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Pho2.ToString();
        player2Pieces.Add(Enums.PieceSetting.Pho2, obj.GetComponent<Piece>());

        //// 마
        obj = GameObject.Instantiate<GameObject>(mha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[2, 9]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Mha1.ToString();
        player1Pieces.Add(Enums.PieceSetting.Mha1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(mha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[6, 9]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Mha2.ToString();
        player1Pieces.Add(Enums.PieceSetting.Mha2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(mha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[2, 0]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Mha1.ToString();
        player2Pieces.Add(Enums.PieceSetting.Mha1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(mha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[6, 0]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Mha2.ToString();
        player2Pieces.Add(Enums.PieceSetting.Mha2, obj.GetComponent<Piece>());

        //// 상
        obj = GameObject.Instantiate<GameObject>(shang);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[1, 9]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Shang1.ToString();
        player1Pieces.Add(Enums.PieceSetting.Shang1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(shang);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[7, 9]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Shang2.ToString();
        player1Pieces.Add(Enums.PieceSetting.Shang2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(shang);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[1, 0]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Shang1.ToString();
        player2Pieces.Add(Enums.PieceSetting.Shang1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(shang);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[7, 0]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Shang2.ToString();
        player2Pieces.Add(Enums.PieceSetting.Shang2, obj.GetComponent<Piece>());

        //// 졸
        obj = GameObject.Instantiate<GameObject>(jol);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[0, 6]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Jol1.ToString();
        player1Pieces.Add(Enums.PieceSetting.Jol1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[2, 6]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Jol2.ToString();
        player1Pieces.Add(Enums.PieceSetting.Jol2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[4, 6]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Jol3.ToString();
        player1Pieces.Add(Enums.PieceSetting.Jol3, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[6, 6]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Jol4.ToString();
        player1Pieces.Add(Enums.PieceSetting.Jol4, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[8, 6]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Jol5.ToString();
        player1Pieces.Add(Enums.PieceSetting.Jol5, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[0, 3]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Jol1.ToString();
        player2Pieces.Add(Enums.PieceSetting.Jol1, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[2, 3]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Jol2.ToString();
        player2Pieces.Add(Enums.PieceSetting.Jol2, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[4, 3]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Jol3.ToString();
        player2Pieces.Add(Enums.PieceSetting.Jol3, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[6, 3]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Jol4.ToString();
        player2Pieces.Add(Enums.PieceSetting.Jol4, obj.GetComponent<Piece>());

        obj = GameObject.Instantiate<GameObject>(jol);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[8, 3]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Jol5.ToString();
        player2Pieces.Add(Enums.PieceSetting.Jol5, obj.GetComponent<Piece>());
    }
}
