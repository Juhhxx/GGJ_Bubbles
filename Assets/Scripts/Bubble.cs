using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public static int? Amount { get; private set; }
    private void Awake()
    {
        if (Amount == null)
            Amount = 1;
        Amount ++;
    }

    public void Move(Vector3 impulse)
    {
        _rigidbody.linearVelocity = impulse;
    }

    public void OnDestroy()
    {
        Debug.Log("Killing bubble, amount: " + Amount);
        Amount --;
    }
}
