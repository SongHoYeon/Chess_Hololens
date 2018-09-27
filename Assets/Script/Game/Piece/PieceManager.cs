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

    public static Dictionary<Enums.PieceSetting, GameObject> player1Pieces;
    public static Dictionary<Enums.PieceSetting, GameObject> player2Pieces;

    void Awake()
    {
        player1Pieces = new Dictionary<Enums.PieceSetting, GameObject>();
        player2Pieces = new Dictionary<Enums.PieceSetting, GameObject>();
    }

    public void CreatePiece()
    {
        GameObject obj = null;

        // 킹
        obj = GameObject.Instantiate<GameObject>(king);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[4, 1]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.King.ToString();
        player1Pieces.Add(Enums.PieceSetting.King, obj);

        obj = GameObject.Instantiate<GameObject>(king);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[4, 8]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.King.ToString();
        player2Pieces.Add(Enums.PieceSetting.King, obj);

        // 사
        obj = GameObject.Instantiate<GameObject>(sha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[3, 0]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Sha1.ToString();
        player1Pieces.Add(Enums.PieceSetting.Sha1, obj);

        obj = GameObject.Instantiate<GameObject>(sha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[5, 0]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Sha2.ToString();
        player1Pieces.Add(Enums.PieceSetting.Sha2, obj);

        obj = GameObject.Instantiate<GameObject>(sha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[3, 9]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Sha1.ToString();
        player2Pieces.Add(Enums.PieceSetting.Sha1, obj);

        obj = GameObject.Instantiate<GameObject>(sha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[5, 9]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Sha2.ToString();
        player2Pieces.Add(Enums.PieceSetting.Sha2, obj);

        // 차
        obj = GameObject.Instantiate<GameObject>(cha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[0, 0]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Cha1.ToString();
        player1Pieces.Add(Enums.PieceSetting.Cha1, obj);

        obj = GameObject.Instantiate<GameObject>(cha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[8, 0]);
        obj.transform.name = "Player1_" + Enums.PieceSetting.Cha2.ToString();
        player1Pieces.Add(Enums.PieceSetting.Cha2, obj);

        obj = GameObject.Instantiate<GameObject>(cha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[0, 9]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Cha1.ToString();
        player2Pieces.Add(Enums.PieceSetting.Cha1, obj);

        obj = GameObject.Instantiate<GameObject>(cha);
        obj.transform.parent = pieceParent;
        obj.GetComponent<Piece>().Init(PointCreater.pointCompList[8, 9]);
        obj.transform.name = "Player2_" + Enums.PieceSetting.Cha2.ToString();
        player2Pieces.Add(Enums.PieceSetting.Cha2, obj);

        //// 포
        //obj = GameObject.Instantiate<GameObject>(pho);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[1, 2]);
        //player1Pieces.Add(Enums.PieceSetting.Pho1, obj);

        //obj = GameObject.Instantiate<GameObject>(pho);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[7, 1]);
        //player1Pieces.Add(Enums.PieceSetting.Pho2, obj);

        //obj = GameObject.Instantiate<GameObject>(pho);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[1, 7]);
        //player2Pieces.Add(Enums.PieceSetting.Pho1, obj);

        //obj = GameObject.Instantiate<GameObject>(pho);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[7, 7]);
        //player2Pieces.Add(Enums.PieceSetting.Pho2, obj);

        //// 마
        //obj = GameObject.Instantiate<GameObject>(mha);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[2, 0]);
        //player1Pieces.Add(Enums.PieceSetting.Mha1, obj);

        //obj = GameObject.Instantiate<GameObject>(mha);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[6, 0]);
        //player1Pieces.Add(Enums.PieceSetting.Mha2, obj);

        //obj = GameObject.Instantiate<GameObject>(mha);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[2, 9]);
        //player2Pieces.Add(Enums.PieceSetting.Mha1, obj);

        //obj = GameObject.Instantiate<GameObject>(mha);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[6, 9]);
        //player2Pieces.Add(Enums.PieceSetting.Mha2, obj);

        //// 상
        //obj = GameObject.Instantiate<GameObject>(shang);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[1, 0]);
        //player1Pieces.Add(Enums.PieceSetting.Shang1, obj);

        //obj = GameObject.Instantiate<GameObject>(shang);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[7, 0]);
        //player1Pieces.Add(Enums.PieceSetting.Shang2, obj);

        //obj = GameObject.Instantiate<GameObject>(shang);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[1, 9]);
        //player2Pieces.Add(Enums.PieceSetting.Shang1, obj);

        //obj = GameObject.Instantiate<GameObject>(shang);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[7, 9]);
        //player2Pieces.Add(Enums.PieceSetting.Shang2, obj);

        //// 졸
        //obj = GameObject.Instantiate<GameObject>(jol);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[0, 3]);
        //player1Pieces.Add(Enums.PieceSetting.Jol1, obj);

        //obj = GameObject.Instantiate<GameObject>(jol);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[2, 3]);
        //player1Pieces.Add(Enums.PieceSetting.Jol2, obj);

        //obj = GameObject.Instantiate<GameObject>(jol);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[4, 3]);
        //player1Pieces.Add(Enums.PieceSetting.Jol3, obj);

        //obj = GameObject.Instantiate<GameObject>(jol);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[6, 3]);
        //player1Pieces.Add(Enums.PieceSetting.Jol4, obj);

        //obj = GameObject.Instantiate<GameObject>(jol);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[8, 3]);
        //player1Pieces.Add(Enums.PieceSetting.Jol5, obj);

        //obj = GameObject.Instantiate<GameObject>(jol);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[0, 6]);
        //player2Pieces.Add(Enums.PieceSetting.Jol1, obj);

        //obj = GameObject.Instantiate<GameObject>(jol);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[2, 6]);
        //player2Pieces.Add(Enums.PieceSetting.Jol2, obj);

        //obj = GameObject.Instantiate<GameObject>(jol);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[4, 6]);
        //player2Pieces.Add(Enums.PieceSetting.Jol3, obj);

        //obj = GameObject.Instantiate<GameObject>(jol);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[6, 6]);
        //player2Pieces.Add(Enums.PieceSetting.Jol4, obj);

        //obj = GameObject.Instantiate<GameObject>(jol);
        //obj.transform.parent = pieceParent;
        //obj.GetComponent<Piece>().Init(PointCreater.pointCompList[8, 6]);
        //player2Pieces.Add(Enums.PieceSetting.Jol5, obj);
    }
}
