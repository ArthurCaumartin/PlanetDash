using UnityEngine;

[ExecuteInEditMode]
public class CameraControler : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 _offSet;

    private void Update()
    {
        if(!target) return;
        Vector3 newPos =  target.position + target.up * _offSet.y + target.right * _offSet.x;
        newPos.z = _offSet.z;
        transform.position = newPos;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target.up);
    }
}
