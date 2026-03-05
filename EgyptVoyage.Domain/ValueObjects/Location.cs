using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Domain.ValueObjects;


public class Location
{
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    //lma mongodb tgeb el data mn eldb then convert it to c# object --Deserialization
    public Location() { }

    public Location(string city, string address) 
    {
        City = city;
        Address = address;
     
    }
}