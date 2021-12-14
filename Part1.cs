using System;
using System.Collections.Generic;
using System.IO;

namespace Part1
{
    static class TaxCalculator
    {
        static Dictionary<string, IList<TaxRecord>> taxDictionary = new Dictionary<string, IList<TaxRecord>>();

        // create a static constructor that:
        static TaxCalculator()
        {
            int floor;
            long ceiling;
            double rate;
            string stateAbbr;
            string stateName;
            // enter a try/catch block for the entire static constructor to print out a message if an error occurs
            try
            {
                // declare a streamreader to read a file
                // initialize the static dictionary to a newly create empty one
                // open the taxtable.csv file into the streamreader
                using (StreamReader sr = new StreamReader("taxtable.csv"))
                {
                    // loop over the lines from the streamreader
                    // read a line from the file

                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            var line = sr.ReadLine();
                            var values = line.Split(',');
                            var success = int.TryParse(values[2], out int value2);
                            var success2 = Int64.TryParse(values[3], out Int64 value3);
                            var success3 = double.TryParse(values[4], out double value4);

                            if (!success)
                            {
                                throw new Exception($"{values[2]} was not able to be parsed to an integer.");

                            }
                            else if (!success2)
                            {
                                throw new Exception($"{values[3]} was not able to be parsed to Int64.");

                            }
                            else if (!success3)
                            {
                                throw new Exception($"{values[4]} was not able to be parsed to a double.");
                            }

                            stateAbbr = values[0];
                            stateName = values[1];
                            floor = value2;
                            ceiling = value3;
                            rate = value4;
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                            continue;
                        }

                        TaxRecord taxRecord = new TaxRecord
                        {
                            StateCode = stateAbbr,
                            State = stateName,
                            Floor = floor,
                            Ceiling = ceiling,
                            Rate = rate
                        };

                        if (taxDictionary.ContainsKey(taxRecord.StateCode))
                        {
                            taxDictionary[taxRecord.StateCode].Add(taxRecord);
                        }
                        else
                        {
                            IList<TaxRecord> taxRecords = new List<TaxRecord>();
                            taxRecords.Add(taxRecord);
                            taxDictionary.Add(taxRecord.StateCode, taxRecords);
                        }
                    }
                }
                //Using makes sure the streamreader is disposed no matter what happens.
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);

            }
        }     
            


        // create a static method (ComputeTaxFor)  to return the computed tax given a state and income
        //  use the state as a key to find the list of taxrecords for that state
        //   throw an exception if the state is not found.
        //   otherwise use the list to compute the taxes
        public static double ComputeTaxFor(string stateAbb, int income)
        {
            string modeBool = File.ReadAllText("verbose.txt");
            //  Create a variable to hold the final computed tax.  set it to 0
            double computedTax = 0;

            foreach(var entry in taxDictionary[stateAbb])
            {
                if(income >= entry.Floor && income <= entry.Ceiling)
                {
                    computedTax += income * entry.Rate;
                    if (modeBool == "true")
                        Console.WriteLine($"Tax was computed by multiplying income ${income} by the rate {entry.Rate}");
                }
                else if(income > entry.Ceiling)
                {
                    computedTax += (entry.Ceiling - entry.Floor) * entry.Rate;
                    if (modeBool == "true")
                        Console.WriteLine($"Income {income} was more than the tax bracket's ceiling. Subtracting tax floor {entry.Floor} from tax ceiling {entry.Ceiling} and multiplying by rate {entry.Rate}. Moving to next tax bracket.");
                }
            }
            return computedTax;

            //  loop over the list of taxrecords for the state
            //     check to see if the income is within the tax bracket using the floor and ceiling properties in the taxrecord
            //     if NOT:  (the income is greater than the ceiling)
            //        compute the total tax for the bracket and add it to the running total of accumulated final taxes
            //        the total tax for the bracket is the ceiling minus the floor times the tax rate for that bracket.  
            //        all this information is located in the taxrecord
            //        after adding the total tax for this bracket, continue to the next iteration of the loop
            //     IF The income is within the tax bracket (the income is higher than the floor and lower than the ceiling
            //        compute the final tax by adding the tax for this bracket to the accumulated taxes
            //        the tax for this bracket is the income minus the floor time the tax rate for this bracket
            //        this number is the total final tax, and can be returned as the final answer

        }


        // try to figure out how to implement the Verbose option AFTER you have everything else done.


    } // this is the end of the Tax Calculator

    
    public class TaxRecord
    {
        // Create a TaxRecord class representing a line from the file.  It should have public properties of the correct type for each of the columns in the file

        //  StateCode   (used as the key to the dictionary)
        public string StateCode { get; set; }
        //  State       (Full state name)
        public string State { get; set; }
        //  Floor       (lowest income for this tax bracket)
        public int Floor { get; set; }
        //  Ceiling     (highest income for this tax bracket )
        public long Ceiling { get; set; }
        //  Rate        (Rate at which income is taxed for this tax bracket)
        public double Rate { get; set; }

        public override string ToString()
        {
            return $"This tax record's state and state abbreviation are: {StateCode}, {State}. The income floor is {Floor} and the ceiling is {Ceiling}. The tax rate for this bracket is {Rate}.";
        }
        //  Create a ctor taking a single string (a csv) and use it to load the record
        //  Be sure to throw detailed exceptions when the data is invalid
        //
        //  Create an override of ToString to print out the tax record info nicely


    }  // this is the end of the TaxRecord

    class Program
    {
        public static void Main()
        {
            // create an infinite loop to:
            // prompt the user for a state and an income
            // validate the data
            // calculate the tax due and print out the total
            // loop

            // after accomplishing this, you may want to also prompt for verbose mode or not in this loop
            // wrap everythign in a try/catch INSIDE the loop.  print out any exceptions that are unhandled
            //  something like this:
            do
            {
                try
                {
                    string modeBool = File.ReadAllText("verbose.txt");
                    if (modeBool == "false")
                    {
                        Console.WriteLine("This program is currently in silent mode. If you would like to change to verbose mode, please enter yes. If not, enter no.");
                        string changeMode = Console.ReadLine();
                        changeMode = changeMode.ToUpper();
                        if (changeMode == "YES")
                        {
                            modeBool = modeBool.Replace("false", "true");
                            File.WriteAllText("verbose.txt", modeBool);
                        }
                    }
                    else
                    {
                        Console.WriteLine("This program is currently in verbose mode. If you would like to change to silent mode, please enter yes. If not, enter no.");
                        string changeMode = Console.ReadLine();
                        changeMode = changeMode.ToUpper();
                        if (changeMode == "YES")
                        {
                            modeBool = modeBool.Replace("true", "false");
                            File.WriteAllText("verbose.txt", modeBool);
                        }
                    }
                        
                    Console.WriteLine("Please enter your state abbreviation: ");
                    string stateAbbr = Console.ReadLine();
                    Console.WriteLine("Please enter your income: ");
                    var success = int.TryParse(Console.ReadLine(), out int income);

                    if (!success)
                        throw new Exception("Your entry for income was invalid, please enter an integer.");

                    var computed = TaxCalculator.ComputeTaxFor(stateAbbr, income);
                    Console.WriteLine("Your tax amount is: {0:0.00}\n", computed);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (true);
        }
    }

}
