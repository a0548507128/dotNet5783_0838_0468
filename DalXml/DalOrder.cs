using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Dal;
internal class DalOrder : IOrder
{
    string OrderPath = @"Order.xml";

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order IdAdd)
    {
        XElement? OrdersRoot = XMLTools.LoadElement(OrderPath);
        IdAdd.ID = XMLConfig.getOrderId();
        XElement? OrderElement = new("Order", 
                              new XElement("ID", IdAdd.ID.ToString()),
                              new XElement("CustomerName", IdAdd.CustomerName),
                              new XElement("CustomerEmail", IdAdd.CustomerEmail),
                              new XElement("CustomerAdress", IdAdd.CustomerAdress),
                              new XElement("OrderDate", DateTime.Now.ToString()),
                              new XElement("ShipDate", IdAdd.ShipDate?.ToShortDateString()),
                              new XElement("DeliveryDate", IdAdd.DeliveryDate?.ToShortDateString()));
        
        OrdersRoot.Add(OrderElement);
        XMLTools.SaveElement(OrdersRoot, OrderPath);
        return IdAdd.ID;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int IdDelete)
    {
        XElement OrderRoot = XMLTools.LoadElement(OrderPath);

        XElement? ord = (from p in OrderRoot.Elements()
                        where int.Parse(p.Element("ID")!.Value) == IdDelete
                        select p).FirstOrDefault();

        if (ord != null)
        {
            ord.Remove(); 

            XMLTools.SaveElement(OrderRoot, OrderPath);
        }
        else
            throw new Exception("this order doesn't exist");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order? Get(int IdGet)
    {
        XElement OrdersRoot = XMLTools.LoadElement(OrderPath);

        Order? o = (from ord in OrdersRoot.Elements()
                    where int.Parse(ord.Element("ID")!.Value) == IdGet
                    select new Order()
                    {
                        ID = int.Parse(ord.Element("ID")!.Value),
                        CustomerName = ord.Element("CustomerName")!.Value,
                        CustomerEmail = ord.Element("CustomerEmail")!.Value,
                        CustomerAdress = ord.Element("CustomerAdress")!.Value,
                        OrderDate = DateTime.Parse(ord.Element("OrderDate")!.Value),
                        ShipDate = ord?.Element("ShipDate")?.Value != "" ? Convert.ToDateTime(ord?.Element("ShipDate")?.Value) : null,
                        DeliveryDate = ord?.Element("DeliveryDate")?.Value != "" ? Convert.ToDateTime(ord?.Element("DeliveryDate")?.Value) : null
                    }
                ).FirstOrDefault();

        if (o?.ID == 0)
            throw new Exception("this order does not exist");
        return o;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order?> GetAll(Predicate<Order?>? predict = null)
    {
        XElement OrdersRoot = XMLTools.LoadElement(OrderPath);
        if (OrdersRoot == null)
            throw new Exception("order not exists,can not get");
        try
        {
            IEnumerable<Order?>? ord = OrdersRoot.Elements().Select(x =>
            {
                Order o = new()
                {
                    ID = int.Parse(x.Element("ID")!.Value.ToString()),
                    CustomerName = x.Element("CustomerName")!.Value.ToString(),
                    CustomerEmail = x.Element("CustomerEmail")!.Value.ToString(),
                    CustomerAdress = x.Element("CustomerAdress")!.Value.ToString()
                   
                };
                try
                {
                    o.ShipDate = DateTime.Parse(x.Element("ShipDate")!.Value.ToString());
                    o.DeliveryDate = DateTime.Parse(x.Element("DeliveryDate")!.Value.ToString());
                    o.OrderDate = DateTime.Parse(x.Element("OrderDate")!.Value.ToString());
                }
                catch
                {
                    o.ShipDate = null;
                    o.DeliveryDate = null;
                    o.OrderDate = null;
                }
                return (Order?)o;
            }).Where(x => predict == null || predict(x));
            return ord;
        }
        catch
        {
            throw new Exception("order not exists,can not get");
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Update(Order IdUpdate)
    {
        XElement OrdersRoot = XMLTools.LoadElement(OrderPath);

        XElement? ord = (from p in OrdersRoot.Elements()
                        where int.Parse(p.Element("ID")!.Value) == IdUpdate.ID
                        select p).FirstOrDefault();

        if (ord != null)
        {
            ord.Element("ID")!.Value = IdUpdate.ID.ToString();
            ord.Element("CustomerName")!.Value = IdUpdate.CustomerName!;
            ord.Element("CustomerEmail")!.Value = IdUpdate.CustomerEmail!;
            ord.Element("CustomerAdress")!.Value = IdUpdate.CustomerAdress!;
            ord.Element("ShipDate")!.Value = IdUpdate.ShipDate.ToString()!;
            ord.Element("DeliveryDate")!.Value = IdUpdate.DeliveryDate.ToString()!;
            ord.Element("OrderDate")!.Value = IdUpdate.OrderDate.ToString()!;
            XMLTools.SaveElement(OrdersRoot, OrderPath);
        }
        else
            throw new Exception("this order doesn't exist");
        return IdUpdate.ID;
    }

}


