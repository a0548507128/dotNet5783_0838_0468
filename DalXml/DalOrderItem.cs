using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dal;
using DalApi;
using DO;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

internal class DalOrderItem : IOrderItem
{
    string OrderItemPath = @"OrderItem.xml";

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem IdAdd)
    {
        List<OrderItem?> ListOrderItem = XMLTools.LoadSerializer<OrderItem>(OrderItemPath);

        if (ListOrderItem.FirstOrDefault(s => s?.ID == IdAdd.ID) != null)
            // throw new ItemAlreadyExistsException("order exists, can not add") { ItemAlreadyExists = _newOrderItem.ToString() };
            //throw new DO.BadPersonIdException(IdAdd.ID, "Duplicate student ID");

            //if (GetPerson(IdAdd.ID) == null)
            //    throw new DO.BadPersonIdException(IdAdd.ID, "Missing person ID");
            IdAdd.ID = XMLConfig.getOrderItemId();
        ListOrderItem.Add(IdAdd);

        XMLTools.SaveSerializer(ListOrderItem, OrderItemPath);
        return IdAdd.ID;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int IdDelete)
    {
        List<OrderItem?> ListOrderItem = XMLTools.LoadSerializer<OrderItem>(OrderItemPath);

        OrderItem? ord = ListOrderItem.Find(p => p?.ID == IdDelete);

        if (ord != null)
        {
            ListOrderItem.Remove(ord);
        }
        //else
        //    throw new DO.BadPersonIdException(id, $"bad student id: {IdDelete}");

        XMLTools.SaveSerializer(ListOrderItem, OrderItemPath);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem? Get(int IdGet)
    {
        List<OrderItem?> ListOrderItem = XMLTools.LoadSerializer<OrderItem>(OrderItemPath);

        DO.OrderItem? ord = ListOrderItem.Find(p => p?.ID == IdGet);
        // if (ord != null)
        return ord;
        //else
        //    throw new DO.BadPersonIdException(id, $"bad student id: {id}");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItem?> GetAll(Predicate<DO.OrderItem?>? predict = null)
    {
        List<OrderItem?> ListOrderItem = XMLTools.LoadSerializer<OrderItem>(OrderItemPath);
        if (predict == null)
        {
            return ListOrderItem;
        }
        IEnumerable<OrderItem?> orderItem = ListOrderItem.FindAll(p => predict(p));
        return orderItem;
        //return from student in ListOrderItem
        //       select student;
    }

    //public DO.OrderItem? getOrderItemByPIDOID(int pid, int oid)
    //{
    //    throw new NotImplementedException();
    //}
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Update(OrderItem IdUpdate)
    {
        List<OrderItem?> ListOrderItem = XMLTools.LoadSerializer<OrderItem>(OrderItemPath);

        DO.OrderItem? stu = ListOrderItem.Find(p => p?.ID == IdUpdate.ID);
        if (stu != null)
        {
            ListOrderItem.Remove(stu);
            ListOrderItem.Add(IdUpdate); 
        }
        //else
        //    throw new DO.BadPersonIdException(IdUpdate.ID, $"bad student id: {IdUpdate.ID}");

        XMLTools.SaveSerializer(ListOrderItem, OrderItemPath);
        return IdUpdate.ID;
    }
}
