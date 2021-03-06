using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
  public class Seller
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "{0} Required")]
    //{0} => atributo, {1} => primeiro argumento e {2} o segundo argumento
    [StringLength(60, MinimumLength = 3, ErrorMessage ="{0} size should be between {2} and {1}")]
    public string Name { get; set; }


    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "{0} Required")]
    [EmailAddress(ErrorMessage ="Enter a valid Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "{0} Required")]
    [Range(100.0, 50000.0, ErrorMessage ="{0} must be from {1} to {2}")]
    [Display(Name ="Base Salary")]
    [DisplayFormat(DataFormatString ="{0:F2}")]
    public double BaseSalary { get; set; }

    [Required(ErrorMessage = "{0} Required")]
    [Display(Name="Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; }

    public int DepartamentId { get; set; }

    //modelando 1-N
    public Departament Departament { get; set; }
    //modelando 1-N
    public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

    public Seller(){}

    public Seller(int id, string name, string email, double baseSalary, DateTime birthDate, Departament departament)
    {
      Id = id;
      Name = name;
      Email = email;
      BaseSalary = baseSalary;
      BirthDate = birthDate;
      Departament = departament;
    }

    public void AddSales(SalesRecord sr)
    {
      Sales.Add(sr);
    }
    public void RemoveSales(SalesRecord sr)
    {
      Sales.Remove(sr);
    }
    public double TotalSales(DateTime initial, DateTime final)
    {
      return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
    }
  }
}
