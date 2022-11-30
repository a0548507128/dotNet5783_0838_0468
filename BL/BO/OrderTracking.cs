using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public EStatus? Status { get; set; }
    public IEnumerable<StatusAndDate>? listOfStatus { get; set; }

    public class StatusAndDate
    {
        public DateTime Date { get; set; }
        public EStatus Status { get; set; }
    }

    public override string ToString() => $@"
    Order tracking ID={ID}
    Status: {Status}
";
}
