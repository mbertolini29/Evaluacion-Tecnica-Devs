using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] Button serverBtn; //inicia el servidor, pero no jugadores
    [SerializeField] Button hostBtn; //inicia el servidor y cliente
    [SerializeField] Button clientBtn; //jugador simple que se conecta a servidores. 

    private void Awake()
    {
        serverBtn.onClick.AddListener( () => {
            NetworkManager.Singleton.StartServer();
        });

        hostBtn.onClick.AddListener( () => {
            NetworkManager.Singleton.StartHost();
        });

        clientBtn.onClick.AddListener( () => {
            NetworkManager.Singleton.StartClient();
        });

        
    }

    public void StopServer()
    {
        NetworkManager.Singleton.Shutdown();
        //Destroy(NetworkManager.Singleton.gameObject);
    }
}
