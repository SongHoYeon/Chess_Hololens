using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCreater : MonoBehaviour
{
    [SerializeField]
    private Transform leftTopPoint;
    [SerializeField]
    private Transform leftBottomPoint;
    [SerializeField]
    private Transform rightTopPoint;
    [SerializeField]
    private Transform rightBottomPoint;

    [SerializeField]
    private Transform pointsParent;

    [SerializeField]
    private PieceManager pieceManager;

    public static Transform[,] pointTransformList;
    public static BoardPoint[,] pointCompList;

    public static float colInterval;
    public static float rowInterval;

    void Awake()
    {
        pointTransformList = new Transform[Defines.BoardProperty.COL_COUNT,Defines.BoardProperty.ROW_COUNT];
        pointCompList = new BoardPoint[Defines.BoardProperty.COL_COUNT, Defines.BoardProperty.ROW_COUNT];
    }

    void Start()
    {
        CreateVirtualPoints();
        pieceManager.CreatePiece();
    }

    private void CreateVirtualPoints()
    {
        colInterval = (rightTopPoint.localPosition.x - leftTopPoint.localPosition.x) / (Defines.BoardProperty.COL_COUNT - 1);
        rowInterval = (leftTopPoint.localPosition.z - leftBottomPoint.localPosition.z) / (Defines.BoardProperty.ROW_COUNT - 1);

        GameObject boardPointPrefab = Resources.Load<GameObject>("Prefab/Game/BoardPoint");

        for (int i = 0; i < Defines.BoardProperty.ROW_COUNT; i++)
        {
            for (int j = 0; j < Defines.BoardProperty.COL_COUNT; j++)
            {
                GameObject newPoint = GameObject.Instantiate(boardPointPrefab, Vector3.zero, Quaternion.identity);
                newPoint.transform.parent = pointsParent;   
                newPoint.transform.localPosition = new Vector3(leftTopPoint.localPosition.x + colInterval * j, 0, leftTopPoint.localPosition.z - rowInterval * i);
                newPoint.transform.localEulerAngles = Vector3.zero;
                newPoint.name = "BoardPoint_[" + i.ToString() + ", " + j.ToString() + "]";

                BoardPoint comp = newPoint.GetComponent<BoardPoint>();
                comp.Init(j, i);

                pointTransformList[j, i] = newPoint.transform;
                pointCompList[j, i] = comp;
            }
        }
    }
}
