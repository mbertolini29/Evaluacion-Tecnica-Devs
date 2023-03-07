using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObjects : MonoBehaviour
{
    public GameObject celestialBodyGameObject;    
    public string nameCelestialObjects;
    public float radius;
    //public float Mass;
    //public float Volumen;
    //public float Distance;
    //public bool clockwise; //agujas del reloj. teoricamente giran en contra del reloj los planetas.
    public Vector3 locationCelestialObjects;
    public List<CelestialObjects> naturalSatellite;
}