using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject Door;
    public void OpenDoor()
    {
        if(Door != null)
        {
            
            Destroy(Door);
        }
    }
}
