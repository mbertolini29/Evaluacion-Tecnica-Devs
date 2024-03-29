using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HexGrid : MonoBehaviour
{
    [Header("Grid Construction")]
    public int width = 6;
    public int height = 6;
    public HexCell cellPrefab;
    HexCell[] cells;

    [Header("Showing coordinates")]
    [SerializeField] Canvas gridCanvas;
    public Text cellLabelPrefab;

    [Header("Rendering Hexagons")]
    [SerializeField] HexMesh hexMesh;

    [Header("Coloring Hexes")]
    public Color defaultColor = Color.white;
    //public Color color;
    //public Color touchedColor = Color.magenta;

    private void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[width * height];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    private void Start()
    {
        hexMesh.Triangulate(cells);
    }

    public void ColorCell(Vector3 position, Color color)
    {
        position = transform.InverseTransformDirection(position);
        //para saber que celda tocamos
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        //Debug.Log("touched at " + coordinates.ToString());
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        cell.color = color;

        //para mi aca, controlaria el color de las celdas en transicion.
        //la preg es como?

        //tenes que controlar si el vecino de la derecha tiene un vecino con color, 
        //tiene color,
        //si es asi, cual? 
        //anotarlo
        //el vecino diagonal tiene color?
        //pintar al vecino con el color de transicion.
        //

        hexMesh.Triangulate(cells);
    }

    void TransitionCell()
    {

    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.color = defaultColor;

        if (x > 0) //Los vecinos este y oeste est�n conectados.
        {
            cell.SetNeighbor(HexDirection.W, cells[i - 1]);
        }
        if (z > 0)
        {
            if ((z & 1) == 0)
            {
                cell.SetNeighbor(HexDirection.SE, cells[i - width]);
                //conectarnos a los vecinos SW.
                //Excepto por la primera celda de cada fila, ya que no la tiene
                if (x > 0) 
                {
                    cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
                }
            }
            else
            {   //ayuda a conectar los vecinos restantes
                cell.SetNeighbor(HexDirection.SW, cells[i - width]);
                if(x < width - 1)
                {
                    cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]);
                }
            }

        }

        //etiqueta
        Text label = Instantiate<Text>(cellLabelPrefab);    
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();
    }
}
