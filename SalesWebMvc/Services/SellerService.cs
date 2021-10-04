using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Services
{
  public class SellerService
  {
    private readonly SalesWebMvcContext _context;

    public SellerService(SalesWebMvcContext context)
    {
      _context = context;
    }


    //lista todos os vendedores do banco
    public List<Seller> FindAll()
    {
      return _context.Seller.ToList();
    }

    public void Insert(Seller seller)
    {
      seller.Departament = _context.Departament.First();
      _context.Add(seller);
      _context.SaveChanges();
    }
  }
}
