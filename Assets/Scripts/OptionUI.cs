using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    Animation anim;
    [SerializeField] Slider sliderBGM;
    [SerializeField] Slider sliderSFX;


    private void Start()
    {
        anim = GetComponent<Animation>();
    }
    public void OnChangeSFXSlider()
    {

    }

    public void OnChangeBGMSlider()
    {

    }
    public void OnAciteveAnim()
    {
        anim.Play("OptionActive");
    }
    public void OnInAciteveAnim()
    {
        anim.Play("OptionInActive");
    }
}
