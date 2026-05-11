using UnityEngine;
using UnityEngine.UI;

public class InterstitialButton : MonoBehaviour
{
    public Button theButton;
    public InterstitialManager ad;

    private void Update()
    {
        theButton.interactable = ad.adLoaded;
    }
}
