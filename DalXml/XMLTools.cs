using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

public static class XMLTools
{
    static string dir = @"../xml/";

    static XMLTools()
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }

    #region SaveLoadSerializer
    public static void SaveSerializer<T>(List<T> list, string path)
    {
        try
        {
            FileStream file = new(dir + path, FileMode.Create);
            XmlSerializer x = new(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }
        catch (Exception ex)
        {
            throw new Exception($"fail to create xml file: {path}", ex);
        }
    }

    internal static List<T?> LoadSerializer<T> (string path) where T : struct
    {
        try
        {
            if (File.Exists(dir + path))
            {
                List<T?>? list=new();
                XmlSerializer x = new(typeof(List<T?>));
                FileStream file = new(dir + path, FileMode.OpenOrCreate);
                list = (List<T?>?)x.Deserialize(file)!;
                file.Close();
                return list;
            }
            else
                return new List<T?>();
        }
        catch (Exception ex)
        {
            throw new Exception($"fail to load xml file: {path}", ex);
        }
    }
    #endregion

    #region SaveLoadWithXElement
    public static void SaveElement(XElement rootElem, string filePath)
    {
        try
        {
            rootElem.Save(dir + filePath);
        }
        catch (Exception ex)
        {
            throw new Exception($"fail to create xml file: {filePath}", ex);
        }
    }

    public static XElement LoadElement(string filePath)
    {
        try
        {
            if (File.Exists(dir + filePath))
            {
                return XElement.Load(dir + filePath);
            }
            else
            {
                XElement rootElem = new XElement(dir + filePath);
                rootElem.Save(dir + filePath);
                return rootElem;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(filePath, ex);
        }
    }
    #endregion
}
