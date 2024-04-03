using System.Collections;
using System.Collections.Generic;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using UnityEngine;

public class Monetization : MonoBehaviour
{
    private void Awake()
    {
        InitializeBridge();

        // DontDestroyOnLoad(gameObject);
    }

    private void InitializeBridge()
    {
        Debug.Log("In InitializeBridge()");

        // Bridge.Initialize(isInitialized =>
        // {
        //     if (isInitialized)
        //     {
        //         Debug.Log("InitializeBridge success: " + isInitialized);
        //         
        //         // Можно установить свой интервал для всех платформ
        //         int seconds = 180;
        //         Bridge.advertisement.SetMinimumDelayBetweenInterstitial(
        //             new SetMinimumDelayBetweenInterstitialVkOptions(seconds),
        //             new SetMinimumDelayBetweenInterstitialYandexOptions(seconds));
        //         
        ShowAd();
        ShowReward();
        //
        //         // if (Bridge.device.type == DeviceType.Mobile)
        //         // {
        //         //     
        //         // }
        //     }
        //     else
        //     {
        //         // Ошибка. Что-то пошло не так.
        //         Debug.Log("InitializeBridge success: " + isInitialized);
        //     }
        // });
    }

     public static bool ShowReward()
     {
       return Reward();
     }

    public static bool ShowAd()
    {
        return Ad();
    }

    private static bool Ad()
    {
        Debug.Log("In Ad()");
        bool isSuccess = false;

        /* -- -- -- Interstitial -- -- -- */
        // Необязательный параметр, игнорировать ли минимальную задержку
        bool ignoreDelay = false; // По умолчанию = false

        // Одинаково для всех платформ
        /*Bridge.advertisement.ShowInterstitial(
            ignoreDelay,
            success =>
            {
                if (success)
                {
                    // Success
                    Debug.Log("Bridge.advertisement.ShowInterstitial success: " + success);
                    isSuccess = true;
                }
                else
                {
                    // Error
                    Debug.Log("Bridge.advertisement.ShowInterstitial success: " + success);
                }
            });*/
        return isSuccess;
    }

    private static bool Reward()
    {
        Debug.Log("In Reward()");

        bool isSuccess = false;
        Bridge.advertisement.ShowRewarded(success =>
        {
            if (success)
            {
                // Success
                Debug.Log("Bridge.advertisement.ShowRewarded success: " + success);
                isSuccess = true;
            }
            else
            {
                // Error
                Debug.Log("Bridge.advertisement.ShowRewarded success: " + success);
            }
        });

        Bridge.advertisement.interstitialStateChanged += state => { Debug.Log($"Interstitial state: {state}"); };
        Bridge.advertisement.rewardedStateChanged += state => { Debug.Log($"Rewarded state: {state}"); };
        return isSuccess;
    }
}