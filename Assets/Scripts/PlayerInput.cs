using System;
using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _velocity;
    [SerializeField] private Animator _feetAnimator;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _hitAnimator;
    [SerializeField] private int _playerNum;
    [SerializeField] private Transform _feetTrans;
    [SerializeField] private Transform _batTrans;
    [SerializeField] private GameControl _gameControl;
    [SerializeField] public bool CanAffect { get; private set; } = true;
    private Shaker _shaker;
    private Rigidbody _rigidbody;
    private Vector2 _moveDelta = new Vector2(0f, 0f);
    private Vector2 _aimDelta = new Vector2(0f, 0f);
    private Vector3 _move = new Vector3(0f, 0f, 0f);
    private Vector3 _aim = new Vector3(0f, 0f, 0f);

    [field:SerializeField] public int Lives { get; private set; }
    public int Points { get; set; }
    public int PlayerNumber => _playerNum;
    [SerializeField] public GameObject _hearts;

    private Vector3 _initialPos;
    private int _initialLives;

    private bool m_CollisionOccurred;

    private GameObject _star;
    private void Start()
    {
        _shaker = FindFirstObjectByType<Shaker>();

        _initialPos = transform.position;
        _initialLives = Lives;

        _star = _hearts.transform.GetChild(0).gameObject;

        for ( int i = 1; i < Lives;  i++ )
        {
            Instantiate(_star, _hearts.transform);
        }

        _rigidbody = GetComponent<Rigidbody>();
        _aim.y = transform.position.y;
    }
    private void Update()
    {
        DoMovemet();
        DoAim();

        if (Input.GetButtonDown("Fire" + _playerNum))
            _animator.SetTrigger("Hold");

        if (Input.GetButtonUp("Fire" + _playerNum))
            _animator.SetTrigger("Swing");
    }

    private void DoMovemet()
    {
        _moveDelta.x = Input.GetAxis("MoveX" + _playerNum);
        _moveDelta.y = Input.GetAxis("MoveY" + _playerNum);

        _move.x = _velocity * _moveDelta.x;
        _move.z = _velocity * _moveDelta.y;

        // Debug.Log("mov: " + _move + "     velocity: " + _velocity + "    _delta: " + _moveDelta);

        Move(_move);
        Look(_feetTrans, _move);

        _feetAnimator.SetBool("IsMoving", _moveDelta != Vector2.zero);
    }
    private void DoAim()
    {
        _aimDelta.x = Input.GetAxis("AimX" + _playerNum);
        _aimDelta.y = Input.GetAxis("AimY" + _playerNum);

        _aim.x = _aimDelta.x;
        _aim.z = _aimDelta.y;

        // Debug.Log("aim: " + _aim + "     _delta: " + _aimDelta);

        Look(_batTrans, _aim);
    }

    private void Die()
    {
        _gameControl.AddPoint(_playerNum == 1 ? 2 : 1);
        Debug.Log("Stupid: " + (_playerNum == 1 ? 2 : 1));
        _animator.SetTrigger("Hurt");
    }

    public void Hurt(Vector3 impulse)
    {
        CanAffect = false;

        
        _hitAnimator.SetTrigger("Hit");
        _shaker.Shake(0.4f, 45f);

        Lives--;

        if (Lives >= 0)
            _hearts.transform.GetChild(Lives).gameObject.SetActive(false);

        Move(impulse * 20f);

        // Maybe some kind of flash?

        if( Lives == 0)
            Die();
    }

    public void BeginAffect()
    {
        StartCoroutine(WaitTillNoCollide());
    }

    private IEnumerator WaitTillNoCollide()
    {
        yield return new WaitUntil(() => ! m_CollisionOccurred);
        CanAffect = true;
    }

    public virtual void OnCollisionStay(Collision c)
    {
        m_CollisionOccurred = true;
    }

    public virtual void FixedUpdate()
    {
        m_CollisionOccurred = false;
    }

    /// <summary>
    /// public pq podemos querer adicionar algum movimento rapido/impacto ao player e fazer com que ele tenha
    //7 controle sobre isso parece me justo
    /// </summary>
    /// <param name="force"> Force with a direction and how far it'll go</param>
    public void Move(Vector3 force)
    {
        // And therefore talvez movimento com fisica seja melhor?
        _rigidbody.linearVelocity = force;
    }

    /// <summary>
    /// Nyeh.
    /// </summary>
    /// <param name="lookTo"></param>
    private void Look(Transform trans, Vector3 lookTo)
    {
        Vector3 actual = transform.position - lookTo;
        trans.LookAt(actual);
    }

    public void ResetPlayer()
    {
        transform.position = _initialPos;
        Lives = _initialLives;

        for ( int i = 0; i < _hearts.transform.childCount;  i++ )
        {
            _hearts.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
