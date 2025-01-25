using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _maxIdleVelocity;
    public static int? Amount { get; private set; }
    private Vector3 _newDir = new Vector3(0f, 0f, 0f);
    private void Awake()
    {
        if (Amount == null)
            Amount = 1;
        Amount ++;
    }

    private void Update()
    {
        int changeDir = Random.Range(0,2);

        if (changeDir == 0)
            _newDir = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), 0f);

        float _velocity = Random.Range(0.01f,_maxIdleVelocity);

        Move(_newDir * _velocity);

        // Debug.Log( "change: " + _newDir * _velocity );
    }

    private void Move(Vector3 impulse)
    {
        _rigidbody.linearVelocity += impulse;
        // Debug.Log("actual: " + _rigidbody.linearVelocity);
    }

    public void OnDestroy()
    {
        Debug.Log("Killing bubble, amount: " + Amount);
        Amount --;
    }
}
