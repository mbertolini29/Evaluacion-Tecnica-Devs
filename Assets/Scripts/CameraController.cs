using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player Point")]
    public PlayerNetwork target;
    //public GameObject targetPlayer;

    private void Start()
    {
        //targetPlayer = GameObject.FindGameObjectWithTag("Player");
        //PlayerNetwork playerScript = targetPlayer.GetComponent <PlayerNetwork>();
    }

    //para que el movimiento de la camara, sea desp de q el jugador se haya movido
    void LateUpdate()
    {
        if(GameObject.FindObjectOfType<PlayerNetwork>() != null)
        {
            target = GameObject.FindObjectOfType<PlayerNetwork>();
            transform.position = new Vector3 (target.transform.position.x, 
                                              target.camTransform.transform.position.y,
                                              target.camTransform.transform.position.z);
            transform.rotation = target.transform.rotation;
        }
    }
}
