using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using BlImplementation;
namespace Simulator;

public static class Simulator
{
    private static string? previousState;
    private static string? afterState;
    static bool finishFlag = false;
    public static event EventHandler? StopSimulator;
    public static event EventHandler? ProgressChange;
    public static void DoStop()
    {
        finishFlag = true;
        if (StopSimulator != null)
            StopSimulator("", EventArgs.Empty);
    }
    public static void run()
    {
        finishFlag = false;
        Thread mainThreads = new Thread(new ThreadStart(chooseOrder));
        mainThreads.Start();
        return;
    }
    public static void chooseOrder()
    {
        IBl bl = new Bl();
        int? id;
        while (!finishFlag)
        {
            id = bl.Order.SelectingAnOrderForTreatment();
            if (id == null)
                DoStop();
            else
            {
                BO.Order o = bl.Order.GetOrderDetails((int)id);
                previousState = o.Status.ToString();
                Random rand = new();
                int num = rand.Next(1000, 5000);
                Details details = new(o, num);
                if (ProgressChange != null)
                {
                    ProgressChange(null, details);
                }
                Thread.Sleep(num);
                afterState = (previousState == "Done" ? bl.Order.OrderShippingUpdate((int)id) : bl.Order.OrderDeliveryUpdate((int)id)).Status.ToString();
            }
        }
        return;
    }
    public class Details : EventArgs
    {
        public BO.Order order;
        public int seconds;
        public Details(BO.Order ord, int sec)
        {
            order = ord;
            seconds = sec;
        }
    }
}
