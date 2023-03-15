using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[System.Serializable]
public struct HexCoordinates 
{
    [SerializeField] int x, z;

    public int X { get { return x; } }

    public int Z { get { return z; } }

    public int Y 
    { 
        get { return -X - Z; } 
    }

    public HexCoordinates (int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public static HexCoordinates FromPosition(Vector3 position)
    {
        //que coordinada pertenece a cada posicion
        float x = position.x / (HexMetrics.innerRadius * 2f);
        float y = -x;
        //cada 2 filas debemos desplazar una unidad completa hacia la izquierda
        float offset = position.z / (HexMetrics.outerRadius * 3f);
        x -= offset;
        y -= offset;
        //redondea al entero mas cerca.
        int iX = Mathf.RoundToInt(x); 
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x -y);
        
        if(iX + iY + iZ != 0)
        {
            //hay veces que las coordenadas no suman cero y esto lo advierte.
            //{ Debug.LogWarning("rounding error!"); }
         
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x -y - iZ);

            if(dX > dY && dX > dZ)
            {
                iX = -iX - iZ;
            }
            else if( dX > dY){
                iZ = -iX - iY;
            }
        }

        return new HexCoordinates(iX, iZ);
    }

    public static HexCoordinates FromOffsetCoordinates(int x, int z)
    {
        return new HexCoordinates(x - z / 2, z);
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")"; 
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }
}
