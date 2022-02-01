
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace TestGeogrInfaCom
{
    [Table("x")]
    public class TestVariable : ObservableObject
    {
        private int _var;

        [Key]
        public int rowid { get; set; }

        public int val { get; set; }

        public int var
        {
            get { return _var; }
            set
            {
                _var = value;
                OnPropertyChanged("var");
            }
        }
    }

    [Table("fx")]
    public class Result
    {
        [Key]
        public int rowid { get; set; }

        public int val { get; set; }

        public int var { get; set; }
        
    }

    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }

        public DbSet<TestVariable> TestVariables { get; set; }

        public DbSet<Result> CalculatedData { get; set; }
    }
}
