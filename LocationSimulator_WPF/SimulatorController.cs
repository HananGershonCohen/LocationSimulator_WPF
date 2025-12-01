using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LocationSimulator_WPF;

/// <summary>
/// Main control unit, activates the UI
/// </summary>
public class SimulatorController
{
    public ObservableCollection<INavigationSensor> Sensors { get; } = new ObservableCollection<INavigationSensor>();
    private UdpSender _udpSender; 
    public SimulatorController()
    {
        _udpSender = new UdpSender(System.Net.IPAddress.Loopback, 11000);
        InitializeSensors();
        RegistForEvent();
    }

    private void RegistForEvent()
    {
        foreach (var sensor in Sensors)
        {
            sensor.OnReadingAvailable += OnSensorReadingAvailable;
        }
    }
    private void InitializeSensors()
    {
        var gps = new GpsSensor { IntervalMs = 500 };
        Sensors.Add(gps);
    }

    public void OnSensorReadingAvailable(object? sender, ReadingArrivedEventArgs e)
    {
        byte[] datagram = e.ReadingData.ToJsonBytes();
        _udpSender.Send(datagram);

    }

    public void StartAll()
    {
        foreach (var sensor in Sensors)
        {
            if (!sensor.IsRunning)
            {
                sensor.Start();
            }
        }
    }

    public void StopAll()
    {
        foreach (var sensor in Sensors)
        {
            if (sensor.IsRunning)
            {
                sensor.Stop();
            }
        }
    }
}