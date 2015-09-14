using UnityEngine;
using System.Collections;

public class DoubleClickButton : MonoBehaviour
{
    public static readonly float BUTTON_DOUBLE_CLICK_THRESHOLD = 0.3f;

    public UnityEngine.UI.Button Button;

    protected float     m_TimeSinceLastClick;
    protected int       m_Clicks;

    // Events
    public MS.Events.Event OnDoubleClick = MS.Events.DefaultAction;

    protected void OnClick()
    {
        m_Clicks++;
    }

    protected void Update()
    {
        if (m_Clicks > 1)
        {
            // TODO: Trigger event
            m_Clicks = 0;
        }
        else if (m_TimeSinceLastClick - Time.time > 0.3f)
        {
            m_Clicks = 0;
        }
    }

    protected void Start()
    {
        Button.onClick.AddListener(() => OnClick());
    }
}
