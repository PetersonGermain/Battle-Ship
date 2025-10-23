using UnityEngine;
using UnityEngine.SceneManagement;

public class LancementPartie : MonoBehaviour
{
    public static LancementPartie instance;

    void Awake()
    {
       if (instance == null)
       {
           instance = this;
       }
       else
       {
           Destroy(gameObject);
       }
    }

   void Start()
   {
       SceneManager.LoadScene("LobbyRelay");
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
