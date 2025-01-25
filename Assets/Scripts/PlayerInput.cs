using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _velocity;
    [SerializeField] private Animator _feetAnimator;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _playerNum;
    [SerializeField] private Transform _feetTrans;
    [SerializeField] private Transform _batTrans;
    private Rigidbody _rigidbody;
    private Vector2 _moveDelta = new Vector2(0f, 0f);
    private Vector2 _aimDelta = new Vector2(0f, 0f);
    private Vector3 _move = new Vector3(0f, 0f, 0f);
    private Vector3 _aim = new Vector3(0f, 0f, 0f);

    private void Start()
    {
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
        _move = _rigidbody.linearVelocity;

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

    public void Hurt(Vector3 impulse)
    {
        Move(impulse * 3f);

        // Maybe some kind of flash?
        _animator.SetTrigger("Hurt");
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
}
