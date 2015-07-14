using UnityEngine;
using UnityEngine.UI;

public class FileButtonManager : MonoBehaviour
{
	public string Text
	{
		get
		{
			return m_Text.text;
		}

		set
		{
			m_Text.text = value;
		}
	}

	[HideInInspector]
    public string FilePath;

    [SerializeField]
    protected Text m_Text;
}
