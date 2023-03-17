using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum HexColorEnum 
{
    white, red, yellow, blue /*, violet, black, orange, green*/
}

public enum cellColorTransition 
{
    violet, black, orange, green
}

public class HexMapEditor : MonoBehaviour
{
    public Color[] colors;
    public Color[] colorsTansition;

    public HexGrid hexGrid;

    public int numColor;
    Color activeColor;

    private void Awake()
    {
        numColor = (int)HexColorEnum.white;
        SelectColor(numColor);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) // && !EventSystem.current.IsPointerOverGameObject())
        {
            HandleInput();
            SelectColor(numColor++);

            if (numColor >= Enum.GetValues(typeof(HexColorEnum)).Length)
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
            
            //se pinta tal grilla. 
            //sabes que grilla se pinto?
            //su vecino, de cada direccion, tiene mas de un color?

        }
    }

    public void SelectColor(int index)
    {
        activeColor = colors[index];
    }
}
