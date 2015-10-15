using UnityEngine;

namespace MS.Components
{
    public class Billboard : MonoBehaviour
    {
        protected void Start()
        {
            //Vector3 position;

            //position = new Vector3(Camera.main.transform.position.x, 0.0f, Camera.main.transform.position.z);

            transform.LookAt(transform.position + Managers.BillboardsManager.PositionOffset, Managers.BillboardsManager.TargetOrientation);
            //transform.LookAt(position, Vector3.up);

            //this.transform.forward = Vector3.back;
        }
    }
}
