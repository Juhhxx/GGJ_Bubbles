using Unity.VisualScripting;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Bubble bubble = other.GetComponent<Bubble>();
        if (bubble != null)
            bubble.Pop();

        PlayerInput player = other.gameObject.GetComponent<PlayerInput>();

        if (player != null && player.CanAffect)
            player.Hurt(player.transform.position - transform.position);
    }   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
