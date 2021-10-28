using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STApi.Data;
using STApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace STApi.Commands
{
    public class SaveProduct : IRequest<IActionResult>
    {
        public Product Body { get; private set; }
        public SaveProduct(Product body)
        {
            Body = body;
        }
    }

    public class SaveProductHandler : IRequestHandler<SaveProduct, IActionResult>
    {
        private DataContext _context;


        public SaveProductHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Handle(SaveProduct request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                if (request.Body.ProductId == 0)
                {
                    _context.Products.Add(request.Body);
                } else
                {
                    var course = _context.Products.FirstOrDefault(x => x.ProductId == request.Body.ProductId);

                    if (course == null)
                    {
                        return new OkObjectResult(new ResponseFailed("Data not found."));
                    }
                    course.ProductName = request.Body.ProductName;
                    course.Price = request.Body.Price;
                    course.Path = request.Body.Path;
                    _context.Products.Attach(course).State = EntityState.Modified;
                }
                _context.SaveChangesAsync();
                return new OkObjectResult(new ResponseSuccess(request.Body));

            });
        }
    }
}
