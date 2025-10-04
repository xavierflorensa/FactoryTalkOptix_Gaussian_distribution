#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.DataLogger;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.InfluxDBStoreRemote;
using FTOptix.Store;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.InfluxDBStore;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using FTOptix.NetLogic;
#endregion

public class RuntimeNetLogic1 : BaseNetLogic
{
    PeriodicTask myTask1;
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        var gaussian = Project.Current.GetVariable("Model/Gaussian");
        myTask1 = new PeriodicTask(my_Function, 1000, LogicObject);
        myTask1.Start();

    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        myTask1.Dispose();
    }
    private void my_Function()
    {
    var gaussian = Project.Current.GetVariable("Model/Gaussian");
    // Generate a Gaussian-distributed random number (mean=5, stddev=2)
    Random rand = new Random();
    double u1 = 1.0 - rand.NextDouble(); // uniform(0,1] random doubles
    double u2 = 1.0 - rand.NextDouble();
    double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); // standard normal
    double mean = 5.0;
    double stddev = 2.0;
    double randNormal = mean + stddev * randStdNormal;
    // Clamp and convert to int32 between 0 and 10
    int value = (int)Math.Round(Math.Max(0, Math.Min(10, randNormal)));
    gaussian.Value = value;
    }


}
