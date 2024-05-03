using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class Maybe
    {
        public static TResult With<TInput, TResult>(this TInput o,  Func<TInput, TResult> func)
    }

    public partial class Person
    {
        public Address Address { get; set; }
    }

    public class Address
    {
        public string PostCode { get; set; }
    }

    internal class MaybeMonadDemo
    {
        public void MyMethod(Person p)
        {
            string postcode;
            if (p != null)
            {
                if (HasMedicalRecord(p) && p.Address != null)
                {
                    CheckAddress(p.Address);
                    if (p.Address.PostCode != null)
                    {
                        postcode = p.Address.PostCode.ToString();
                    }
                    else
                    {
                        postcode = "UNKNOWN";
                    }
                }
            }
        }

        private void CheckAddress(Address address)
        {
            throw new NotImplementedException();
        }

        private bool HasMedicalRecord(Person person)
        {
            throw new NotImplementedException();
        }

        static void Main(string[] args)
        {

        }
    }
}
