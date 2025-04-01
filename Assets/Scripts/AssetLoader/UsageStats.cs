using UnityEngine;

public class UsageStats : MonoBehaviour
{
    public static void GetAppUsageStats()
    {
        #if UNITY_ANDROID 
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");

            AndroidJavaClass usageStatsManagerClass = new AndroidJavaClass("android.app.usage.UsageStatsManager");
            AndroidJavaObject usageStatsManager = context.Call<AndroidJavaObject>("getSystemService", "usagestats");

            // Query for app usage stats (this requires special permissions)
            long currentTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long oneDayAgo = currentTime - 24 * 60 * 60 * 1000;

            AndroidJavaObject statsList = usageStatsManager.Call<AndroidJavaObject>("queryUsageStats", 4, oneDayAgo, currentTime);
       
            // Process the statsList here as needed.
        }
        #else
        Debug.Log("App usage stats are unavailable on this platform.");
        #endif
    }

    void Start()
    {
        GetAppUsageStats();
    }
}
