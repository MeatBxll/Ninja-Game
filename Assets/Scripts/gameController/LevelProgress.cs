using UnityEngine;
using System.Collections.Generic;

public class LevelProgress : MonoBehaviour
{
  public int saveSlot = 0;
  public HashSet<int> completedLevels = new HashSet<int>();

  private string SaveKey => $"CompletedLevels_{saveSlot}";

  public void MarkLevelComplete(int levelIndex)
  {
    if (completedLevels.Add(levelIndex))
      SaveProgress();
  }

  public bool IsLevelComplete(int levelIndex)
  {
    return completedLevels.Contains(levelIndex);
  }

  public void SaveProgress()
  {
    PlayerPrefs.SetString(SaveKey, string.Join(",", completedLevels));
    PlayerPrefs.Save();
  }

  public void LoadProgress()
  {
    completedLevels.Clear();
    string saved = PlayerPrefs.GetString(SaveKey, "");
    if (!string.IsNullOrEmpty(saved))
    {
      foreach (string s in saved.Split(','))
      {
        if (int.TryParse(s, out int index))
          completedLevels.Add(index);
      }
    }
  }

  public void ResetProgress()
  {
    completedLevels.Clear();
    PlayerPrefs.DeleteKey(SaveKey);
    PlayerPrefs.Save();
  }
}
