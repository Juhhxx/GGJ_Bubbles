using UnityEngine;

public class Bounce : MonoBehaviour
{
    private Shaker _shaker;
    void Start()
    {
        _shaker = FindFirstObjectByType<Shaker>();
    }

    // Update is called once per frame
    private void OnCollisionStay(Collider other)
    {
        Rigidbody rigidbody = other.gameObject.GetComponentInParent<Rigidbody>();

        _shaker.Shake(0.2f, 30f);

        rigidbody.linearVelocity *= 20f;
    }
}
