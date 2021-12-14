using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2
{


    public class EmployeeRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StateCode { get; set; }
        public int HoursWorkedInTheYear { get; set; }
        public double HourlyRate { get; set; }
        public int YearlyPay
        {
            get
            {
                return (int)(HourlyRate * HoursWorkedInTheYear);
            }
        }
        public double ComputedTax
        {
            get
            {
                return Part1.TaxCalculator.ComputeTaxFor(StateCode, YearlyPay);
            }
        }

        public override string ToString()
        {
            return $"Employee Id: {Id}, Employee Name: {Name}, State Abbreviation: {StateCode}, Hours Worked This Year: {HoursWorkedInTheYear}, Hourly Rate: {HourlyRate}, Yearly Pay: {YearlyPay}, Computed Tax: $" + string.Format("{0:0.00}", ComputedTax);
        }
        // create an employee Record with public properties
        //    ID                        a number to identify an employee
        //    Name                      the employee name
        //    StateCode                 the state collecting taxes for this employee
        //    HoursWorkedInTheYear      the total number of hours worked in the entire year (including fractions of an hour)
        //    HourlyRate                the rate the employee is paid for each hour worked
        //                                  assume no changes to the rate throughout the year.

        //    provide a constructor that takes a csv and initializes the employeerecord
        //       do all error checking and throw appropriate exceptions

        //    provide an additional READ ONLY property called  YearlyPay that will compute the total income for the employee
        //        by multiplying their hours worked by their hourly rate

        //    provide an additional READONLY property that will compute the total tax due by:
        //        calling into the taxcalculator providing the statecode and the yearly income computed in the YearlyPay property

        //    provide an override of toString to output the record : including the YearlyPay and the TaxDue
    }

    public static class EmployeesList
    {
        public static IList<EmployeeRecord> employees = new List<EmployeeRecord>();
        static string[] states = new string[] { "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "FL", "GA", "HI", "IA", "ID", "IL", "KS", "KY", "LA", "MA", "MA", "MD", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VA", "VT", "WA", "WI", "WV", "WY"};

        static EmployeesList()
        {
            int id;
            string name;
            string stateAbbr;
            int hoursWorkedYear;
            double hourlyRate;
            try
            {
                using (StreamReader sr = new StreamReader("employees.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            var line = sr.ReadLine();
                            var values = line.Split(',');
                            var success = int.TryParse(values[0], out int value0);
                            var success2 = int.TryParse(values[3], out int value3);
                            var success3 = double.TryParse(values[4], out double value4);

                            if (!success)
                            {
                                throw new Exception($"{values[0]} was not able to be parsed to an integer.");

                            }
                            else if (!success2)
                            {
                                throw new Exception($"{values[3]} was not able to be parsed to an integer.");

                            }
                            else if (!success3)
                            {
                                throw new Exception($"{values[4]} was not able to be parsed to a double.");
                            }
                            if (!states.Contains(values[2]))
                            {
                                throw new Exception($"State code {values[2]} is not a valid state abbreviation.");
                            }

                            id = value0;
                            name = values[1];
                            stateAbbr = values[2];
                            hoursWorkedYear = value3;
                            hourlyRate = value4;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }

                        EmployeeRecord employee = new EmployeeRecord
                        {
                            Id = id,
                            Name = name,
                            StateCode = stateAbbr,
                            HoursWorkedInTheYear = hoursWorkedYear,
                            HourlyRate = hourlyRate
                        };
                        employees.Add(employee);
                    }
                   
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        // create an EmployeeList class that will read all the employees form the Employees.csv file
        // the logic is similar to the way the taxcalculator read its taxrecords

        // Create a List of employee records.  The employees are arranged into a LIST not a DICTIONARY
        //   because we are not accessing the employees by state,  we are accessing the employees sequentially as a list

        // create a static constructor to load the list from the file
        //   be sure to include try/catch to display messages

        public static void OuputEmployees()
        {
            foreach(var employee in employees)
            {
                Console.WriteLine(employee.ToString());
            }
        }
        
    }

    class Program
    {
        // loop over all the employees in the EmployeeList and print them
        // If the ToString() in the employee record is correct, all the data will print out.

        public static void Main()
        {

            try
            {
                EmployeesList.OuputEmployees();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
