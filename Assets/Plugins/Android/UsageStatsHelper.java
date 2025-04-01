package com.example.usagestats;

import android.app.usage.UsageStats;
import android.app.usage.UsageStatsManager;
import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.os.Build;
import java.util.List;
import java.util.Calendar;
import java.util.HashMap;
import java.util.Map;
import java.util.HashSet;
import java.util.Set;

public class UsageStatsHelper {

    public static int TimeUsage = 0;

    // Use a HashSet for faster lookup
    private static final Set<String> applabel = new HashSet<>();

    static {
        applabel.add("YouTube");
        applabel.add("Google");
        applabel.add("LinkedIn");
        applabel.add("WhatsApp");
    }

    public static int getTimeUsageValue() {
        return TimeUsage;
    }

    public static int getAppUsageData(Context context) {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP) {
            UsageStatsManager usm = (UsageStatsManager) context.getSystemService(Context.USAGE_STATS_SERVICE);

            Calendar calendar = Calendar.getInstance();
            long endTime = calendar.getTimeInMillis();
            calendar.add(Calendar.DAY_OF_YEAR, -1); // Get data for the last 24 hours
            long startTime = calendar.getTimeInMillis();

            List<UsageStats> stats = usm.queryUsageStats(UsageStatsManager.INTERVAL_DAILY, startTime, endTime);

            if (stats == null || stats.isEmpty()) {
                //return "No usage data available.";
                return 1;
            }

            // Store total time per app
            Map<String, Long> appUsageMap = new HashMap<>();

            for (UsageStats usageStats : stats) {
                String packageName = usageStats.getPackageName();
                String appName = getAppName(context, packageName);
                long totalTime = usageStats.getTotalTimeInForeground();

                if (totalTime > 0) {
                    appUsageMap.put(appName, appUsageMap.getOrDefault(appName, 0L) + totalTime);
                }
            }

            StringBuilder usageData = new StringBuilder();

            for (Map.Entry<String, Long> entry : appUsageMap.entrySet()) {
                String appName = entry.getKey();
                long totalTime = entry.getValue();

                if (applabel.contains(appName)) {
                    TimeUsage += totalTime; // Sum only for selected apps
                    System.out.println("myAppname: " + appName + " " + totalTime);
                }
                
                usageData.append("App: ").append(appName).append(" Time: ").append(totalTime).append(" ms\n");
            }
            System.out.println("myAppname: timeusage " + TimeUsage);
            // return usageData.toString();
            return TimeUsage;
        }
      //  return "UsageStatsManager is not supported on this device.";
        return 1;
    }

    public static String getAppName(Context context, String packageName) {
        try {
            PackageManager pm = context.getPackageManager();
            ApplicationInfo ai = pm.getApplicationInfo(packageName, 0);
            return pm.getApplicationLabel(ai).toString();
        } catch (PackageManager.NameNotFoundException e) {
            return packageName;
        }
    }
}



// import android.app.usage.UsageStats;
// import android.app.usage.UsageStatsManager;
// import android.content.Context;
// import android.os.Build;
// import java.util.List;
// import java.util.Calendar;

// public class UsageStatsHelper {

//     public static int TimeUsage = 10;
//     public static int getTimeUsageValue()
//     {
//         return TimeUsage;
//     }


//     public static String getAppUsageData(Context context) {
//         if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP) {
//             UsageStatsManager usm = (UsageStatsManager) context.getSystemService(Context.USAGE_STATS_SERVICE);

//             Calendar cal = Calendar.getInstance();
//             long endTime = cal.getTimeInMillis();

//             // // Go back 1 month (30 days)
//             // //cal.add(Calendar.DAY_OF_MONTH, -30); //INTERVAL_WEEKLY INTERVAL_DAILY
//             // INTERVAL_MONTHLY

//             // cal.add(Calendar.DAY_OF_MONTH, -30);
//             // long startTime = cal.getTimeInMillis();

//             // Calendar calendar = Calendar.getInstance();
//             // calendar.set(Calendar.HOUR_OF_DAY, 1);
//             // calendar.set(Calendar.MINUTE, 0);
//             // calendar.set(Calendar.SECOND, 0);
//             // long startTime = calendar.getTimeInMillis();

//             Calendar calendar = Calendar.getInstance();
//             // long endTime = calendar.getTimeInMillis(); // Current time

//             calendar.add(Calendar.DAY_OF_YEAR, -1); // 7 days ago
//             long startTime = calendar.getTimeInMillis();

//             List<UsageStats> stats = usm.queryUsageStats(UsageStatsManager.INTERVAL_DAILY, startTime, endTime);

//             StringBuilder usageData = new StringBuilder();
//             int count = stats.size();
//             System.out.println("The list contains " + count + " elements.");

//             if (stats != null) {
//                 for (UsageStats usageStats : stats) {
//                     String appName = usageStats.getPackageName();
//                      String appnamee = getAppName(context, appName);
//                     long totalTime = usageStats.getTotalTimeInForeground();
//                     usageData.append("App: " + appName + " Time: " + totalTime +"ms\n");

//                     System.out.println("it entered here :  ");
//                     if (totalTime != 0 ) {
//                         System.out.println("get the name here " + appName + " Time: " + totalTime + "ms\n");

//                     }
//                 }
//             }

//             return usageData.toString();
//         }
//         return "UsageStatsManager is not supported on this device.";
//     }


//     // private static String getAppNameFromPackage(Context context, String packageName) {
//     //     PackageManager packageManager = context.getPackageManager();
//     //     try {
//     //         ApplicationInfo applicationInfo = packageManager.getApplicationInfo(packageName, 0);
//     //         return packageManager.getApplicationLabel(applicationInfo).toString();
//     //     } catch (PackageManager.NameNotFoundException e) {
//     //         return packageName; // Return package name if app name not found
//     //     }
//     // }

//     public  String getAppName(Context context, String packageName) {
//         try {
//             return context.getPackageManager()
//                     .getApplicationLabel(context.getPackageManager().getApplicationInfo(packageName, 0))
//                     .toString();
//         } catch (PackageManager.NameNotFoundException e) {
//             return packageName; // Return package name if app not found
//         }
//     }
//}