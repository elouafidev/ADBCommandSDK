/*
* Enums.cs - Developed by Mouad Elouafi for MobileLibrary.dll
*/
namespace ADBCommandSDK
{
    public enum WaitForExitState
    {
        /// <summary>
        /// <see cref="BeginReadLine"/> wait Process for exit method "waitForExit"
        /// </summary>
        WAITFOREXIT,
        /// <summary>
        /// <see cref="BeginReadLine"/> wait Process for exit Bool "StateProcess"
        /// </summary>
        STATEPROCESS
    }

    public enum StateInstallApk
    {
        /// <summary>
        ///('-s' means install on SD card instead of internal storage)
        /// </summary>
        S,
        /// <summary>
        ///('-r' means reinstall the app, keeping its data)
        /// </summary>
        R,
        /// <summary>
        ///('-l' means forward-lock the app)
        /// </summary>
        L,

    
    }
    public enum DeviceState
    {
        /// <summary>
        /// <see cref="Device"/> is online
        /// </summary>
        DEVICE,

        /// <summary>
        /// <see cref="Device"/> is offline
        /// </summary>
        OFFLINE,

        /// <summary>
        /// <see cref="Device"/> is in recovery
        /// </summary>
        RECOVERY,

        /// <summary>
        /// <see cref="Device"/> is in fastboot
        /// </summary>
        FASTBOOT,

        /// <summary>
        /// <see cref="Device"/> is in sideload mode
        /// </summary>
        SIDELOAD,

        /// <summary>
        /// <see cref="Device"/> is not authorized
        /// </summary>
        UNAUTHORIZED,

        /// <summary>
        /// <see cref="Device"/> is in an unknown state
        /// </summary>
        UNKNOWN
    }
}