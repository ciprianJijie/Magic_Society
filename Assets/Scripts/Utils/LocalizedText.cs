using UnityEngine;
using System.Collections;
using MS.Core;

[ExecuteInEditMode]
public class LocalizedText : MonoBehaviour
{
    public string ID;

    private UnityEngine.UI.Text m_text;

    void Start()
    {
        m_text = this.gameObject.GetComponent<UnityEngine.UI.Text>();

        UpdateText();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Application.isPlaying == false)
        {
            UpdateText();
        }
#endif
    }

    void UpdateText()
    {
        if (m_text == null)
        {
            m_text = this.gameObject.GetComponent<UnityEngine.UI.Text>();
        }

#if UNITY_EDITOR
        if (Application.isPlaying == false)
        {
            m_text.text = ID;
        }
        else
        {
            m_text.text = Localization.GetString(ID);
        }
#else
        m_text.text = Localization.GetString(ID);
#endif
    }
}
