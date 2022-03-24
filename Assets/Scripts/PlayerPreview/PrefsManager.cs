using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    private const string PLAYER_KEY = "Player";
    
    public static void SaveLastSelectedPlayer(string name)
    {
        PlayerPrefs.SetString(PLAYER_KEY, name);
        PlayerPrefs.Save();
    }
    
    public static string GetLastSelectedPlayer()
    {
        return PlayerPrefs.GetString(PLAYER_KEY);
    }
}
