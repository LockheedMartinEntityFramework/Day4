using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;

namespace EDMX_Database_First
{
    public static class ExceptionHandling
    {
        public static void TryLoad<T>(this IQueryable<T> ExtensionDBSet) where T : class
        {
            try
            {
                ExtensionDBSet.Load();
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine("Misconfigured EF Connection String! " + ex.Message);
            }
            catch (MetadataException ex)
            {
                // Show an error message.
                System.Diagnostics.Debug.WriteLine("Can't Find .EDMX Metadata! " + ex.Message);
                // Exit the event handler.
                return;
            }
            catch (DataException ex)
            {
                // Show an error message.
                System.Diagnostics.Debug.WriteLine("Unexpected Data Exception! " +
                    ex.GetType().FullName + " " + ex.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Boom... " + ex.Message);
            }
        }
    }   
}
