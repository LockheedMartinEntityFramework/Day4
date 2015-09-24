using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EFWorkflow = EDMX_Database_First;
using EDMX_Database_First;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace TestClient
{
    public partial class EFMasterDetails : Form
    {
        private EFWorkflow.CustomerContextContainer customerContext = new EFWorkflow.CustomerContextContainer();

        public EFMasterDetails()
        {
            InitializeComponent();

            customerContext.Customers.TryLoad();
            //customerContext.Purchases.Load();
            //customerContext.Customers.Include(c => c.Purchases).TryLoad();
        }

        private void EFMasterDetails_Load(object sender, EventArgs e)
        {
            this.customerBindingSource.DataSource = this.customerContext.Customers.Local.ToBindingList();
        }

        private void customerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Used to cause Concurrency Issue, Remember to Set LastName Entity Property Concurrency to Fixed
                this.customerContext.Database.ExecuteSqlCommand(
                    "UPDATE dbo.Customers SET LastName = 'Flintstone2' WHERE CustomerId = 1");

                this.customerContext.SaveChanges();
                this.errorProviderValidation.Clear();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string errorMessage = "Concurrency Exception! ";
                var entry = ex.Entries.Single();


                var currentValues = entry.CurrentValues;
                var databaseValues = entry.GetDatabaseValues();

                MessageBox.Show(String.Format("currentValue = {0}, databaseValue = {1}", currentValues.GetValue<String>("LastName"),
                    databaseValues.GetValue<String>("LastName")));

                entry.Reload();
                errorMessage += "Data Reloaded from Database";

                this.errorProviderValidation.SetError(this.customerIdTextBox, errorMessage);
                this.customerBindingSource.ResetBindings(false);
            }
            catch (DbEntityValidationException ex)
            {
                //this.errorProviderValidation.DataSource = ex.EntityValidationErrors;
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var ve in error.ValidationErrors)
                    {
                        Control controlWithValidationError = this.customerIdTextBox;
                        switch (ve.PropertyName)
                        {
                            case "LastName":
                                controlWithValidationError = this.lastNameTextBox;
                                break;
                            case "FirstName":
                                controlWithValidationError = this.firstNameTextBox;
                                break;
                        }

                        this.errorProviderValidation.SetError(controlWithValidationError, ve.ErrorMessage);
                    }
                }
            }
        }
    }
}
