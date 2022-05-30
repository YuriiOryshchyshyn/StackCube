using System.Collections;
using UnityEngine;

public class CollectCubeText : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _riseY;
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private Vector3 _targetXPosition;

    private Vector3 _targetPosition;

    public void StartAnimation()
    {
        _targetPosition = transform.position + new Vector3(transform.position.x, transform.position.y + _riseY, transform.position.z);
        StartCoroutine(ScaletAnimation());
    }

    public void DestroyText()
    {
        Invoke("DisableText", 1.5f);
    }

    private void DisableText()
    {
        Destroy(gameObject);
    }

    private IEnumerator ScaletAnimation()
    {
        Vector3 startScale = transform.localScale;
        float lerpValue = 0;

        while(transform.localScale != _targetScale)
        {
            transform.localScale = Vector3.Lerp(startScale, _targetScale, lerpValue);
            lerpValue += _speed * Time.deltaTime;
            yield return null;
        }
    }

    void Update()
    {
        if (transform.position.y < _targetPosition.y)
            transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.x > _targetXPosition.x)
            transform.Translate(-transform.right * _speed * Time.deltaTime);
    }
}
