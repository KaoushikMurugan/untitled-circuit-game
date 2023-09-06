using UnityEngine;

public class Wire : CircuitElement
{
    // 0bABCD
    // A - up
    // B - down
    // C - left
    // D - right
    public int wireConnections = 0b0000;
    public bool powerState = false;
    public override int OnTickCellOutput()
    {
        powerState = nextTickUpdateMask != 0b0000;
        UpdateTexture();
        return powerState ? 0b1111 : 0b0000;
    }

    public void UpdateTexture()
    {
        if (powerState)
        {
            // TODO
        }
        else
        {
            // TODO
        }
    }
}