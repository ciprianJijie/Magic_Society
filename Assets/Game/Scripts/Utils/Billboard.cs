using UnityEngine;

namespace MS.Components
{
    public class Billboard : MonoBehaviour
    {
        protected void Start()
        {
            transform.LookAt(transform.position + Managers.BillboardsManager.PositionOffset, Managers.BillboardsManager.TargetOrientation);
        }
    }
}
