using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; // for the buttons
using Unity.Netcode; // to control the NetworkManager

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    private void Awake() {
        // Start a server
        serverButton.onClick.AddListener(() => {
            Debug.Log("Button clicked");
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
