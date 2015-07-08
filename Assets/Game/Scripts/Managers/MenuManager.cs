using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public void LoadScene(string name)
    {
        Application.LoadLevel(name);
    }
}
