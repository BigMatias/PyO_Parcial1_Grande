using UnityEngine;
using UnityEngine.UI;

public class RewardedAdButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private RewardedAdManager ad;

    private void Update()
    {
        button.interactable = ad.adLoaded;
    }
}
