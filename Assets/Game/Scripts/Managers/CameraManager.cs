using UnityEngine;namespace MS.Managers{    public class CameraManager : MonoBehaviour    {        public Camera       Camera;        public float        Speed;        public float        NearHeight;        public float        FarHeight;        public Vector3      NearRotation;        public Vector3      FarRotation;

        // References
        public UnityEngine.EventSystems.EventSystem EventSystem;        protected float m_CurrentZoom;        protected void Start()
        {
            m_CurrentZoom = 0.5f;
        }        protected void LateUpdate()        {            Vector3 vertical;            Vector3 horizontal;            Vector3 position;            Vector3 rotation;            float   height;            if (EventSystem.IsPointerOverGameObject() == false)
            {
                m_CurrentZoom   =   Mathf.Max(0.0f, m_CurrentZoom + Mathf.Min(1.0f, Input.GetAxis("Mouse ScrollWheel")));
                vertical        =   Vector3.forward * Input.GetAxis("Vertical") * Speed * Time.deltaTime;
                horizontal      =   Vector3.right * Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
                height          =   Mathf.Lerp(NearHeight, FarHeight, m_CurrentZoom);
                rotation        =   Vector3.Lerp(FarRotation, NearRotation, m_CurrentZoom);

                position = new Vector3(Camera.transform.position.x, height, Camera.transform.position.z);
                position += vertical;
                position += horizontal;

                Camera.transform.position       =   position;
                Camera.transform.eulerAngles    =   rotation;
            }        }    }}