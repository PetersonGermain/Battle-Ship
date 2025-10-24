using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;
using Unity.Services.Matchmaker.Models;
using Unity.Netcode.Transports.UTP;
using System.Collections;

public class GameManager : NetworkBehaviour
{

    public static GameManager singleton { get; private set; }
    public GameObject panelDeConnection;
    public GameObject panelAttente;

     public GameObject bateau;

  private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

  public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        NetworkManager.Singleton.OnClientConnectedCallback += OnNouveauClientConnecte;
    }

    private void OnNouveauClientConnecte(ulong obj)
    {
        if (!IsServer) return; // Only the server should control UI sync

        int playerCount = NetworkManager.Singleton.ConnectedClients.Count;

        UpdateUIClientRpc(playerCount);
    }

    [ClientRpc]
    private void UpdateUIClientRpc(int playerCount)
    {
        if (playerCount == 1)
        {
            panelDeConnection.SetActive(false);
            panelAttente.SetActive(true);
        }
        else if (playerCount == 2 || playerCount == 3)
        {
            panelDeConnection.SetActive(false);
            panelAttente.SetActive(true);
        }
        else if (playerCount >= 4)
        {
            panelDeConnection.SetActive(false);
            panelAttente.SetActive(false);
            ChargementSceneJeu();
        }
    }

    public void LanceCommeHote()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void LanceCommeClient()
    {
        NetworkManager.Singleton.StartClient();
    }

     public void ChargementSceneJeu()
   {
       NetworkManager.Singleton.SceneManager.LoadScene("Jeu", LoadSceneMode.Single);
   }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return; // each client only controls their own clicks

        // Detect touch or mouse click
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
            Input.GetMouseButtonDown(0))
        {
            Vector2 screenPos = Input.touchCount > 0
                ? Input.GetTouch(0).position
                : (Vector2)Input.mousePosition;

            // Create a ray from the camera through the click position
            Ray ray = Camera.main.ScreenPointToRay(screenPos);

            // We'll hit a flat plane (like ground or ocean)
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // y = 0 plane
            float distance;

            if (groundPlane.Raycast(ray, out distance))
            {
                Vector3 worldPos = ray.GetPoint(distance);
                // Ask the server to spawn the boat there
                SpawnBoatServerRpc(worldPos);
            }
        }
    }
    
    [ServerRpc]
    void SpawnBoatServerRpc(Vector3 position, ServerRpcParams rpcParams = default)
    {
        GameObject bateauCopie = Instantiate(bateau, position, Quaternion.identity);
        bateauCopie.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
    }
}

