using WMPLib;

namespace DevChatter.SoundBoard.Infra
{
    public class AudioPlayer
    {
        public void PlayAudioTrack(string audioTrackUrl)
        {
            var mediaPlayer = new WindowsMediaPlayer {URL = audioTrackUrl};

            mediaPlayer.controls.play();
        }
    }
}