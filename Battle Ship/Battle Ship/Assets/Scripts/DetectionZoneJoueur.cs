using UnityEngine;
using Unity.Netcode;

public class DetectionZoneJoueur : MonoBehaviour
{
    private Collider zoneToucher;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {

        //     // Create a ray from the camera through the click position
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;

        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         if (hit.collider == GetComponent<Collider>())
        //         {
        //             Debug.Log($"Clicked on {gameObject.name}");
        //         }
        //     }
        // }
    }
    
      void OnMouseUp()
    {
        Debug.Log("MouseUp");
    }
}
