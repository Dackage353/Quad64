public static class Helper
{
    public const string Divider = "----------";

    public static string LevelIDToName(int id)
    {
        switch (id)
        {
            case 0x09: return "Course 1";
            case 0x18: return "Course 2";
            case 0x0C: return "Course 3";
            case 0x05: return "Course 4";
            case 0x04: return "Course 5";
            case 0x07: return "Course 6";
            case 0x16: return "Course 7";
            case 0x08: return "Course 8";
            case 0x17: return "Course 9";
            case 0x0A: return "Course 10";
            case 0x0B: return "Course 11";
            case 0x24: return "Course 12";
            case 0x0D: return "Course 13";
            case 0x0E: return "Course 14";
            case 0x0F: return "Course 15";
            case 0x10: return "Overworld 1";
            case 0x06: return "Overworld 2";
            case 0x1A: return "Overworld 3";
            case 0x11: return "Bowser 1 course";
            case 0x13: return "Bowser 2 course";
            case 0x15: return "Bowser 3 course";
            case 0x1E: return "Bowser 1 fight";
            case 0x21: return "Bowser 2 fight";
            case 0x22: return "Bowser 3 fight";
            case 0x1C: return "Cap Level, Metal Cap";
            case 0x1D: return "Cap Level, Wing Cap";
            case 0x12: return "Cap Level, Vanish Cap";
            case 0x1B: return "Secret Level 1";
            case 0x14: return "Secret Level 2";
            case 0x1F: return "Secret Level 3";
            case 0x19: return "Secret Level 4";
        }

        return string.Empty;
    }

    public static string WithQuotesIfNeeded(string line)
    {
        if (line is null) return string.Empty;

        if (line.Contains(","))
        {
            return "\"" + line + "\"";
        }
        else
        {
            return line;
        }
    }
}