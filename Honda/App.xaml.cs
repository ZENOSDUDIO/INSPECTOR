using System.Windows;
using GalaSoft.MvvmLight.Threading;
using Honda.ViewModel;
using System.Diagnostics;

namespace Honda
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //当前运行WPF程序的进程实例
            Process process = Process.GetCurrentProcess();
            //遍历WPF程序的同名进程组
            foreach (Process p in Process.GetProcessesByName(process.ProcessName))
            {
                //不是同一进程并且本进程启动时间最晚,则关闭较早进程
                if (p.Id != process.Id && (p.StartTime - process.StartTime).TotalMilliseconds <= 0)
                {
                    p.Kill(); //这个地方用kill 而不用Shutdown();的原因是,Shutdown关闭程序在进程管理器里进程的释放有延迟不是马上关闭进程的
                    //Application.Current.Shutdown();
                    return;
                }
            }
            base.OnStartup(e);
        }
    }
}