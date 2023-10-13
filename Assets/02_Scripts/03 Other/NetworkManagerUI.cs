using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    private void Avake() {
        // Start a server
        serverButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
        });
        // Start a host 
        hostButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });
        // Start a client
        clientButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
        });
    }
}
