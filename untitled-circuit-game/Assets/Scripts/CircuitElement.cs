using UnityEngine;

abstract public class CircuitElement : MonoBehaviour
{
    protected int rowIndex;
    protected int columnIndex;
    [SerializeField] protected GameObject grid;
    protected GridManager gridManager;
    protected bool isConnectedToGrid = false;
    protected bool elementIsPowered = false;

    protected int nextTickUpdateMask = 0b0000;

    protected void Awake()
    {
        gridManager = grid.GetComponent("GridManager") as GridManager;
    }
    protected void Start()
    {
        
    }

    protected void Update()
    {

    }
    /// <summary>
    /// Update `nextTickUpdateMask` for the next tick
    /// </summary>
    /// <param name="powerState"></param>
    /// true - on
    /// false - off
    /// <param name="receivingFrom"></param>
    /// 0 - up
    /// 1 - down
    /// 2 - left
    /// 3 - right
    public void OnReceiveCellUpdate(bool powerState, int receivingFrom)
    {
        if (isConnectedToGrid)
        {
            if(powerState) //if on, set 
            {
                nextTickUpdateMask |= 0b1000 >> receivingFrom;
            }
            else // if off, then add the recivingFrom to the update mask
            {
                nextTickUpdateMask &= ~(0b1000 >> receivingFrom);
            }
        }
    }

    /// <summary>
    /// How the element should update the cells that it is attached to
    /// </summary>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    abstract public int OnTickCellOutput();

    protected void SendCellUpdateUp(bool powerState)
    {
        if (isConnectedToGrid)
        {
            gridManager.UpdateAdjecentCellUp(rowIndex, columnIndex, powerState);
        }
    }
    protected void SendCellUpdateDown(bool powerState)
    {
        if (isConnectedToGrid)
        {
            gridManager.UpdateAdjecentCellDown(rowIndex, columnIndex, powerState);
        }
    }
    protected void SendCellUpdatlLeft(bool powerState)
    {
        if (isConnectedToGrid)
        {
            gridManager.UpdateAdjecentCellLeft(rowIndex, columnIndex, powerState);
        }
    }
    protected void SendCellUpdateRight(bool powerState)
    {
        if (isConnectedToGrid)
        {
            gridManager.UpdateAdjecentCellRight(rowIndex, columnIndex, powerState);
        }
    }

    private void SetIndices(int rowIndex, int columnIndex)
    {
        this.rowIndex = rowIndex;
        this.columnIndex = columnIndex;
    }

    public void DetachFromGrid()
    {
        isConnectedToGrid = false;
        rowIndex = -1;
        columnIndex = -1;
    }

    public void AttachToGrid(GridCell gridCell)
    {
        isConnectedToGrid = true;
        gridCell.GetIndices(out int rowIndex, out int columnIndex);
        SetIndices(rowIndex, columnIndex);
    }
}