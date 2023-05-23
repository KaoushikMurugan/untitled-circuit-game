using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.Object GridCellPrefab;
    [SerializeField] private Material GridMat1;
    [SerializeField] private Material GridMat2;
    [SerializeField] private int width = 11;
    [SerializeField] private int height = 11;
    private GameObject[,] GridCells;
    private void Start()
    {
        GridCells = new GameObject[height, width];

        const int cellWidth = 1;
        const int cellHeight = 1;
        Vector3 startPos = this.gameObject.transform.position - new Vector3((width * cellWidth) / 2, 0, (height * cellHeight) / 2);
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Vector3 spawnPos = startPos + new Vector3(j * cellWidth, 0, i * cellHeight);
                var newObj = GameObject.Instantiate(GridCellPrefab, spawnPos, Quaternion.identity) as GameObject;
                var gridCell = newObj.GetComponent("GridCell") as GridCell;
                gridCell.setIndices(i, j);
                newObj.transform.parent = this.gameObject.transform;
                newObj.name = String.Format("GridCell {0} {1}", i, j);
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

    public GameObject[,] getGridCells()
    {
        return GridCells;
    }

    public GameObject getGridCellFromIndex(int rowIndex, int columnIndex)
    {
        return GridCells[rowIndex, columnIndex];
    }
}