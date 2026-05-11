#if UNITY_IOS || UNITY_ANDROID
    using System.Collections;
    using Unity.Notifications.Android;
    using UnityEngine;

    public class NotisManager : MonoBehaviour
    {
        private static string CHANNEL_ID = "notis01";

        private void Start()
        {
            string NotiChannels_Created_Key = "NotiChannels_Created";
            if (!PlayerPrefs.HasKey(NotiChannels_Created_Key))
            {
                var group = new AndroidNotificationChannelGroup()
                {
                    Id = "Main",
                    Name = "Main notifications",
                };
                AndroidNotificationCenter.RegisterNotificationChannelGroup(group);
                var channel = new AndroidNotificationChannel()
                {
                    Id = CHANNEL_ID,
                    Name = "Default Channel",
                    Importance = Importance.Default,
                    Description = "Generic notifications",
                    Group = "Main", 
                };
                AndroidNotificationCenter.RegisterNotificationChannel(channel);

                StartCoroutine(RequestPermission());

                PlayerPrefs.SetString(NotiChannels_Created_Key, "y");
                PlayerPrefs.Save();
            }
            else
            {
                ScheduleNotis();
            }
        }

        private IEnumerator RequestPermission()
        {
            var request = new PermissionRequest();
            while (request.Status == PermissionStatus.RequestPending)
                yield return null;

            ScheduleNotis();
        }

        private void ScheduleNotis()
        {
            AndroidNotificationCenter.CancelAllScheduledNotifications();

            var notification10mins = new AndroidNotification();
            notification10mins.Title = "Hola :p";
            notification10mins.Text = "Grande Matías";
            notification10mins.FireTime = System.DateTime.Now.AddMinutes(10);

            AndroidNotificationCenter.SendNotification(notification10mins, CHANNEL_ID);
        }
    }
#endif
