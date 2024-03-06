using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Context.GymDbContext;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    public readonly GymDbContext context;

    public TestController(GymDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public List<TestData> Get()
    {
        return context.Data.ToList();
    }

    [HttpPost]
    public void Post(TestData data)
    {
        context.Data.Add(data);
        context.SaveChanges();
    }
}