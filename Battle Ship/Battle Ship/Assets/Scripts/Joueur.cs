using UnityEngine;
using Unity.Netcode;

public class Joueur : NetworkBehaviour
{
    public GameObject bateau; // assign in inspector

    void Update()
    {
    //     if (!IsOwner) return; // each client only controls their own clicks

    //     // Detect touch or mouse click
    //     if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
    //         Input.GetMouseButtonDown(0))
    //     {
    //         Vector2 screenPos = Input.touchCount > 0 
    //             ? Input.GetTouch(0).position 
    //             : (Vector2)Input.mousePosition;

    //         // Create a ray from the camera through the click position
    //         Ray ray = Camera.main.ScreenPointToRay(screenPos);

    //         // We'll hit a flat plane (like ground or ocean)
    //         Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // y = 0 plane
    //         float distance;

    //         if (groundPlane.Raycast(ray, out distance))
    //         {
    //             Vector3 worldPos = ray.GetPoint(distance);
    //             // Ask the server to spawn the boat there
    //             SpawnBoatServerRpc(worldPos);
    //         }
    //     }
    }

    // [ServerRpc]
    // void SpawnBoatServerRpc(Vector3 position, ServerRpcParams rpcParams = default)
    // {
    //     GameObject bateauCopie = Instantiate(bateau, position, Quaternion.identity);
    //     bateauCopie.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
    // }
}
