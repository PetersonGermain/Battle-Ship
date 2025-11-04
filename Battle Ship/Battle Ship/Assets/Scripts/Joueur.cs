using UnityEngine;
using Unity.Netcode;

public class DetectionClick : NetworkBehaviour
{
    public GameObject bateau; // assign in inspector

    public NetworkVariable<bool> spawnBateau = new NetworkVariable<bool>(true);
    public NetworkVariable<bool> spawnExplosion = new NetworkVariable<bool>(false);

    private Collider zoneSpawn;
    public Collider zoneJoueur1;
    public Collider zoneJoueur2;
    public Collider zoneJoueur3;
    public Collider zoneJoueur4;
    private Collider zoneToucher;

    // 👇 Use OnNetworkSpawn instead of Start, because OwnerClientId isn’t valid in Start
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // Always assign on both client AND server
        AssignZoneToPlayer((int)OwnerClientId);

        if (IsOwner)
        {
            Debug.Log($"[Client {OwnerClientId}] zone assigned: {zoneSpawn?.name}");
        }

        if (IsServer)
        {
            Debug.Log($"[Server] zone assigned for player {OwnerClientId}: {zoneSpawn?.name}");
        }
    }

    void AssignZoneToPlayer(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0:
                zoneSpawn = zoneJoueur1;
                break;
            case 1:
                zoneSpawn = zoneJoueur2;
                break;
            case 2:
                zoneSpawn = zoneJoueur3;
                break;
            case 3:
                zoneSpawn = zoneJoueur4;
                break;
            default:
                Debug.LogWarning($"Aucune zone définie pour le joueur {playerIndex}");
                break;
        }
    }

    void Update()
    {
        if (!IsOwner) return; // only control own clicks
        if (!spawnBateau.Value) return; // already spawned, stop here

        // Detect touch or mouse click
        if (Input.GetMouseButtonDown(0))
        {

            // Create a ray from the camera through the click position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                zoneToucher = hit.collider;
                Debug.Log("Clicked on: " + zoneToucher.gameObject.name);
                // if(hit.collider == zoneSpawn)
                // {
                //     Vector3 worldPos = hit.point;
                //     SpawnBoatServerRpc(worldPos);
                //     Debug.Log($"Client {OwnerClientId} spawned boat in their zone!");
                // }
            }

            // Debug.Log($"Client {OwnerClientId} clicked at screen position: {Input.mousePosition}");
        }


    }
    

    [ServerRpc]
    void SpawnBoatServerRpc(Vector3 position, ServerRpcParams rpcParams = default)
    {
        if (zoneSpawn == null)
        {
            Debug.LogWarning($"Client {OwnerClientId} has no assigned zone!");
            return;
        }

        // ✅ Check if the spawn position is inside the player's zone
        if (!zoneSpawn.bounds.Contains(position))
        {
            Debug.LogWarning($"Client {OwnerClientId} tried to spawn outside their zone!");
            return;
        }

        spawnBateau.Value = false; // disable further spawns
        GameObject bateauCopie = Instantiate(bateau, position, Quaternion.identity);
        bateauCopie.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);

        Debug.Log($"✅ Boat spawned for player {OwnerClientId} at {position}");
    }
}
