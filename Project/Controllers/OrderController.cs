using Microsoft.AspNetCore.Mvc;
using Project.Models.Entities;
using Project.Models.Interfaces;
using System.Collections.Generic;

public class OrderController : Controller
{
    private readonly IOrderRepository _orderRepository;

    public OrderController()
    {
        _orderRepository = new OrderRepository(); // Hardcoded repository instantiation
    }

    public ActionResult Index()
    {
        List<Order> orders = _orderRepository.GetAll();
        return View(orders);
    }

    public IActionResult Details(int id)
    {
        var order = _orderRepository.GetById(id);
        
        return View();
    }


    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Order order)
    {
        if (ModelState.IsValid)
        {
            _orderRepository.Create(order);
            return RedirectToAction("Index");
        }
        return View(order);
    }

    public ActionResult Edit(int id)
    {
        Order order = _orderRepository.GetById(id);
        if (order == null)
        {
            return NotFound();
        }
        return View(order);
    }

    [HttpPost]
    public ActionResult Edit(Order order)
    {
        if (ModelState.IsValid)
        {
            _orderRepository.Update(order);
            return RedirectToAction("Index");
        }
        return View(order);
    }

    public ActionResult Delete(int id)
    {
        Order order = _orderRepository.GetById(id);
        
        return View();
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        _orderRepository.Delete(id);
        return RedirectToAction("Index");
    }
}
