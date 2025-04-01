using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UsageStatsPermission : MonoBehaviour
{
    public Button openSettingsButton;
    public UsageStatsExample usageStatsExample;

    void Start()
    {
        openSettingsButton.onClick.AddListener(OpenUsageAccessSettings);
    }

    public void OpenUsageAccessSettings()
    {
        Debug.Log(" OpenUsageAccessSettings gets call : 1");
        // Application.OpenURL("tel:+1234567890");

        RequestUsageAccess();
        // Application.OpenURL("android.settings.USAGE_ACCESS_SETTINGS");
        

        Debug.Log(" OpenUsageAccessSettings gets call : 1.1" + " " + usageStatsExample.GetUsageStatsData());
        
        
        
        Debug.Log(" OpenUsageAccessSettings gets call : ");
    }

    public void RequestUsageAccess()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", "android.settings.USAGE_ACCESS_SETTINGS");
                activity.Call("startActivity", intent);
            }
        }
    }


}
