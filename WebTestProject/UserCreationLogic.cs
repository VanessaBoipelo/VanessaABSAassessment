using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebTestProject
{
    public static class UserCreationLogic
    {
        public static List<User> GetUsersFromExcel()
        {
            var list = new List<User>();
            ExtractUsersFromExcelFile(list);
            return list;
        }

        public static void ExtractUsersFromExcelFile(List<User> list)
        {

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\Venessa Matlala\source\repos\VanessaABSAassessment\UsersList.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 2; i <= rowCount; i++)
            {
                var newUser = new User
                {
                    FirstName = xlRange.Cells[i, 1].Value2.ToString(),
                    LastName = xlRange.Cells[i, 2].Value2.ToString(),
                    Username = xlRange.Cells[i, 3].Value2.ToString(),
                    Password = xlRange.Cells[i, 4].Value2.ToString(),
                    Customer = xlRange.Cells[i, 5].Value2.ToString(),
                    Role = xlRange.Cells[i, 6].Value2.ToString(),
                    Email = xlRange.Cells[i, 7].Value2.ToString(),
                    Cell = xlRange.Cells[i, 8].Value2.ToString()
                };

                list.Add(newUser);
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}
