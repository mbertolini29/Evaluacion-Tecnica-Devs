using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour
{
    public Color[] colors;

    public HexGrid hexGrid;

    public int numColor = 0;
    Color activeColor;

    private void Awake()
    {
        SelectColor(numColor);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) // && !EventSystem.current.IsPointerOverGameObject())
        {
            HandleInput();
            SelectColor(numColor++);
            if (numColor >= 4)
            {
                numColor = 0;
            }
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);   
        RaycastHit hit;
        if(Physics.Raycast(inputRay, out hit) )
        {
            hexGrid.ColorCell(hit.point, activeColor);
         
        }
    }

    public void SelectColor(int index)
    {
        activeColor = colors[index];
    }
}
