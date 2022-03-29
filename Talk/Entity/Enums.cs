using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Talk
{
    internal static class Enums { }
    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 本人
        /// </summary>
        Personal = 0,
        /// <summary>
        /// 他人
        /// </summary>
        Other = 1
    }
    /// <summary>
    /// 消息状态枚举
    /// </summary>
    public enum MsgStatus
    {
        /// <summary>
        /// 未读
        /// </summary>
        UnRead = 0,
        /// <summary>
        /// 已读
        /// </summary>
        HaveRead = 1
    }
    /// <summary>
    /// 通讯类型枚举
    /// </summary>
    public enum ChatType
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        MESG = 0,
        /// <summary>
        /// 游戏消息
        /// </summary>
        GAME = 1
    }
    /// <summary>
    /// 通讯消息状态枚举
    /// </summary>
    public enum ChatStatus
    {
        /// <summary>
        /// 有效
        /// </summary>
        Effective = 0,
        /// <summary>
        /// 无效
        /// </summary>
        Invalid = 1
    }

    /// <summary>
    /// 游戏消息类型
    /// </summary>
    public enum GameType
    {
        /// <summary>
        /// 请求
        /// </summary>
        Request = 0,
        /// <summary>
        /// 同意
        /// </summary>
        Agree = 1,
        /// <summary>
        /// 不同意
        /// </summary>
        DisAgree = 2,
        /// <summary>
        /// 游戏中
        /// </summary>
        GameIn = 3
    }
    /// <summary>
    /// 声音文件
    /// </summary>
    public enum SoundResource
    {
        /// <summary>
        /// Giao歌
        /// </summary>
        Giao,
        /// <summary>
        /// OhNo 声
        /// </summary>
        Ohno,
        /// <summary>
        /// 咚声
        /// </summary>
        Dong,
        /// <summary>
        /// 石头打水声
        /// </summary>
        StoneWater,
        /// <summary>
        /// 雨声
        /// </summary>
        Rain
    }

}
