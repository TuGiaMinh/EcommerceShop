using EcommerceShop.Application.Service.Storage;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Backend.Tests
{
    public static class FileStorageServiceMock
    {
        public static IStorageService FileStorageService()
        {
            var fileStorageService = Mock.Of<IStorageService>();

            return fileStorageService;
        }
    }
}
