using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDMX_Database_First
{
    [MetadataType(typeof(CustomerMetaData))]
    public partial class Customer
    {
        public string FullName { get { return String.Format("{0} {1}", FirstName, LastName); } }
    }

    public class CustomerMetaData
    {
        [MinLength(1, ErrorMessage="Last Name Must Have Data")]
        public string LastName { get; set; }
    }
}
