﻿using com.tinylabproductions.TLPLib.Android;
using com.tinylabproductions.TLPLib.Extensions;
using UnityEngine;

namespace com.tinylabproductions.TLPLib.Utilities {
  public static class Platform {
    public const string
      ANDROID = "android",
      IOS = "ios",
#if !UNITY_5_3_OR_NEWER
      WP8 = "wp8",
#endif
#if !UNITY_5_4_OR_NEWER
      BLACKBERRY = "blackberry",
      WEB = "web",
#endif
      METRO = "metro",
      PC = "pc",
      OTHER = "other",

      SUBNAME_EDITOR = "editor",
      SUBNAME_AMAZON_REGULAR = "amazon",
      SUBNAME_AMAZON_UNDERGROUND = "amazon-underground",
      SUBNAME_OUYA = "ouya",
      SUBNAME_GAMESTICK = "gamestick",
      SUBNAME_OPERA = "opera",
      SUBNAME_TV = "tv",
      SUBNAME_WINDOWS = "windows",
      SUBNAME_OSX = "osx",
      SUBNAME_OSX_DASHBOARD = "osx-dashboard",
      SUBNAME_LINUX = "linux",
      SUBNAME_WILDTANGENT = "wildtangent",
      SUBNAME_NONE = "";

    public static string fullName => name.joinOpt(subname, separator: "-");

    public static string name { get {
      switch (Application.platform) {
        case RuntimePlatform.Android: return ANDROID;
        case RuntimePlatform.IPhonePlayer: return IOS;
#if !UNITY_5_3_OR_NEWER
        case RuntimePlatform.WP8Player: return WP8;
#endif
#if UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
        case RuntimePlatform.MetroPlayerX86:
        case RuntimePlatform.MetroPlayerX64:
        case RuntimePlatform.MetroPlayerARM:
          return METRO;
#else
        case RuntimePlatform.WSAPlayerX86:
        case RuntimePlatform.WSAPlayerX64:
        case RuntimePlatform.WSAPlayerARM:
          return METRO;
#endif
#if !UNITY_5_4_OR_NEWER
        case RuntimePlatform.BlackBerryPlayer: return BLACKBERRY;
        case RuntimePlatform.WindowsWebPlayer:
        case RuntimePlatform.OSXWebPlayer:
          return WEB;
#endif
        case RuntimePlatform.WindowsPlayer:
        case RuntimePlatform.OSXPlayer:
        case RuntimePlatform.OSXDashboardPlayer:
        case RuntimePlatform.LinuxPlayer:
          return PC;
        default: 
          return OTHER;
      }
    } }

    public static bool isAmazon =>
#if SUBNAME_AMAZON_REGULAR || UNITY_AMAZON_UNDERGROUND
        true;
#else
        false;
#endif


    public static string subname { get {
      if (Application.isEditor) return SUBNAME_EDITOR;

#if UNITY_ANDROID
      if (name == ANDROID) {
#if UNITY_AMAZON_REGULAR
        return SUBNAME_AMAZON_REGULAR;
#elif UNITY_AMAZON_UNDERGROUND
        return SUBNAME_AMAZON_UNDERGROUND;
#elif UNITY_OUYA
        return SUBNAME_OUYA;
#elif UNITY_GAMESTICK
        return SUBNAME_GAMESTICK;
#elif UNITY_OPERA
        return SUBNAME_OPERA;
#elif UNITY_WILDTANGENT
        return SUBNAME_WILDTANGENT;
#else
        if (!Droid.hasTouchscreen) return SUBNAME_TV;
#endif
      }
#endif
      if (name == PC) {
        switch (Application.platform) {
          case RuntimePlatform.WindowsPlayer:
            return SUBNAME_WINDOWS;
          case RuntimePlatform.OSXPlayer:
            return SUBNAME_OSX;
          case RuntimePlatform.OSXDashboardPlayer:
            return SUBNAME_OSX_DASHBOARD;
          case RuntimePlatform.LinuxPlayer:
            return SUBNAME_LINUX;
        }
      }
#if !UNITY_5_4_OR_NEWER
      if (name == WEB) {
        switch (Application.platform) {
          case RuntimePlatform.WindowsWebPlayer:
            return SUBNAME_WINDOWS;
          case RuntimePlatform.OSXWebPlayer:
            return SUBNAME_OSX;
        }
      }
#endif
      return SUBNAME_NONE;
    } }
  }
}