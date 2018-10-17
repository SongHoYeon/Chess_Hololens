using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPoint : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    private int colIdx;
    public int GetXPos() { return colIdx; }
    private int rowIdx;
    public int GetYPos() { return rowIdx; }

    public void Init(int colIdx, int rowIdx)
    {
        this.colIdx = colIdx;
        this.rowIdx = rowIdx;

        if (rowIdx == 0)
            DrawColLine();
        if (colIdx == 0)
            DrawRowLine();
    }

    private void DrawColLine()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(1, new Vector3(0, 0, - PointCreater.rowInterval * (Defines.BoardProperty.ROW_COUNT - 1)));
    }

    private void DrawRowLine()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(1, new Vector3(PointCreater.colInterval * (Defines.BoardProperty.COL_COUNT - 1), 0, 0));
    }
}
