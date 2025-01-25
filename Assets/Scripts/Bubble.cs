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
        int changeDir = Random.Range(0,3);

        if (changeDir == 0)
            _newDir = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f), 0f);

        _maxIdleVelocity = Random.Range(0.01f,_maxIdleVelocity);

        Move(_newDir * _maxIdleVelocity);

        Debug.Log("actual: " + _newDir * _maxIdleVelocity);
    }

    private void Move(Vector3 impulse)
    {
        _rigidbody.linearVelocity += impulse;
        Debug.Log("actual: " + _rigidbody.linearVelocity);
    }

    public void OnDestroy()
    {
        Debug.Log("Killing bubble, amount: " + Amount);
        Amount --;
    }
}
