using UnityEngine;

abstract public class CircuitElement : MonoBehaviour
{
    protected int rowIndex;
    protected int columnIndex;
    [SerializeField] protected GameObject grid;
    protected GridManager gridManager;
    protected bool isConnectedToGrid = false;
    protected bool elementIsPowered = false;

    /// <summary>
    /// The mask that is used to determine which cells to send updates to.
    /// Or in other words, which cells can the current cell propogate power to.
    /// Format: 0bABCD
    /// A: 1 if the cell can power the cell above it
    /// B: 1 if the cell can power the cell below it
    /// C: 1 if the cell can power the cell to the left of it
    /// D: 1 if the cell can power the cell to the right of it
    /// </summary>
    protected int connectionSendMask;

    /// <summary>
    /// The mask that is used to determine which cells to accept updates from.
    /// Or in other words, which surrounding cells can propogate power to the current cell.
    /// Format: 0bABCD
    /// A: 1 if the cell above this cell can power this cell
    /// B: 1 if the cell below this cell can power this cell
    /// C: 1 if the cell to the left of this cell can power this cell
    /// D: 1 if the cell to the right of this cell can power this cell
    /// </summary>
    protected int connectionReceiveMask;

    protected void Awake()
    {
        connectionReceiveMask = 0b0000;
        connectionSendMask = 0b0000;
        gridManager = grid.GetComponent("GridManager") as GridManager;
    }
    protected void Start()
    {

    }

    protected void Update()
    {

    }
    /// <summary>
    /// Called when the cell that this element is attached to is updated
    /// </summary>
    /// <param name="updateMask">
    /// updateMask is of the following format: 0bABCD
    /// A: 1 if the cell above this cell is powering this cell
    /// B: 1 if the cell below this cell is powering this cell
    /// C: 1 if the cell to the left of this cell is powering this cell
    /// D: 1 if the cell to the right of this cell is powering this cell
    /// </param>
    public void onReceiveCellUpdate(int receivedUpdateMask)
    {
        if (isConnectedToGrid)
        {
            int effectiveUpdateMark = receivedUpdateMask & connectionReceiveMask;
            if (effectiveUpdateMark != 0)
            {
                if (!elementIsPowered)
                {
                    // current cell was unpowered and is now powered
                    onRisingEdge(effectiveUpdateMark);
                }
            }
            else
            {
                if (elementIsPowered)
                {
                    // current cell was powered and is now unpowered
                    onFallingEdge(effectiveUpdateMark);
                }
            }
        }
    }

    public void sendCellUpdate(int sendUpdateMask)
    {
        if (isConnectedToGrid)
        {
            gridManager.updateAdjecentCells(rowIndex, columnIndex, sendUpdateMask);
        }
    }

    /// <summary>
    /// Called when the this element changes from a unpowered to powered state
    /// </summary>
    /// <param name="updateMask">
    /// updateMask is of the following format: 0bABCD
    /// 
    /// </param>
    abstract public void onRisingEdge(int updateMask);
    /// <summary>
    /// Called when the this element changes from a powered to unpowered state
    /// </summary>
    abstract public void onFallingEdge(int updateMask);

    private void setIndices(int rowIndex, int columnIndex)
    {
        this.rowIndex = rowIndex;
        this.columnIndex = columnIndex;
    }

    public void detachFromGrid()
    {
        isConnectedToGrid = false;
        rowIndex = -1;
        columnIndex = -1;
    }

    public void attachToGrid(GridCell gridCell)
    {
        isConnectedToGrid = true;
        var rowIndex = 0;
        var columnIndex = 0;
        gridCell.getIndices(out rowIndex, out columnIndex);
        setIndices(rowIndex, columnIndex);
    }
}