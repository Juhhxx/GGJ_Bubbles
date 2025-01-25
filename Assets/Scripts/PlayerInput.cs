using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _velocity;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _playerNum;
    private Vector2 _moveDelta = new Vector2(0f, 0f);
    private Vector2 _aimDelta = new Vector2(0f, 0f);
    private Vector3 _move = new Vector3(0f, 0f, 0f);
    private Vector3 _aim = new Vector3(0f, 0f, 0f);

    private void Start()
    {
        _aim.z = transform.position.z;
    }
    private void Update()
    {
        _move = _rigidbody.linearVelocity;

        _moveDelta.x = Input.GetAxis("MoveX" + _playerNum);
        _moveDelta.y = Input.GetAxis("MoveY" + _playerNum);

        _move.x = _velocity * _moveDelta.x;
        _move.y = _velocity * _moveDelta.y;

        Debug.Log("mov: " + _move + "     velocity: " + _velocity + "    _delta: " + _moveDelta);

        Move(_move);

        _animator.SetBool("IsMoving", _moveDelta != Vector2.zero);


        _aimDelta.x = Input.GetAxis("AimX" + _playerNum);
        _aimDelta.y = Input.GetAxis("AimY" + _playerNum);

        _aim.x = _aimDelta.x;
        _aim.y = _aimDelta.y;

        Debug.Log("aim: " + _aim + "     _delta: " + _aimDelta);

        Look(_aim);


        if (Input.GetButtonDown("Fire" + _playerNum))
            _animator.SetTrigger("Hold");

        if (Input.GetButtonUp("Fire" + _playerNum))
            _animator.SetTrigger("Swing");
    }

    /// <summary>
    /// public pq podemos querer adicionar algum movimento rapido/impacto ao player e fazer com que ele tenha
    //7 controle sobre isso parece me justo
    /// </summary>
    /// <param name="force"> Force with a direction and how far it'll go</param>
    public void Move(Vector3 force)
    {
        // And therefore talvez movimento com fisica seja melhor?
        _rigidbody.linearVelocity += force;
    }

    /// <summary>
    /// Nyeh.
    /// </summary>
    /// <param name="lookTo"></param>
    private void Look(Vector3 lookTo)
    {
        Vector3 actual = transform.position + lookTo;
        transform.LookAt(actual);
    }
}
