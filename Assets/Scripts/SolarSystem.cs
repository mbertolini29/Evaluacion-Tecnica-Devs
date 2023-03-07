using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : SolarSystemManager
{
    CelestialObjects celestialBody;
    
    public GameObject[] celestialGameObject;

    // Start is called before the first frame update
    void Start()
    {
        nameSolarSystem = "Sistema planetario";
        locationSolarSytem = new Vector3(0.0f, 0.0f, 0.0f);
        //un sistema sola tiene una lista de objetos celestiales
        celestialObjectsSS = new List<CelestialObjects>();

        //Creacion de cada objeto celestial.

        //1 estrella
        celestialBody = new CelestialObjects
        {
            celestialBodyGameObject = celestialGameObject[0],
            nameCelestialObjects = "Sol",
            radius = 696.34f, //696.340 km
            locationCelestialObjects = new Vector3(0.0f, 0.0f, 0.0f),
            //naturalSatellite = null
        };
        celestialObjectsSS.Add(celestialBody);

        //1º planeta
        celestialBody = new CelestialObjects
        {
            celestialBodyGameObject = celestialGameObject[1],
            nameCelestialObjects = "Venus",
            radius = 6.05f, // 6.051,8 km
            locationCelestialObjects = new Vector3(0.72f, 0.0f, 0.0f) //108 millones de Km
            //naturalSatellite = null;
        };
        celestialObjectsSS.Add(celestialBody);

        //2º planeta
        celestialBody = new CelestialObjects
        {
            celestialBodyGameObject = celestialGameObject[2],
            nameCelestialObjects = "Tierra",
            radius = 6.37f, //6.371 km
            locationCelestialObjects = new Vector3(1.0f, 0.0f, 0.0f), //149,6 millones de Km
            //naturalSatellite = null;
        };

        celestialObjectsSS.Add(celestialBody);
                
        //3º planeta
        celestialBody = new CelestialObjects
        {
            celestialBodyGameObject = celestialGameObject[3],
            nameCelestialObjects = "Marte",
            radius = 3.39f, //3.389,5 km
            locationCelestialObjects = new Vector3(1.52f, 0.0f, 0.0f),  //249,1f millones de Km
            naturalSatellite = new List<CelestialObjects>(),
        };

        //Un satélite natural es un cuerpo celeste que orbita alrededor de un planeta
        // 2 lunas respetivamente del ultimo planeta agregado, en este caso Marte.

        //1º luna de marte
        celestialBody.naturalSatellite.Add(
            new CelestialObjects()
            {
                celestialBodyGameObject = celestialGameObject[4],
                nameCelestialObjects = "Fobos",
                radius = 0.11f, //11,267 km
                locationCelestialObjects = new Vector3(0.16f, 0.0f, 0.0f),            
            }
        );

        //2º luna de marte
        celestialBody.naturalSatellite.Add(
            new CelestialObjects()
            {
                celestialBodyGameObject = celestialGameObject[5],
                nameCelestialObjects = "Deimos",
                radius = 0.06f, //6,2 km 
                locationCelestialObjects = new Vector3(0.35f, 0.0f, 0.0f), //23.500 km.de marte 
            }
        );

        celestialObjectsSS.Add(celestialBody);
        //Debug.Log(celestialObjectsSS);

        // Unidad Astronómica (au)
        // 1 au = 149 597 870 700 metros

        foreach (CelestialObjects item in celestialObjectsSS)
        {
            Instantiate(item.celestialBodyGameObject, item.locationCelestialObjects, Quaternion.identity);
        } 

        foreach (CelestialObjects item in celestialBody.naturalSatellite)
        {
            Instantiate(item.celestialBodyGameObject,
                        item.locationCelestialObjects + celestialObjectsSS[3].locationCelestialObjects, 
                        Quaternion.identity);

            //celestialObjectsSS[3].celestialBodyGameObject.gameObject.transform.parent = item.celestialBodyGameObject.gameObject.transform;
        }        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
