using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Runtime.InteropServices;

namespace Server_Info.Domain
{
    public class ServerInfo : Info    
    {        
        public ServerInfo(HttpContext context, HttpRequest request) : base(context, request)
        {
            // Basic Environment Information
            Parameters.Add("Machine Name", Environment.MachineName);
            Parameters.Add("User Domain Name", Environment.UserDomainName);
            Parameters.Add("User Name", Environment.UserName);
            Parameters.Add("User Interactive", Environment.UserInteractive.ToString());
            Parameters.Add("User OS Version", Environment.OSVersion.ToString());
            Parameters.Add("Tick Count", (Environment.TickCount / (1000 * 60 * 60)) + " Hours");

            // Extended System Information
            Parameters.Add("OS Platform", Environment.OSVersion.Platform.ToString());
            Parameters.Add("OS Version String", Environment.OSVersion.VersionString);
            Parameters.Add("Processor Count", Environment.ProcessorCount.ToString());
            Parameters.Add("System Directory", Environment.SystemDirectory);
            Parameters.Add("Current Directory", Environment.CurrentDirectory);
            Parameters.Add("Command Line", Environment.CommandLine);
            Parameters.Add("Is 64-bit OS", Environment.Is64BitOperatingSystem.ToString());
            Parameters.Add("Is 64-bit Process", Environment.Is64BitProcess.ToString());
            
            // .NET Runtime Information
            Parameters.Add(".NET Version", Environment.Version.ToString());
            Parameters.Add("Runtime Directory", RuntimeEnvironment.GetRuntimeDirectory());
            Parameters.Add("Framework Description", RuntimeInformation.FrameworkDescription);
            Parameters.Add("Runtime Identifier", RuntimeInformation.RuntimeIdentifier);
            Parameters.Add("OS Architecture", RuntimeInformation.OSArchitecture.ToString());
            Parameters.Add("Process Architecture", RuntimeInformation.ProcessArchitecture.ToString());
            Parameters.Add("OS Description", RuntimeInformation.OSDescription);

            // Memory Information
            Parameters.Add("Working Set (MB)", (Environment.WorkingSet / (1024 * 1024)).ToString());
            Parameters.Add("GC Total Memory (MB)", (GC.GetTotalMemory(false) / (1024 * 1024)).ToString());
            Parameters.Add("GC Memory After Collection (MB)", (GC.GetTotalMemory(true) / (1024 * 1024)).ToString());

            // Process Information
            var currentProcess = Process.GetCurrentProcess();
            Parameters.Add("Process ID", currentProcess.Id.ToString());
            Parameters.Add("Process Name", currentProcess.ProcessName);
            Parameters.Add("Process Start Time", currentProcess.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            Parameters.Add("Process Threads Count", currentProcess.Threads.Count.ToString());
            Parameters.Add("Process Peak Working Set (MB)", (currentProcess.PeakWorkingSet64 / (1024 * 1024)).ToString());
            Parameters.Add("Process Total Processor Time", currentProcess.TotalProcessorTime.ToString());

            // Special Folders
            Parameters.Add("System Folder", Environment.GetFolderPath(Environment.SpecialFolder.System));
            Parameters.Add("Windows Folder", Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            Parameters.Add("Program Files", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            Parameters.Add("Temp Folder", Path.GetTempPath());

            // Network Information
            try
            {
                var hostName = Dns.GetHostName();
                Parameters.Add("Host Name", hostName);
                
                var hostEntry = Dns.GetHostEntry(hostName);
                Parameters.Add("Host Addresses", string.Join(", ", hostEntry.AddressList.Select(ip => ip.ToString())));

                // Network Interfaces
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(ni => ni.OperationalStatus == OperationalStatus.Up)
                    .ToList();
                
                Parameters.Add("Active Network Interfaces Count", networkInterfaces.Count.ToString());
                
                for (int i = 0; i < networkInterfaces.Count && i < 5; i++) // Limit to 5 interfaces
                {
                    var ni = networkInterfaces[i];
                    Parameters.Add($"<b>Network Interface {i + 1}:</b> Name", ni.Name);
                    Parameters.Add($"<b>Network Interface {i + 1}:</b> Type", ni.NetworkInterfaceType.ToString());
                    Parameters.Add($"<b>Network Interface {i + 1}:</b> Speed", (ni.Speed / 1_000_000).ToString() + " Mbps");
                }
            }
            catch (Exception ex)
            {
                Parameters.Add("Network Info Error", ex.Message);
            }

            // Time Zone Information
            var timeZone = TimeZoneInfo.Local;
            Parameters.Add("Time Zone", timeZone.DisplayName);
            Parameters.Add("Time Zone Standard Name", timeZone.StandardName);
            Parameters.Add("Time Zone UTC Offset", timeZone.BaseUtcOffset.ToString());
            Parameters.Add("Is Daylight Saving Time", timeZone.IsDaylightSavingTime(DateTime.Now).ToString());

            // Application Domain Information
            var appDomain = AppDomain.CurrentDomain;
            Parameters.Add("Application Domain", appDomain.FriendlyName);
            Parameters.Add("Base Directory", appDomain.BaseDirectory);

            // Logical Drivers
            List<string> logical = Environment.GetLogicalDrives().ToList<string>();
            string result = "";
            foreach (string item in logical)
            {
                result = result + string.Format("{0}, ", item);
                
                // Drive Information
                try
                {
                    var driveInfo = new DriveInfo(item);
                    if (driveInfo.IsReady)
                    {
                        Parameters.Add($"<b>Drive</b> {item} Type", driveInfo.DriveType.ToString());
                        Parameters.Add($"<b>Drive</b> {item} Format", driveInfo.DriveFormat);
                        Parameters.Add($"<b>Drive</b> {item} Total Size (GB)", (driveInfo.TotalSize / (1024 * 1024 * 1024)).ToString());
                        Parameters.Add($"<b>Drive</b> {item} Available Space (GB)", (driveInfo.AvailableFreeSpace / (1024 * 1024 * 1024)).ToString());
                    }
                }
                catch (Exception ex)
                {
                    Parameters.Add($"Drive {item} Error", ex.Message);
                }
            }
            Parameters.Add("Logical Drivers", result);

            // Performance Counters (if available)
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                    cpuCounter.NextValue(); // First call returns 0
                    Thread.Sleep(100); // Wait a bit for accurate reading
                    Parameters.Add("CPU Usage %", cpuCounter.NextValue().ToString("F2"));
                }
                else
                {
                    Parameters.Add("CPU Usage", "Not available on this platform");
                }
            }
            catch (Exception ex)
            {
                Parameters.Add("CPU Usage Error", ex.Message);
            }

            // Server Startup Time
            Parameters.Add("Server Started At", currentProcess.StartTime.ToString("yyyy-MM-dd HH:mm:ss UTC"));
            Parameters.Add("Server Uptime", (DateTime.Now - currentProcess.StartTime).ToString(@"dd\.hh\:mm\:ss"));

            // CLR Information
            Parameters.Add("GC Generation 0 Collections", GC.CollectionCount(0).ToString());
            Parameters.Add("GC Generation 1 Collections", GC.CollectionCount(1).ToString());
            Parameters.Add("GC Generation 2 Collections", GC.CollectionCount(2).ToString());

            // Environment Variables (filtered)
            IDictionary environmentVariables = Environment.GetEnvironmentVariables();
            foreach (DictionaryEntry de in environmentVariables)
            {
                if (
                    de.Key.ToString() != "Path"
                    //&& de.Key.ToString() != "PSModulePath"
                    //&& de.Key.ToString() != "DOTNET_STARTUP_HOOKS"
                    && de.Key.ToString() != "ASPNETCORE_AUTO_RELOAD_WS_KEY"
                    )
                {
                    Parameters.Add($"<b>env:</b> {de.Key}", de.Value.ToString());
                }
            }
        }
    }
}
