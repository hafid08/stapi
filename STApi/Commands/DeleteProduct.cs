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
    public class DeleteProduct : IRequest<IActionResult>
    {
        public int CourseId { get; private set; }
        public DeleteProduct(int courseId)
        {
            CourseId = courseId;
        }
    }
    public class DeleteProductHandler : IRequestHandler<DeleteProduct, IActionResult>
    {
        private DataContext _context;


        public DeleteProductHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Handle(DeleteProduct request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var course = _context.Products.FirstOrDefault(x => x.ProductId == request.CourseId);
                _context.Products.Remove(course);
                _context.SaveChangesAsync();
                return new OkObjectResult(new ResponseSuccess(request.CourseId.ToString()));

            });
        }
    }
}
