using UnityEngine;
using Unity.Netcode;

public class Joueur : NetworkBehaviour
{

    public GameObject bateau;

    public override void OnNetworkSpawn() 
    {
        base.OnNetworkSpawn();

        if(IsServer) 
        {
            if(Input.GetMouseButtonDown(0)) 
            {
                Vector3 touchPosition = Input.mousePosition;

                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

                Instantiate(bateau, worldPosition, Quaternion.identity);
            }
        }

        else 
        {
            if(Input.GetMouseButtonDown(0)) 
            {
                Vector3 touchPosition = Input.mousePosition;

                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

                Instantiate(bateau, worldPosition, Quaternion.identity);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
