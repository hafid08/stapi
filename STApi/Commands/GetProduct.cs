using MediatR;
using Microsoft.AspNetCore.Mvc;
using STApi.Data;
using STApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace STApi.Commands
{
    public class GetProduct : IRequest<IActionResult>
    {
        public int CourseId { get; private set; }
        public GetProduct(int courseId = 0)
        {
            this.CourseId = courseId;
        }

    }
    public class GetProdutHandler : IRequestHandler<GetProduct, IActionResult>
    {

        private DataContext _context;


        public GetProdutHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Handle(GetProduct request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                if (request.CourseId == 0)
                {
                    var courses = _context.Products.ToList();
                    return new OkObjectResult(new { Data = courses });
                }
                else
                {
                    var course = _context.Products.FirstOrDefault(x => x.ProductId == request.CourseId);

                    if (course == null)
                    {
                        return new OkObjectResult(new ResponseFailed("Data not found."));
                    }
                    return new OkObjectResult(new ResponseSuccess(course));
                }
            });
        }

    }
}
