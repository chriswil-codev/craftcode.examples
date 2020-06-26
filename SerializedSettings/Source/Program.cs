using System;
using System.Runtime.CompilerServices;
using SerializedSettings.Source;

namespace SerializedSettings
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Example example = new Example("TestApp");

            example.Settings.StringValue = "Test";
            example.Settings.IntegerValue = 42;
            example.Settings.ComplexValue.Items.Add("Item 1");
            example.Settings.ComplexValue.Items.Add("Item 2");

            example.Save();

            example.Reload();


        }
    }
}
