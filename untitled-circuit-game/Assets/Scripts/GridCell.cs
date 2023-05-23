using UnityEngine;

public class GridCell : MonoBehaviour
{
    // The element that this cell is holding
    public CircuitElement circuitElement;
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
            setCircuitElement(circuitElement);
        }
    }

    public void setIndices(int rowIndex, int columnIndex)
    {
        this.rowIndex = rowIndex;
        this.columnIndex = columnIndex;
    }

    public void setCircuitElement(CircuitElement circuitElement)
    {
        this.circuitElement = circuitElement;
        circuitElement.gridCell = this;
    }

    public bool hasCircuitElement()
    {
        return circuitElement != null;
    }

    public void removeCircuitElement()
    {
        if (circuitElement != null)
        {
            circuitElement.gridCell = null;
            circuitElement = null;
        }
    }
}