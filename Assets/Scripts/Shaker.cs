using UnityEngine;

public class Shaker : MonoBehaviour
{
    private float duration = 0;
    private float magnitude = 0;
    private bool isShaking;
    private Vector3 _original;
    void Update()
    {
        if (isShaking)
        {
            transform.position = _original;
            
            if ( duration <=  0 )
            {
                duration = 0;
                magnitude = 0;
                isShaking = false;
                return;
            }

            float x = Random.Range(-1f, 1f) * magnitude * duration;
            float y = Random.Range(-1f, 1f) * magnitude * duration;

            transform.position += new Vector3(x, y, 0f);

            duration -= Time.deltaTime;
        }
    }

    public void Shake(float duration, float magnitude)
    {
        if ( !isShaking )
            _original = transform.position;
        
        this.duration = duration;
        this.magnitude = magnitude;
        isShaking = true;
    }
}