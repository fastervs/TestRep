using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace TestGeogrInfaCom
{

    class Model:ObservableObject
    {
        ApplicationContext _db;

        private int _selectedVariable;
        public int SelectedVariable
        {
            get => _selectedVariable;
            set
            {
                if (value != _selectedVariable)
                {
                    _selectedVariable = value;
                    OnPropertyChanged("SelectedVariable");
                }
            }
        }


        public bool HasData
        {
            get { return TestVariables != null && TestVariables.Count > 0; }
            set
            {
                OnPropertyChanged("HasData");
            }
        }

        List<TestVariable> _testvatiables;

        public List<TestVariable> TestVariables
        {
            get { return _testvatiables; }
            set
            {
                _testvatiables = value;
                OnPropertyChanged("TestVariables");
                HasData = true;
            }
        }

        IEnumerable<int> _allVariables;

        public IEnumerable<int> AllVariables
        {
            get { return _allVariables; }
            set
            {
                _allVariables = value;
                OnPropertyChanged("AllVariables");
            }
        }

        public bool HasResult
        {
            get { return ResultData != null && ResultData.Count > 0; }
            set
            {
                OnPropertyChanged("HasResult");
            }
        }

        List<Result> _resultdata;

        public List<Result> ResultData
        {
            get { return _resultdata; }
            set
            {
                _resultdata = value;
                OnPropertyChanged("ResultData");
                HasResult = true;
            }
        }

        public Model()
        {
            _db = new ApplicationContext();
            
            _db.TestVariables.Load();

            _db.CalculatedData.Load();

            AllVariables = _db.TestVariables.Select(v => v.var).Distinct().ToList();

        }

        public async Task GetVariables(int variable)
        {
            await Task.Run(() =>
            {
                TestVariables = _db.TestVariables.Where(v => v.var == variable).ToList();
            });
        }

        public async Task FillFx()
        {
            await Task.Run(() =>
            {
                ResultData = TestVariables.Select(fx=> new Result() { var = fx.val * fx.var, val=fx.val }).ToList();
                _db.CalculatedData.AddRange(ResultData);
                _db.SaveChanges();
            });
        }

        public async Task SaveXlsx(string filepath)
        {
            await Task.Run(() =>
            {
                ExportToExcel.CreateTestExcelFile(filepath,ResultData);
            });
        }

    }
}
