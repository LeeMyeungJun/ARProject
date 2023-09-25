using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void Play(string soundName)
    {
        SoundPlayer.PlaySoundFx(soundName);
    }

    public void PopupOpenSFX()
    {
        SoundPlayer.PlaySoundFx("magic_pop_open_01");
    }
    public void PopupCloseSFX()
    {
        SoundPlayer.PlaySoundFx("ui_button_socket_movement_03");

    }
    public void BtnSFX()
    {
        SoundPlayer.PlaySoundFx("ui_menu_button_beep_17");
    }
}
