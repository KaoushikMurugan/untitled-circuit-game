using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    // The element that this cell is holding
    private CircuitElement circuitElement;
    private int rowIndex;
    private int columnIndex;

    private void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(
            transform.position,
            Vector3.up,
            out hit,
            2,
            LayerMask.GetMask("DragableCircuitElements")
        ))
        {
            var circuitElement = hit.collider.gameObject.GetComponent("CircuitElement") as CircuitElement;
            SetCircuitElement(circuitElement);
        }
    }

    public void SetIndices(int rowIndex, int columnIndex)
    {
        this.rowIndex = rowIndex;
        this.columnIndex = columnIndex;
    }

    public void GetIndices(out int rowIndex, out int columnIndex)
    {
        rowIndex = this.rowIndex;
        columnIndex = this.columnIndex;
    }

    public void SetCircuitElement(CircuitElement circuitElement)
    {
        this.circuitElement = circuitElement;
        circuitElement.AttachToGrid(this);
    }

    public bool HasCircuitElement()
    {
        return circuitElement != null;
    }

    public void RemoveCircuitElement()
    {
        if (circuitElement != null)
        {
            circuitElement.DetachFromGrid();
            circuitElement = null;
        }
    }

    public void OnReceiveCellUpdate(bool powerState, int receivingFrom)
    {
        if (circuitElement != null)
        {
            circuitElement.OnReceiveCellUpdate(powerState, receivingFrom);
        }
    }
}