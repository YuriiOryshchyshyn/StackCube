using UnityEngine;

public class EmptyCubesContainer : MonoBehaviour
{
    [SerializeField] GameObject _cube;

    public void GetCubes(Transform parent)
    {
        if (transform.childCount < 3)
        {
            for (int i = 0; i < 3 - transform.childCount; i++)
            {
                Instantiate(_cube, transform);
            }
        }

        for (int i = 0; i < transform.childCount && i < 3; i++)
        {
            CubeObject cube = transform.GetChild(i).GetComponent<CubeObject>();
            cube.transform.SetParent(parent);
            cube.Rigidbody.isKinematic = false;
            cube.IsInPlayerContainer = false;
        }
    }

    public void DisableCubes()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
