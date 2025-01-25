using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _velocity;
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        
    }
    private void Update()
    {
        
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
}
