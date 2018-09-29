using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MdiExtender
{
    [ComVisible(true)]
    [Guid("D6B5474F-F645-4470-9760-7ABE526611CB")]
    [ClassInterface(ClassInterfaceType.None)]
    public class HostWindows : IHostWindow
    {
        #region IHostWindow 


        #endregion
        public void Mostra(string wTitle)
        {
            SampleForm sampleForm = new SampleForm();

            MDIExtender.SetMDIForm(sampleForm, wTitle);
            sampleForm.Show();
        }

        public void MostraPopUp(string wTitle)
        {
            SampleForm sampleForm = new SampleForm();

            MDIExtender.SetMDIForm(sampleForm, wTitle);
            sampleForm.Show();
        }
    }
}
