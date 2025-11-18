using UnityEngine;

public class Dies : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
public void Destroy()
    {
        Debug.Log("dies");
        Destroy(gameObject);
    }
}
