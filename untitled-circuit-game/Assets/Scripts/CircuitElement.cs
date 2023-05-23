using UnityEngine;

public class CircuitElement : MonoBehaviour
{
    public GridCell gridCell;
    private void Start()
    {

    }

    private void Update()
    {

    }

    public void setGridCell(GridCell gridCell)
    {
        this.gridCell = gridCell;
        gridCell.circuitElement = this;
    }

    public void detachFromGrid()
    {
        if (gridCell != null)
        {
            gridCell.circuitElement = null;
            gridCell = null;
        }
    }

}