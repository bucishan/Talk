using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Media;

namespace Talk
{
    /// <summary>
    /// 音效播放工具类
    /// </summary>
    public static class SoundTool
    {
        /// <summary>
        /// 调用API播放音效
        /// </summary>
        /// <param name="pszSound"></param>
        /// <param name="hmod"></param>
        /// <param name="fdwSound"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern bool PlaySoundAPI(string pszSound, int hmod, int fdwSound);
        public const int SND_FILENAME = 0x00020000;
        public const int SND_ASYNC = 0x0001;

        /// <summary>
        /// 播放音效 调用API实现
        /// </summary>
        public static void PlaySoundUseApi()
        {
            string soundPath = Path.Combine(Application.StartupPath, "sound/ohno.wav");
            bool dd = File.Exists(soundPath);
            PlaySoundAPI(soundPath, 0, SND_ASYNC | SND_FILENAME);
        }


        /// <summary>
        /// 音频播放对象
        /// </summary>
        private static SoundPlayer _SoundPlayer = null;

        /// <summary>
        /// 初始化工具类属性
        /// </summary>
        public static void InitSoundTool()
        {
            string soundPath = Path.Combine(Application.StartupPath, "sound/stonewater.wav");
            _SoundPlayer = new SoundPlayer();
            _SoundPlayer.SoundLocation = soundPath;
            _SoundPlayer.Load();
        }

        /// <summary>
        /// 设置播放的音频文件
        /// </summary>
        public static SoundResource PlaySoundResource
        {
            set
            {
                string soundPath = GetSoundPath(value);
                if (string.IsNullOrEmpty(soundPath)) return;
                _SoundPlayer.SoundLocation = soundPath;
                _SoundPlayer.Load();
            }
        }

        private static string GetSoundPath(SoundResource _SoundResource)
        {
            string SoundName = "";
            switch (_SoundResource)
            {
                case SoundResource.Giao:
                    SoundName = "giao.wav";
                    break;
                case SoundResource.Ohno:
                    SoundName = "ohno.wav";
                    break;
                case SoundResource.Dong:
                    SoundName = "dong.wav";
                    break;
                case SoundResource.StoneWater:
                    SoundName = "stonewater.wav";
                    break;
                case SoundResource.Rain:
                    SoundName = "rain.wav";
                    break;
                default:
                    SoundName = null;
                    break;
            }
            return Path.Combine(Application.StartupPath, "sound", SoundName);
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        public static void PlaySound()
        {
            if (_SoundPlayer == null) return;
            _SoundPlayer.Play();
        }


    }
}
