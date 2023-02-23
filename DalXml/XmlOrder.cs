using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;

namespace Dal;

internal class XmlOrder : IOrder
{
    string OrderPath = @"Order.xml";
    public int Add(Order IdAdd)
    {
        XElement OrdersRoot = XMLTools.LoadElement(OrderPath);

        XElement? per1 = (from p in OrdersRoot.Elements()
                         where int.Parse(p.Element("ID")!.Value) == IdAdd.ID
                         select p).FirstOrDefault();

        //if (per1 != null)
        //    throw new DO.BadPersonIdException(IdAdd.ID, "Duplicate person ID");

        XElement OrderElement = new XElement("ID", new XElement("ID", IdAdd.ID.ToString()),
                              new XElement("CustomerName", IdAdd.CustomerName),
                              new XElement("CustomerEmail", IdAdd.CustomerEmail),
                              new XElement("CustomerAdress", IdAdd.CustomerAdress),
                              new XElement("OrderDate", DateTime.Now.ToString()),
                              new XElement("ShipDate", IdAdd.ShipDate == null ? null : IdAdd.ShipDate.ToString()),
                              new XElement("DeliveryDate", IdAdd.DeliveryDate == null ? null : IdAdd.DeliveryDate.ToString()));
        OrdersRoot.Add(OrderElement);
        XMLTools.SaveElement(OrdersRoot, OrderPath);
        return IdAdd.ID;
    }
    public void Delete(int IdDelete)
    {
        XElement OrderRoot = XMLTools.LoadElement(OrderPath);

        XElement? ord = (from p in OrderRoot.Elements()
                        where int.Parse(p.Element("ID")!.Value) == IdDelete
                        select p).FirstOrDefault();

        if (ord != null)
        {
            ord.Remove(); //<==>   Remove per from personsRootElem

            XMLTools.SaveElement(OrderRoot, OrderPath);
        }
        //else
        //    throw new DO.BadPersonIdException(id, $"bad person id: {IdDelete}");
    }

    public Order? Get(int IdGet){
    XElement OrdersRoot = XMLTools.LoadElement(OrderPath);

        Order? o = (from ord in OrdersRoot.Elements()
                where int.Parse(ord.Element("ID")!.Value) == IdGet
                   select new Order()
                   {
                       ID = Int32.Parse(ord.Element("ID")!.Value),
                       CustomerName = ord.Element("CustomerName")!.Value,
                       CustomerEmail = ord.Element("CustomerEmail")!.Value,
                       CustomerAdress = ord.Element("CustomerAdress")!.Value,
                       OrderDate = DateTime.Parse(ord.Element("OrderDate")!.Value),
                       ShipDate = DateTime.Parse(ord.Element("ShipDate")!.Value),
                       DeliveryDate = DateTime.Parse(ord.Element("DeliveryDate")!.Value)
                   }
                ).FirstOrDefault();

            if (o == null)
                throw new Exception("this order does not exist");
       // throw new DO.BadPersonIdException(id, $"bad person id: {id}");

        return o;
}

    public IEnumerable<Order?> GetAll(Predicate<Order?>? predict = null)
    {
        XElement OrdersRoot = XMLTools.LoadElement(OrderPath);
        //if (OrdersRoot == null)
        //    throw new RequestedItemNotFoundException("orders not exists,can not get") { RequestedItemNotFound = predict?.ToString() };
        try
        {
            IEnumerable<Order?>? ord = OrdersRoot.Elements().Select(x =>
            {
                Order o = new()
                {
                    ID = Int32.Parse(x.Element("ID")!.Value.ToString()),
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
                return (DO.Order?)o;
            }).Where(x => predict == null || predict(x));
            return ord;
        }
        catch
        {
            throw new Exception("order not exists,can not get");
           // throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = predict?.ToString() };
        }
    }

    public int Update(Order IdUpdate)
    {
        XElement personsRootElem = XMLTools.LoadElement(OrderPath);

        XElement? per = (from p in personsRootElem.Elements()
                        where int.Parse(p.Element("ID")!.Value) == IdUpdate.ID
                        select p).FirstOrDefault();

        if (per != null)
        {
            per.Element("ID")!.Value = IdUpdate.ID.ToString();
            per.Element("CustomerName")!.Value = IdUpdate.CustomerName!;
            per.Element("CustomerEmail")!.Value = IdUpdate.CustomerEmail!;
            per.Element("CustomerAdress")!.Value = IdUpdate.CustomerAdress!;
            per.Element("ShipDate")!.Value = "";
            per.Element("DeliveryDate")!.Value ="";
            per.Element("OrderDate")!.Value = DateTime.Now.ToString();

            XMLTools.SaveElement(personsRootElem, OrderPath);
        }
        //else
        //    throw new DO.BadPersonIdException(person.ID, $"bad person id: {person.ID}");
        return IdUpdate.ID;
    }

}


