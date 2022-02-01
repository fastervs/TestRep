using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace TestGeogrInfaCom
{
    class VM:ObservableObject
    {
        public Model Model { get; set; }

        private Visibility _isBusy=Visibility.Collapsed;
        public Visibility IsBusy
        {
            get => _isBusy;
            set
            {
                if (value != _isBusy)
                {
                    _isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }

        public VM()
        {
            Model = new Model();
        }

        public ICommand ShowTable
        {
            get => new  DelegateCommand(async (object obj) =>
            {
                IsBusy = Visibility.Visible;
               await Model.GetVariables(Model.SelectedVariable);
                IsBusy = Visibility.Collapsed;
            });
        }

        public ICommand Calculate
        {
            get => new DelegateCommand(async (object obj) =>
            {
                IsBusy = Visibility.Visible;
                await Model.FillFx();
                IsBusy = Visibility.Collapsed;
            });
        }

        public ICommand SaveXlsx
        {
            get => new DelegateCommand(async (object obj) =>
            {
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Filter = "Excel файлы (*.xlsx)|*xslx";
                SFD.DefaultExt = ".xlsx";
                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    if(SFD.FileName!=null)
                    {
                        await Model.SaveXlsx(SFD.FileName);
                    }
                }
            });
        }
    }

    
}
