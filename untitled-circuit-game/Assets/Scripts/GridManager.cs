using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.Object GridCellPrefab;
    [SerializeField] private Material GridMat1;
    [SerializeField] private Material GridMat2;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float tickRate;
    private GameObject[,] GridCells;
    private void Awake()
    {
        GenerateTheGrid();
    }

    private void GenerateTheGrid()
    {
        GridCells = new GameObject[height, width];

        const int cellWidth = 1;
        const int cellHeight = 1;
        Vector3 startPos = gameObject.transform.position - new Vector3(width * cellWidth / 2, 0, (height * cellHeight) / 2);
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Vector3 spawnPos = startPos + new Vector3(j * cellWidth, 0, i * cellHeight);
                var newObj = Instantiate(GridCellPrefab, spawnPos, Quaternion.identity) as GameObject;
                var gridCell = newObj.GetComponent("GridCell") as GridCell;
                gridCell.SetIndices(i, j);
                newObj.transform.parent = gameObject.transform;
                newObj.name = string.Format("GridCell {0} {1}", i, j);
                GridCells[i, j] = newObj;
                var newObjMR = newObj.GetComponent<MeshRenderer>();
                if ((i + j) % 2 == 0)
                {
                    newObjMR.material = GridMat1;
                }
                else
                {
                    newObjMR.material = GridMat2;
                }
            }
        }
    }

    private void Update() {
        
    }

    public GameObject[,] GetGridCells()
    {
        return GridCells;
    }

    public GameObject GetGridCellFromIndex(int rowIndex, int columnIndex)
    {
        return GridCells[rowIndex, columnIndex];
    }

    public void UpdateAdjecentCellUp(int rowIndex, int columnIndex, bool powerState)
    {
        if (columnIndex < width - 1)
        {
            var upAdjecentCell = GridCells[rowIndex, columnIndex + 1].GetComponent("GridCell") as GridCell;
            upAdjecentCell.OnReceiveCellUpdate(powerState, 0);
        }
    }
    public void UpdateAdjecentCellDown(int rowIndex, int columnIndex, bool powerState)
    {
        if (columnIndex < width - 1)
        {
            var downAdjecentCell = GridCells[rowIndex, columnIndex + 1].GetComponent("GridCell") as GridCell;
            downAdjecentCell.OnReceiveCellUpdate(powerState, 1);
        }
    }
    public void UpdateAdjecentCellLeft(int rowIndex, int columnIndex, bool powerState)
    {
        if (columnIndex < width - 1)
        {
            var leftAdjecentCell = GridCells[rowIndex, columnIndex + 1].GetComponent("GridCell") as GridCell;
            leftAdjecentCell.OnReceiveCellUpdate(powerState, 2);
        }
    }
    public void UpdateAdjecentCellRight(int rowIndex, int columnIndex, bool powerState)
    {
        if (columnIndex < width - 1)
        {
            var rightAdjecentCell = GridCells[rowIndex, columnIndex + 1].GetComponent("GridCell") as GridCell;
            rightAdjecentCell.OnReceiveCellUpdate(powerState, 3);
        }
    }
}