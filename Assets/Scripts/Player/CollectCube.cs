using UnityEngine;

public class CollectCube : MonoBehaviour
{
    [SerializeField] private CubesContainer _cubesContainer;
    [SerializeField] private GameObject _collectCubeText;
    [SerializeField] private Transform _textContainer;

    private void OnEnable()
    {
        _cubesContainer.CubeStacked += InstantiateCollectCubeText;
    }

    private void OnDisable()
    {
        _cubesContainer.CubeStacked -= InstantiateCollectCubeText;
    }

    private void InstantiateCollectCubeText(Vector3 position)
    {
        CollectCubeText collectCubeText = Instantiate(_collectCubeText).GetComponent<CollectCubeText>();
        collectCubeText.transform.position = position + new Vector3(0, 0.5f, 0);
        collectCubeText.StartAnimation();
        collectCubeText.transform.SetParent(_textContainer);
        collectCubeText.DestroyText();
    }
}
