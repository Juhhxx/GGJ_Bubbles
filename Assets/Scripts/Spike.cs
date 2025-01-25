using Unity.VisualScripting;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Bubble bubble = other.GetComponent<Bubble>();
        if (bubble != null)
            bubble.Pop();

        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
