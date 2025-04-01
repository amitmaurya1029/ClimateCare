using UnityEngine;

public class UsageStatsExample : MonoBehaviour
{
    public static int timeUsage;
    void Start()
    {
        // Only run on Android platform
        #if UNITY_ANDROID
        int usageData = GetUsageStatsData();
        timeUsage = usageData; 
        Debug.Log("Usage Data: " + usageData);
        CallThisFun();
        #else
        Debug.Log("This feature is only available on Android.");
        #endif
        
    }

    public int GetUsageStatsData()
    {
        // Use AndroidJavaObject to call the Java method
        using (AndroidJavaClass usageStatsHelper = new AndroidJavaClass("com.example.usagestats.UsageStatsHelper"))
        {
            using (AndroidJavaObject context = GetContext())
            {
                Debug.Log(" OpenUsageAccessSettings gets call : 3");
                return usageStatsHelper.CallStatic<int>("getAppUsageData", context);
            }

        }
    }

    private AndroidJavaObject GetContext()
    {
        // Get the current activity context in Unity
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }


      public void CallThisFun()
    {
        // Use AndroidJavaObject to call the Java method
        using (AndroidJavaClass usageStatsHelper = new AndroidJavaClass("com.example.usagestats.UsageStatsHelper"))
        {
            using (AndroidJavaObject context = GetContext())
            {
                timeUsage = usageStatsHelper.CallStatic<int>("getTimeUsageValue",context);
                Debug.Log("GET THE FINAL APP USAGE VALUE :  " + timeUsage);
            }
        }
    }

  
}
