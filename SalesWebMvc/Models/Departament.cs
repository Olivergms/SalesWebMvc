using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
  public class Departament
  {
    public int Id { get; set; }
    public string Name { get; set; }

    //modelando 1-N
    public ICollection<Seller> Sellers { get; set; } = new List<Seller>();




    public Departament(){}

    public Departament(int id, string name)
    {
      Id = id;
      Name = name;
    }

    public void AddSeller(Seller seller)
    {
      Sellers.Add(seller);
    }
    public void RemoveSeller(Seller seller)
    {
      Sellers.Remove(seller);
    }
    public double TotalSales(DateTime initial, DateTime final)
    {
      return Sellers.Sum(sl => sl.TotalSales(initial, final));
    }
  }
}
