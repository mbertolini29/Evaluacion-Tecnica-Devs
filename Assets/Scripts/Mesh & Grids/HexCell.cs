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
    //metodo de extension.
    public static HexDirection Opposite (this HexDirection direction)
    {
        return (int)direction < 3 ? (direction + 3) : (direction - 3);
    }

    public static HexDirection Previous (this HexDirection direction)
    {
        return direction == HexDirection.NE ? HexDirection.NW : (direction - 1);
    }

    public static HexDirection Next (this HexDirection direction)
    {
        return direction == HexDirection.NW ? HexDirection.NE : (direction + 1);
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
        cell.neighbors[(int)direction.Opposite()] = this;
    }    

    //controlar vecinos y sus colores.
    //public void 
}
