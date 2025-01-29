using UnityEngine;

public class Bounce : MonoBehaviour
{
    private Shaker _shaker;
    [SerializeField] private Animator _animator;
    private void Start()
    {
        _shaker = FindFirstObjectByType<Shaker>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered bounce. ");

        Bubble bbl = other.gameObject.GetComponentInParent<Bubble>();
        PlayerInput player = other.gameObject.GetComponentInParent<PlayerInput>();

        if (bbl != null || player != null)
        {
            _animator.SetTrigger("Hit");

            Rigidbody rigidbody = other.gameObject.GetComponentInParent<Rigidbody>();

            _shaker.Shake(0.1f, 20f);

            Debug.Log("bouncing: " + rigidbody.linearVelocity);
            if (rigidbody != null)
                rigidbody.linearVelocity *= -1.2f;
            
            if ( other.gameObject.GetComponentInParent<PlayerInput>() != null )
                player.Move(rigidbody.linearVelocity * 3f);

            Debug.Log("after bouncing: " + rigidbody.linearVelocity);
        }
    }
}
