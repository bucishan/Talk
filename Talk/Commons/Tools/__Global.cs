using Gobangs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Talk
{
    /// <summary>
    /// 全局信息通用类
    /// </summary>
    public static class __Global
    {
        /// <summary>
        /// 当前主程序
        /// </summary>
        public static MChat FormTalk = null;
        /// <summary>
        /// 是否正在进行游戏
        /// </summary>
        public static bool IsGameIn = false;

        /// <summary>
        /// 当前进行的五子棋游戏对象
        /// </summary>
        public static Gobang Game_Gobang = null;
    }
}
