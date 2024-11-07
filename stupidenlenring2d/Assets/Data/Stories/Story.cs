using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Story : ScriptableObject
{
    public string sid;
    public string storyName;
    [TextArea]
    public List<string> storyLines;
}
