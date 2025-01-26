using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    // When bubbles are spawned they may be near spikes or players, so disable the collider that has direct access to the Bubble component
    // This collider should be a trigger, because i should also be the one that sees if it can hurt players or not (the bat will have its own trigger so it should still be hitable by bat)
    // It should still have a child collider that is exactly the same, so that physics still happens and the bubble doesn't escape the map
    [SerializeField] private Collider _collider;
    [SerializeField] private float _maxIdleVelocity;
    [SerializeField] private float _minimumForceToHit;
    public static int? Amount { get; private set; }
    private Vector3 _newDir = new Vector3(0f, 0f, 0f);

    private void Start()
    {
        if (Amount == null)
            Amount = 1;
            
        Amount ++;

        _collider.enabled = false;

        StartCoroutine(CommenceStart());
    }

    private IEnumerator CommenceStart()
    {
        yield return new WaitForSeconds(1.5f);

        _collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInput player = other.GetComponent<PlayerInput>();

        if ( player != null && _rigidbody.linearVelocity.magnitude >= _minimumForceToHit)
        {
            player.Hurt(_rigidbody.linearVelocity);
            Pop();
        }
    }

    private void Update()
    {
        int changeDir = Random.Range(0,2);

        if (changeDir == 0)
            _newDir = new Vector3(Random.Range(-1f,1f), 0f, Random.Range(-1f,1f));

        float _velocity = Random.Range(0.01f,_maxIdleVelocity);

        Move(_newDir * _velocity);

        Debug.Log( "change: " + _newDir * _velocity );
    }
    public void Duplicate(bool Impulse = false)
    {
        GameObject newBubble = Instantiate(gameObject);

        if (Impulse)
            newBubble.GetComponent<Bubble>().Move(_rigidbody.linearVelocity * -10);
    }

    public void Move(Vector3 impulse)
    {
        _rigidbody.linearVelocity += impulse;
        // Debug.Log("actual: " + _rigidbody.linearVelocity);
    }

    public void Pop()
    {
        StartCoroutine(StartPop());
    }

    private IEnumerator StartPop()
    {
        Debug.Log("Killing bubble, amount: " + Amount);

        if (Amount <= 1)
            // Duplicate(true);

        Amount --;

        Renderer renderer = GetComponentInChildren<Renderer>();

        for (int i = 0; i < 7; i++)
        {
            renderer.enabled = ! renderer.enabled;
            yield return new WaitForSeconds(0.2f);
        }

        renderer.enabled = false;
        _collider.enabled = false;

        ParticleSystem particles = GetComponentInChildren<ParticleSystem>();

        if (particles != null)
            particles.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }
}
