using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//noreste, este, sureste, suroeste, oeste y noroeste
public enum HexDirection
{
    NE, E, SE, SW, W, NW
}

public static class HexDirectionExtensions
{
    public static HexDirection opposite (this HexDirection direction)
    {
        return (int)direction < 3 ? (direction + 3) : (direction - 3);
    }
}

public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;

    public Color color;

    [SerializeField] HexCell[] neighbors;

    //recupera al vecino de una celda, en un direccion
    public HexCell GetNighbor (HexDirection direction)
    {
        return neighbors[(int)direction];
    }

    //establecer un vecino
    public void SetNeighbor(HexDirection direction, HexCell cell)
    {
        neighbors[(int)direction] = cell;
        //los vecinos son bidireccionales, por lo tanto configuras la direccion opuesta.
        //cell.neighbors[(int)direction.Opposite()] = this;

    }

    
}
