using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Preloader : MonoBehaviour
{
    private void Awake()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++) 
        {
            if (args[i] == "-launch-as-client")
                OnClientClick();
            else if (args[i] == "-launch-as-server")
                OnServerClick();
        }
    }

    public void OnClientClick()
    {
        NetworkManager.Singleton.StartClient();
    }
    
    public void OnServerClick()
    {
        NetworkManager.Singleton.StartServer();
    }
}
