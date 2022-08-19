using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Syncfusion.UI.Xaml.Gauges;
using System.ComponentModel;
using ClassLibraryPLCTag;

namespace WpfGaugeTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker backgroundworker1 ;

        public int DintagTry { get; private set; }

        public MainWindow()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHJqVVhjWlpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF9jQX9Xd0JnXXtcdHVcQQ==;Mgo+DSMBPh8sVXJ0S0R+XE9HcFRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3xTfkRnWHhadHBXQmReVg==;ORg4AjUWIQA/Gnt2VVhiQlFadVlJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdk1iXX5ZdHBRRGRZUkM=;Njk3MzE3QDMyMzAyZTMyMmUzMExtL2pnV05zYVNLak4yY0FJOWxUMloyRXl4bmNrU2Vxc2c0Z1MzVDhJUEE9;Njk3MzE4QDMyMzAyZTMyMmUzMFBzZ3dIOWROY2trd1ZLeXM0dVJuL01NdksvME83azM1OXRqQ0tZNCsvejA9;NRAiBiAaIQQuGjN/V0Z+Xk9EaFxEVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdEVrWHtfd3BRQmJZUkFy;Njk3MzIwQDMyMzAyZTMyMmUzMEtPQ0x4ZzFyektic0E0SXlxM3pncVBlUTh5TWJSWEo4dWJsanBmQ2k2WHM9;Njk3MzIxQDMyMzAyZTMyMmUzMForRHFOcEdPdmxPUXBhMzYyUDVvVVlHd2RtRUFUY0E1eFZaUEtEL0Y0V0U9;Mgo+DSMBMAY9C3t2VVhiQlFadVlJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdk1iXX5ZdHBRRGZdU0Y=;Njk3MzIzQDMyMzAyZTMyMmUzMEtKbmFNbksySGI1cG1neVJUbWFiU2dsaklXY2pqSmFucVNTWU1DbGRTRTg9;Njk3MzI0QDMyMzAyZTMyMmUzMElnNzAySTNCdVNOU0JpQXpPanhxZFNDWjBmNWszRmxkT2JmZXlFbFUvT289;Njk3MzI1QDMyMzAyZTMyMmUzMEtPQ0x4ZzFyektic0E0SXlxM3pncVBlUTh5TWJSWEo4dWJsanBmQ2k2WHM9");

            InitializeComponent();
            backgroundworker1 = new BackgroundWorker();
            backgroundworker1.DoWork += backgroundworker1_Dowork;
            backgroundworker1.ProgressChanged += backgroundwoker1_ProgressChanged;
            //backgroundworker1.RunWorkerCompleted += backgroundworker1_RunWorkCompleted;
            backgroundworker1.WorkerReportsProgress = true;
            backgroundworker1.WorkerSupportsCancellation = true;
            int DintagTry;
            
        }

        private void BtnReadPLCStart_Click(object sender, RoutedEventArgs e)
        {
            if (!backgroundworker1.IsBusy) 
            {
                backgroundworker1.RunWorkerAsync();
                btnStart.Content = "Stop";
                //int DinTag = CLXSimpleRead.Run("CTR_1.ACC");
                //DummyBox.Text = DinTag.ToString();

            }
            else 
            {
                backgroundworker1.CancelAsync();
                btnStart.Content = "Start";
            }
                


        }
        private void backgroundworker1_Dowork(object sender, DoWorkEventArgs e)
        {
            //BackgroundWorker worker = sender as BackgroundWorker;
            //int TagV_Last = -1;
            int TagV = 0;
           
            //for  (int i=1; i<=10;i++)
            do
            {
                TagV = CLXSimpleRead.Run("CTR_1.ACC");
                DintagTry = TagV;
                System.Threading.Thread.Sleep(200);
                backgroundworker1.ReportProgress(TagV);
               // worker.ReportProgress(TagV);
            } while (!backgroundworker1.CancellationPending);
          
        }

        private void backgroundwoker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TagValueBox.Text = DintagTry.ToString(); //e.ProgressPercentage.ToString(); //+ "Counts");
            DummyBox.Text = (e.ProgressPercentage.ToString() + " Counts");
            P1.Value = DintagTry;
        }
    }
}
