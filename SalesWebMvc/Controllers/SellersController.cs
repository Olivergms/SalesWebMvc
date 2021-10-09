using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
  public class SellersController : Controller
  {
    private readonly SellerService _sellerService;
    private readonly DepartamentService _departamentService;

    public SellersController(SellerService sellerService, DepartamentService departamentService)
    {
      _sellerService = sellerService;
      _departamentService = departamentService;
    }

    public async Task<IActionResult> Index()
    {
      var list = await _sellerService.FindAllAsync();
      return View(list);
    }

    public async Task<IActionResult> Create()
    {
      var departaments = await _departamentService.FindAllAsync();

      var viewModel = new SellerFormViewModel { Departaments = departaments };
      return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Seller seller)
    {
      if (!ModelState.IsValid)
      {
        var viewModel = new SellerFormViewModel
        {
          Departaments = await _departamentService.FindAllAsync(),
          Seller = seller
        };

        return View(viewModel);
      }

      await _sellerService.InsertAsync(seller);
      return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return RedirectToAction(nameof(Error), new { msg = "Id not provided"});
      }

      var obj = await _sellerService.FindByIdAsync(id.Value);

      if (obj == null)
      {
        return RedirectToAction(nameof(Error), new { msg = "Id not found" });
      }


      return View(obj);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
      await _sellerService.RemoveAsync(id);
      return RedirectToAction(nameof(Index));
      }
      catch(IntegrityException e)
      {
        return RedirectToAction(nameof(Error), new { msg = e.Message});
      }
    }

    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return RedirectToAction(nameof(Error), new { msg = "Id not provided" });
      }

      var obj = await _sellerService.FindByIdAsync(id.Value);

      if (obj == null)
      {
        return RedirectToAction(nameof(Error), new { msg = "Id not found" });
      }


      return View(obj);
    }

    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return RedirectToAction(nameof(Error), new { msg = "Id not provided" });
      }

      var obj = await _sellerService.FindByIdAsync(id.Value);

      if (obj == null)
      {
        return RedirectToAction(nameof(Error), new { msg = "Id not found" });
      }

      List<Departament> departaments = await _departamentService.FindAllAsync();

      SellerFormViewModel viewModel = new SellerFormViewModel
      {
        Seller = obj,
        Departaments = departaments
      };


      return View(viewModel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Seller seller)
    {

      if (!ModelState.IsValid)
      {
        var viewModel = new SellerFormViewModel
        {
          Departaments = await _departamentService.FindAllAsync(),
          Seller = seller
        };

        return View(viewModel);
      }

      if (id != seller.Id)
      {
        return RedirectToAction(nameof(Error), new { msg = "Id mismatch" });
      }

      try
      {
        await _sellerService.UpdateAsync(seller);

      }
      catch(ApplicationException e)
      {
        return RedirectToAction(nameof(Error), new { msg = e.Message });
      }

      return RedirectToAction(nameof(Index));

    }


    public IActionResult Error(string msg)
    {
      var viewModel = new ErrorViewModel
      {
        Message = msg,
        //pegando id interno da requisição
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
      };

      return View(viewModel);
    }
  }
}
