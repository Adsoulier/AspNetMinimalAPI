using Microsoft.EntityFrameworkCore;
using MinimalWebApi.Data;
using MinimalWebApi.Models;
using MinimalWebApi.Services;
using Xunit;

namespace MinimalWebApi.Tests
{
    public class ServicesTests
    {

        private ContactService CreateService(string dbName)
        {
            var options = new DbContextOptionsBuilder<ContactsContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

            var context = new ContactsContext(options);

            return new ContactService(context);
        }

        [Fact]
        public void TestGetAllContacts()
        {
            var service = CreateService("testGetAll");

            Assert.NotEmpty(service.GetAllContacts());
        }

        [Fact]
        public void TestCreateContact()
        {
            var service = CreateService("testCreateContact");

            _ = service.CreateContactAsync(new Contact
            {
                Name = "Test User",
                FistName = "Test",
                Email = "yes@yes"
            });

            Assert.Contains(service.GetAllContacts(), c => c.Name == "Test User" && c.FistName == "Test" && c.Email == "yes@yes");
        }
    }
}