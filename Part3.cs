using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Extend the program again to allow the user to sort the list of employees by one of several attributes:

//State
//Employee ID
//Employee Name
//Yearly Pay
//Tax due

// use LINQ logic to do this

// here is a stub main that will prompt the user to enter a sort column, and a sort direction.
// examine the logic of the code 
// run the logic and try putting in invalid entries and valid entries.
//   once you select a column and sort order it is expected that you will throw an exception
//   because the LINQ statements are currently empty
//
//   Once you understand how the basice Main works, then add the appropriate LINQ statements to the
//   various parts of the switch statements.  Now the sorting will start

//   You will need to figure out the syntax for EMPLOYEECOLLECTION.  This is the List that contains all the employee records from Part 2.
//   


namespace Part3
{
    class Program
    {
        public static void Main()
        {
            var employees = Part2.EmployeesList.employees;
            try
            {

                IEnumerable<Part2.EmployeeRecord> FinalQuery = null;   // these default values set to null, as required by c#
                IEnumerable<Part2.EmployeeRecord> ColumnQuery = null;  // these default values set to null, as required by c#
                do
                {

                    // this is the section to choose the sort column
                    Console.Write("choose a column to sort by: (S)tate (N)ame (I)d (P)ay (T)ax or (E)xit:");
                    string selection = Console.ReadLine();
                    // this switch selects the basic column and MAKEs R using Linq from the original Query Q
                    switch(selection.ToUpper())
                    {
                        case ("S"): ColumnQuery = employees.OrderBy(x => x.StateCode).Select(x => x); break;  // use linq logic here to assign the proper query
                        // example
                        //ColumnQuery = from e in EMPLOYEECOLLECTION orderby e.StateCode select e;  break;

                        case ("N"): ColumnQuery = employees.OrderBy(x => x.Name).Select(x => x); break;  // use linq logic here to assign the proper query
                        // example
                        //ColumnQuery = from e in EMPLOYEECOLLECTION orderby e.Name select e;  break;

                        case ("I"): ColumnQuery = employees.OrderBy(x => x.Id).Select(x => x); break;  // use linq logic here to assign the proper query
                        case ("P"): ColumnQuery = employees.OrderBy(x => x.YearlyPay).Select(x => x); break;  // use linq logic here to assign the proper query
                        case ("T"): ColumnQuery = employees.OrderBy(x => x.ComputedTax).Select(x => x); break;  // use linq logic here to assign the proper query
                        case ("E"):  Console.WriteLine("Goodbye..."); return;
                        default:
                            Console.WriteLine("Choice not recognized, try again...");
                            continue;  // this continue is for the outer do (choose a column)
                    }
                    do
                    {
                        Console.Write("choose a direction to sort by: (A)scending (D)escending:");
                        string order = Console.ReadLine();
                        switch (order.ToUpper())
                        {
                            case ("A"): FinalQuery = ColumnQuery; break;             // Set FinalQuery
                                                                                     // To normal order here
                                                                                     // - break out of the switch
                            case ("D"): FinalQuery = ColumnQuery.Reverse(); break;   // Set FinalQuery to the reverse here
                                                                                     // - break out of the switch
                            default:
                                Console.WriteLine("Choice not recognized, try again...");
                                continue;  // this continue is for the inner do (ascending or descending)
                        }
                        break;  // getting here means you have selected both a column and an order
                                // so this break gets out of the outer do so we can continue
                    } while (true);

                    foreach (Part2.EmployeeRecord r in FinalQuery)  
                    {
                        try
                        {
                            Console.WriteLine(r);
                        }
                        catch (Exception ex)  // to catch the exceptions when calculating the tax,
                                              //  and go to the next since it is within the foreach
                        {
                            Console.WriteLine(ex.Message);  
                        }
                    }
                } while (true);

            }

            catch (Exception ex)  // global catch to catch all exceptions, and exit the program
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}

