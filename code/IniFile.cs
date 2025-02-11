using System;
using System.Runtime.InteropServices;
using System.Text;

public class IniFile
{
    private string filePath;

    // 导入 Windows API 函数
    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    // 构造函数，传入 INI 文件路径
    public IniFile(string path)
    {
        filePath = path;
    }

    // 写入键值对
    public void Write(string section, string key, string value)
    {
        WritePrivateProfileString(section, key, value, filePath);
    }

    // 读取键值对
    public string Read(string section, string key, string defaultValue = null)
    {
        StringBuilder retVal = new StringBuilder(255);
        GetPrivateProfileString(section, key, defaultValue, retVal, 255, filePath);
        return retVal.ToString();
    }

    // 删除键
    public void DeleteKey(string section, string key)
    {
        Write(section, key, null);
    }

    // 删除节
    public void DeleteSection(string section)
    {
        Write(section, null, null);
    }

    // 检查键是否存在
    public bool KeyExists(string section, string key)
    {
        return Read(section, key).Length > 0;
    }
}