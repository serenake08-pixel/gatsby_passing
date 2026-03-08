using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ShakeCamera()
    {
        yield return Shake();
    }

    IEnumerator Shake()
    {
        Vector3 start = transform.position;
        float elapsed = 0.0f;
        while (elapsed < 0.3f)
        {
            elapsed += Time.deltaTime;
            transform.position = start + Random.insideUnitSphere * 0.5f;
            yield return null;
        }
        transform.position = start;
    }
}
