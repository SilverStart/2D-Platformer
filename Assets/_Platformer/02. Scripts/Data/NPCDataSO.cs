using UnityEngine;

// [System.Serializable]
// public class NPCData
// {
//     public string name;
//     public string age;
//     public string gender;
//     public string job;
//     [TextArea] public string description;
//     
//     public Sprite icon;
// }

[CreateAssetMenu(fileName = "NPCDataSO", menuName = "Scriptable Objects/NPCDataSO")]
public class NPCDataSO : ScriptableObject
{
    public string name;
    public string age;
    public string gender;
    public string job;
    [TextArea] public string description;

    public Sprite icon;
    
    // public int dialogueIndex;
    // public string[] dialogues;
    
    // public NPCData[] datas;
}