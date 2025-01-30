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
    [SerializeField] private ParticleSystem _particles;
    public static int? Amount { get; private set; }
    private Vector3 _newDir = new Vector3(0f, 0f, 0f);
    private Vector3 _initPos;

    private void Start()
    {
        _initPos = transform.position;

        if (Amount == null)
            Amount = 1;

        Amount ++;

        // _collider.enabled = false;

        StartCoroutine(CommenceStart());
    }

    private IEnumerator CommenceStart()
    {
        yield return new WaitForSeconds(1.5f);

        // _collider.enabled = true;
    }

    private void FixedUpdate()
    {
        _moved = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInput player = other.gameObject.GetComponent<PlayerInput>();

        if ( player != null && _rigidbody.linearVelocity.magnitude >= _minimumForceToHit && player.CanAffect)
        {
            player.Hurt(_rigidbody.linearVelocity);
            Pop();
        }

        _moved = true;
    }

    private Coroutine _balloon;

    private void Update()
    {
        int changeDir = Random.Range(0,2);

        if (changeDir == 0)
            _newDir = new Vector3(Random.Range(-1f,1f), 0f, Random.Range(-1f,1f));

        float _velocity = Random.Range(0.01f,_maxIdleVelocity);

        Move(_newDir * _velocity);

        if  ((_rigidbody.linearVelocity.magnitude >= 299f) || _bood)
        {
            _bood = true;
            Debug.Log("boop");

            if (_balloon == null) _balloon = StartCoroutine(Balloon());
        }

        // Debug.Log( "change: " + _newDir * _velocity );
    }

    private IEnumerator Balloon()
    {
        float min = 300f;
        float max = 450f;
        float initMax = _maxIdleVelocity;
        _maxIdleVelocity *= Mathf.Lerp(15f, 45f, Mathf.InverseLerp(min, max, Mathf.Clamp(_rigidbody.linearVelocity.magnitude, min, max)));

        Debug.Log("boop1");
        yield return new WaitForSeconds(Mathf.Lerp(1.2f, 0f, Mathf.InverseLerp(min, max, Mathf.Clamp(_rigidbody.linearVelocity.magnitude, min, max))));

        Debug.Log("boop2");

        while(_rigidbody.linearVelocity.magnitude >= 30f)
        {
            _rigidbody.linearVelocity = Vector3.Lerp(_rigidbody.linearVelocity, Vector3.zero, Mathf.Lerp(0.02f, 1f, Mathf.InverseLerp(min, max, Mathf.Clamp(_rigidbody.linearVelocity.magnitude, min, max))));
            
            if (_moved)
            {
                _balloon = null;
                _bood = false;
                _maxIdleVelocity = initMax;
                yield break;
            }

            yield return null;
        }

        Debug.Log("boop3");

        _bood = false;
        _balloon = null;
        _maxIdleVelocity = initMax;
    }


    public void Duplicate(bool Impulse = false)
    {
        GameObject newBubble = Instantiate(gameObject);

        if (Impulse)
            newBubble.GetComponent<Bubble>().Move(_rigidbody.linearVelocity * -10);
    }

    private bool _bood = false;
    private bool _moved = false;
    public void Move(Vector3 impulse)
    {
        // Debug.Log("bbl vel mag: " + _rigidbody.linearVelocity.magnitude );
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

        _particles.gameObject.SetActive(true);
        _particles.Play();

        for (int i = 0; i < 7; i++)
        {
            renderer.enabled = ! renderer.enabled;
            yield return new WaitForSeconds(0.2f);
        }

        renderer.enabled = true;
        // _collider.enabled = true;

        yield return new WaitForSeconds(5f);

        _particles.Stop();
        _particles.gameObject.SetActive(false);

        // Destroy(gameObject);
    }

    public void ResetBubble()
    {
        _rigidbody.linearVelocity = new Vector3(0f, 0f, 0f);
        transform.position = _initPos;
    }
}
