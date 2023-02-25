using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

public class XMLConfig
{

    public static int getOrderId()
    {
        XElement config = XMLTools.LoadElement(@"Config.xml");
        int id = (int)config.Element("_numOfOrder")!;
        id++;
        config.Element("_numOfOrder")!.SetValue(id);
        XMLTools.SaveElement(config, @"Config.xml");
        return id;
    }
    public static int getOrderItemId()
    {
        XElement config = XMLTools.LoadElement(@"Config.xml");
        int id = (int)config.Element("_numOfOrderItem")!;
        id++;
        config.Element("_numOfOrderItem")!.SetValue(id);
        XMLTools.SaveElement(config, @"Config.xml");
        return id;
    }
    public static int getProductId()
    {
        XElement config = XMLTools.LoadElement(@"Config.xml");
        int id = (int)config.Element("_numOfProduct")!;
        id++;
        config.Element("_numOfProduct")!.SetValue(id);
        XMLTools.SaveElement(config, @"Config.xml");
        return id;
    }
}