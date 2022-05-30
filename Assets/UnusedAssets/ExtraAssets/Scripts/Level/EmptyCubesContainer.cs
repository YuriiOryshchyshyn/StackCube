using UnityEngine;

public class EmptyCubesContainer : MonoBehaviour
{
    public void GetCubes(Transform parent)
    {
        for (int i = 0; i < transform.childCount && i < 3; i++)
        {
            CubeObject cube = transform.GetChild(i).GetComponent<CubeObject>();
            cube.transform.SetParent(parent);
            cube.Rigidbody.isKinematic = false;
            cube.IsInContainer = false;
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
