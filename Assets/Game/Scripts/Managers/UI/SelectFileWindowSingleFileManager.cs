using UnityEngine;
using UnityEngine.UI;

public class SelectFileWindowSingleFileManager : MonoBehaviour
{
    public string       FilePath;
    public Toggle       Toggle;
    public Text         Text;

    public string LabelText
    {
        get
        {
            return Text.text;
        }

        set
        {
            Text.text = value;
        }
    }

    public ToggleGroup ToggleGroup
    {
        get
        {
            return Toggle.group;
        }

        set
        {
            Toggle.group = value;
        }
    }
}
