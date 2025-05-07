public static class HeistGameState
{
    public static bool hasGold = false;
    public static bool hackerSuccess = false;

    public static bool mainRobberReady = false;
    public static bool lockPickerReady = false;
    public static bool insiderReady = false;

    public static void Reset()
    {
        hasGold = false;
        hackerSuccess = false;
        mainRobberReady = false;
        lockPickerReady = false;
        insiderReady = false;
    }
}
