using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

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
      _context.Add(seller);
      _context.SaveChanges();
    }

    public Seller FindById(int id)
    {
      //include faz o join com a tabela departament
      return _context.Seller.Include(obj => obj.Departament).FirstOrDefault(s => s.Id == id);
    }

    public void Remove(int id)
    {
      var obj = _context.Seller.Find(id);

      _context.Seller.Remove(obj);
      _context.SaveChanges();
    }

    public void Update(Seller seller)
    {
      if(! _context.Seller.Any(x => x.Id == seller.Id))
      {
        throw new NotFoundException("Id not found");
      }
      try
      {
      _context.Update(seller);
      _context.SaveChanges();
      }
      catch(DbUpdateConcurrencyException e)
      {
        throw new DbConcurrencyException(e.Message);  
      }
    }
  }
}
