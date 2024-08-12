using Microsoft.Extensions.Configuration;
using Products.Domain.Models;
using Products.Domain.Repositories;
using Products.Domain.Services;
using Products.Persistence;
using Products.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Diagnostics;
using System.Xml.Linq;

namespace UnitTests
{
    internal class ProductsTests
    {
        private IProductService productServiceVar;

        [OneTimeSetUp]
        public void Setup()
        {
            // you'll need to change the SqliteConnString path in UnitTests/appsettings.json to point to UnitTests/testdatabase.db
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IProductRepository repo = new ProductRepository(config);
            productServiceVar = new ProductService(repo);
        }
        [Test]
        public void VerifyCreateProduct()
        {
            
            Product prod =  productServiceVar.CreateProduct("Table",7,2);
            Assert.AreEqual("Table",prod.Name);
            Assert.AreEqual(7,prod.Price);
            Assert.AreEqual(2, prod.Stock);
            //TC1:Verify that a new product can be successfully created when provided with valid name, price, and initialStock under create method.

        }
        [Test]
        public void VerifyCreateProductWithNegativePrice()
        {

            Product prod = productServiceVar.CreateProduct("egg", -9, 1786);
            Assert.AreEqual(-9, prod.Price);
            //TC2:verify that the price is not taking values less than 0 under create method.
            // Negative case:here the service is allowing invalid inputs without error,so wanted to suggest validation logic in the actual service code.

        }
        [Test]
        public void VerifyCreateProductPriceLessThanOne()
        {

            Product prod = productServiceVar.CreateProduct("egg", 0.1, 1786);
            Assert.AreEqual(0.1, prod.Price);
            //TC3:verify that the price is taking values less than 1 and greater than 0 under create method.

        }
        [Test]
        public void VerifyCreateProductWithPriceZero()
        {

            Product prod = productServiceVar.CreateProduct("bag", 0, 1786);
            Assert.AreEqual(0, prod.Price);
            //TC4:verify that the price value is taking "0" input under create method.

        }
        [Test]
        public void VerifyCreateProductStockZero()
        {

           Product prod = productServiceVar.CreateProduct("bag", 8, 0);
           Assert.AreEqual(0, prod.Stock);
           //TC5:verify that the stock value is taking "0" input under create method.

        }
        [Test]
        public void VerifyGetAllProducts()
        {
           
            Product prod1 = productServiceVar.CreateProduct("cot", 650, 36);
            Product prod2 = productServiceVar.CreateProduct("pillow", 70, 100);
            List<Product> productList = productServiceVar.GetAllProducts();
            Assert.That(productList, Does.Contain(prod1));
            Assert.That(productList, Does.Contain(prod2));
            //TC6:Verify that all product are successfully listed when executed Get method.

        }
        [Test]
        public void VerifyGetProductbyId()
        {
            
            Product productList = productServiceVar.GetProduct(1);
            Assert.AreEqual(1, productList.Id);
            //TC7:Verify that the given product is successfully listed when executed Get(id) method.

        }
        [Test]
        public void VerifyGetProductbyNonexistid()
        {
           
            Product productList = productServiceVar.GetProduct(100);
            Assert.AreEqual(null, productList);
            //TC8:Verify that when parsed non existing id under Get(id)method,it should give null value.

        }
        [Test]
        public void VerifyDeleteProduct()
        {
           
            bool productList = productServiceVar.DeleteProduct(23);
            Assert.AreEqual(true, productList);
            //TC9:Verify that product of given id is successfully deleted when executed delete method and returns true.

        }
        [Test]
        public void VerifyDeleteProductNonexistID()
        {
           
            bool productList = productServiceVar.DeleteProduct(2000);
            Assert.AreEqual(false, productList);
            //TC10:Verify that when a non-existing id is parsed under delete method, it should return false 

        }
        [Test]
        public void VerifyDeleteProductandGetId()
        {
           
            bool productList = productServiceVar.DeleteProduct(24);
            Product productList1 = productServiceVar.GetProduct(24);
            Assert.AreEqual(null, productList1);
            Assert.AreEqual(true, productList);
            //TC11:Verify when given an id under delete method, that respective product should be successfully deleted and verify that product id returns null under get(id) method.

        }
        [Test]
        public void VerifyUpdateProduct()
        {
          
            Product p = new Product();
            p.Id = 33;
            p.Name = "Paint brush";
            p.Price = 65;
            p.Stock = 100;
            Product productList = productServiceVar.UpdateProduct(p);
            Assert.AreEqual(65, productList.Price);
            Assert.AreEqual(100, productList.Stock);
            Assert.AreEqual("Paint brush", productList.Name);
            //TC12: Verify that a product can be successfully updated when provided with valid id,name, price, and stock values under update method.

        }
        [Test]
        public void VerifyUpdateProductName()
        {
           
            Product p1 = new Product();
            p1.Id = 19;
            p1.Name = "pe#%8n";
            p1.Price = 6;
            p1.Stock = 60;
            Product productList = productServiceVar.UpdateProduct(p1);
            Assert.AreEqual("pe#%8n", productList.Name);
            //TC13: Verify that a product can be successfully updated when given any data type within the string under update method.

        }
        [Test]
        public void VerifyUpdateProductPrice()
        {
           
            Product p2 = new Product();
            p2.Id = 30;
            p2.Name = "paint";
            p2.Price = 6.5787;
            p2.Stock = 60;
            Product productList = productServiceVar.UpdateProduct(p2);
            Assert.AreEqual(6.5787, productList.Price);
            //TC14: Verify that a product can be successfully updated when given Double data type as price input under update method.

        }
        [Test]
        public void VerifyUpdateProductNonExistID()
        {
           
            Product p3 = new Product();
            p3.Id = 700;
            p3.Name = "paint";
            p3.Price = 6.5787;
            p3.Stock = 60;
            Product productList = productServiceVar.UpdateProduct(p3);
            Assert.AreEqual(null, productList);
            //TC15: Verify that when given a nonexisting product under update method,it should return null values.

        }




    }
}
