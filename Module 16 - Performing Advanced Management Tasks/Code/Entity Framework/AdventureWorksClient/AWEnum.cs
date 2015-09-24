using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Entity;

namespace AdventureWorksClient
{
    public partial class AWEnum : Form
    {
        private AdventureWorks2012Entities awContext = new AdventureWorks2012Entities();

        public AWEnum()
        {
            InitializeComponent();
            awContext.SalesOrderHeaders.Load();

        }

        private void AWEnum_Load(object sender, EventArgs e)
        {
            salesOrderHeaderBindingSource.DataSource = awContext.SalesOrderHeaders.Local;
            shipMethodIDComboBox.DataSource = Enum.GetValues(typeof(ShipMethods));
            shipMethodIDComboBox.SelectedItem = ShipMethods.Cargo;
        }
    }
}
