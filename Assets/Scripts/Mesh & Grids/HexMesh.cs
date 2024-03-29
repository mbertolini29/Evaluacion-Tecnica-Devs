using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    Mesh hexMesh;

    List<Vector3> vertices;
    List<int> triangles;
    List<Color> colors;

    MeshCollider meshCollider;

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.name = "Hex Mesh";

        vertices = new List<Vector3>();
        triangles = new List<int>();
        colors = new List<Color>();
        
        meshCollider = gameObject.AddComponent<MeshCollider>();   
    }

    public void Triangulate(HexCell[] cells)
    {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        colors.Clear();

        for (int i = 0; i < cells.Length; i++)
        {
            Triangulate(cells[i]);
        }

        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.colors = colors.ToArray();
        hexMesh.RecalculateNormals();

        //Desp de triangular le asignamos la malla  
        meshCollider.sharedMesh = hexMesh;
    }

    void Triangulate (HexCell cell)
    {
        for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
        {
            Triangulate(d, cell);
        }
    }

    void Triangulate (HexDirection direction, HexCell cell)
    {
        Vector3 center = cell.transform.localPosition;

        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                 center,
                 center + HexMetrics.GetFirstSolidCorner(direction),
                 center + HexMetrics.GetSecondSolidCorner(direction)
             //center + HexMetrics.corners[(int)direction],
             //center + HexMetrics.corners[(int)direction + 1]
             );
            // a ?? b  // .a != null ? a : b
            //color para cada triangulo
            HexCell prevNeighbor = cell.GetNighbor(direction.Previous()) ?? cell;
            HexCell neighbor = cell.GetNighbor(direction) ?? cell;
            HexCell nextNeighbor = cell.GetNighbor(direction.Next()) ?? cell;

            //Color edgeColor = (cell.color + neighbor.color) * 0.5f;

            AddTriangleColor(
                cell.color,
                (cell.color + prevNeighbor.color + neighbor.color) / 3f,
                (cell.color + neighbor.color + nextNeighbor.color) / 3f
            );
        }
    }



    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);        
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    void AddTriangleColor(Color c1, Color c2, Color c3)
    {
        colors.Add(c1);
        colors.Add(c2);
        colors.Add(c3);
    }
}
