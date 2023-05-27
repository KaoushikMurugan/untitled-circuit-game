using UnityEngine;

public class Wire : CircuitElement
{
    override public void onFallingEdge(int updateMask)
    {
        // Unpower adjecent cells
    }

    override public void onRisingEdge(int updateMask)
    {
        // power adjecent cells
    }

    private void updateState(bool isPowered, int connection)
    {
        elementIsPowered = isPowered;
    }
}